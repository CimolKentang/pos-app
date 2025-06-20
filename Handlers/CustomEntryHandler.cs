using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Handlers;

namespace inovasyposmobile.Handlers
{
    public class CustomEntryHandler : EntryHandler
    {
#if ANDROID
        protected override void ConnectHandler(AndroidX.AppCompat.Widget.AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);
            // bottom border
            platformView.Background = null;

            // cursor color
            var isDark = Application.Current?.RequestedTheme == AppTheme.Dark;
            // platformView.SetHighlightColor(Android.Graphics.Color.Gray);
            platformView?.TextCursorDrawable?.SetTint(isDark ? Android.Graphics.Color.White : Android.Graphics.Color.Black);
        }
#endif

#if IOS || MACCATALYST
        protected override void ConnectHandler(MauiTextField platformView)
        {
            base.ConnectHandler(platformView);
            // bottom border
            platformView.BorderStyle = UIKit.UITextBorderStyle.None;

            // cursor color
            var isDark = Application.Current?.RequestedTheme == AppTheme.Dark;
            platformView.TintColor = isDark ? UIKit.UIColor.White : UIKit.UIColor.Black;
        }
#endif
    }
}