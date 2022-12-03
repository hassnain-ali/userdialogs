using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;
using Orientation = Android.Widget.Orientation;
using Acr.UserDialogs.Infrastructure;
#if ANDROIDX
using Google.Android.Material.BottomSheet;
#else
using Android.Support.Design.Widget;
#endif

namespace Acr.UserDialogs.Fragments;

public class BottomSheetDialogFragment : AbstractAppCompatDialogFragment<ActionSheetConfig>
{
    protected override void SetDialogDefaults(Dialog dialog)
    {
        dialog.KeyPress += OnKeyPress;
        if (Config.Cancel == null)
        {
            dialog.SetCancelable(false);
            dialog.SetCanceledOnTouchOutside(false);
        }
        else
        {
            dialog.SetCancelable(true);
            dialog.SetCanceledOnTouchOutside(true);
            dialog.CancelEvent += (sender, args) => Config.Cancel.Action.Invoke();
        }
    }


    protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
    {
        if (args.KeyCode != Keycode.Back)
            return;

        args.Handled = true;
        Config?.Cancel?.Action?.Invoke();
        Dismiss();
        base.OnKeyPress(sender, args);
    }


    protected override Dialog CreateDialog(ActionSheetConfig config)
    {
        var dlg = new BottomSheetDialog(Activity, config.AndroidStyleId ?? 0);
        var layout = new LinearLayout(Activity)
        {
            Orientation = Orientation.Vertical
        };

        if (!string.IsNullOrWhiteSpace(config.Title))
            layout.AddView(GetHeaderText(config.Title));

        foreach (var action in config.Options)
            layout.AddView(CreateRow(action, false));

        if (config.Destructive != null)
        {
            layout.AddView(CreateDivider());
            layout.AddView(CreateRow(config.Destructive, true));
        }
        if (config.Cancel != null)
        {
            if (config.Destructive == null)
                layout.AddView(CreateDivider());

            layout.AddView(CreateRow(config.Cancel, false));
        }
        dlg.SetContentView(layout);
        dlg.SetCancelable(false);
        return dlg;
    }


    protected virtual View CreateRow(ActionSheetOption action, bool isDestructive)
    {
        var row = new LinearLayout(Activity)
        {
            Clickable = true,
            Orientation = Orientation.Horizontal,
            LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, DpToPixels(48))
        };
        row.SetBackgroundResource(Extensions.GetSelectableItemBackground(Activity));

        if (action.ItemIcon != null)
            row.AddView(GetIcon(action.ItemIcon));

        row.AddView(GetText(action.Text, isDestructive));
        row.Click += (sender, args) =>
        {
            action.Action?.Invoke();
            Dismiss();
        };
        return row;
    }


    protected virtual TextView GetHeaderText(string text)
    {
        var layout = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, DpToPixels(56))
        {
            LeftMargin = DpToPixels(16)
        };
        var txt = new TextView(Activity)
        {
            Text = text,
            LayoutParameters = layout,
            Gravity = GravityFlags.CenterVertical
        };
        txt.SetTextSize(ComplexUnitType.Sp, 16);
        return txt;
    }


    protected virtual TextView GetText(string text, bool isDestructive)
    {
        var layout = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
        {
            TopMargin = DpToPixels(8),
            BottomMargin = DpToPixels(8),
            LeftMargin = DpToPixels(16)
        };

        var txt = new TextView(Activity)
        {
            Text = text,
            LayoutParameters = layout,
            Gravity = GravityFlags.CenterVertical
        };
        txt.SetTextSize(ComplexUnitType.Sp, 16);
        if (isDestructive)
            txt.SetTextColor(Color.Red);

        return txt;
    }


    protected virtual ImageView GetIcon(string icon)
    {
        var layout = new LinearLayout.LayoutParams(DpToPixels(24), DpToPixels(24))
        {
            TopMargin = DpToPixels(8),
            BottomMargin = DpToPixels(8),
            LeftMargin = DpToPixels(16),
            RightMargin = DpToPixels(16),
            Gravity = GravityFlags.Center
        };

        var img = new ImageView(Activity)
        {
            LayoutParameters = layout
        };
        if (icon != null)
            img.SetImageDrawable(ImageLoader.Load(icon));

        return img;
    }


    protected virtual View CreateDivider()
    {
        var view = new View(Activity)
        {
            Background = new ColorDrawable(System.Drawing.Color.LightGray.ToNative()),
            LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, DpToPixels(1))
        };
        view.SetPadding(0, DpToPixels(7), 0, DpToPixels(8));
        return view;
    }


    protected virtual int DpToPixels(int dp)
    {
        var value = TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, Activity.Resources.DisplayMetrics);
        return Convert.ToInt32(value);
    }

    /*
public boolean onTouch(View v, MotionEvent event) {

    final int DELAY = 100;

    if(event.getAction() == MotionEvent.ACTION_UP) {


        RelativeLayout fondo = (RelativeLayout) findViewById(R.id.fondo);

        ColorDrawable f = new ColorDrawable(0xff00ff00);
        ColorDrawable f2 = new ColorDrawable(0xffff0000);
        ColorDrawable f3 = new ColorDrawable(0xff0000ff);
        ColorDrawable f4 = new ColorDrawable(0xff0000ff);

        AnimationDrawable a = new AnimationDrawable();
        a.addFrame(f, DELAY);
        a.addFrame(f2, DELAY);
        a.addFrame(f3, DELAY);
        a.addFrame(f4, DELAY);
        a.setOneShot(false);

        fondo.setBackgroundDrawable(a); // This method is deprecated in API 16
        // fondo.setBackground(a); // Use this method if you're using API 16
        a.start();
     }
     return true;
}
     */
}
