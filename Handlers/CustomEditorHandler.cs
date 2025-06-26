using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Handlers;

namespace inovasyposmobile.Handlers
{
    public class CustomEditorHandler : EditorHandler
    {
#if ANDROID
        protected override void ConnectHandler(AndroidX.AppCompat.Widget.AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);
            platformView.Background = null;
        }
#endif

#if IOS || MACCATALYST
        protected override void ConnectHandler(MauiTextField platformView)
        {
            platformView.BorderStyle = UIKit.UITextBorderStyle.None;
        }
#endif
    }
}