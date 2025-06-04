using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Handlers;

namespace inovasyposmobile.Handlers
{
#if ANDROID
    public class NoBorderBottomEntryhandler : EntryHandler
    {
        protected override void ConnectHandler(AndroidX.AppCompat.Widget.AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);
            platformView.Background = null;
        }
    }
#endif

#if IOS || MACCATALYST
    public class NoBorderBottomEntryhandler : EntryHandler
    {
        protected override void ConnectHandler(MauiTextField platformView)
        {
            base.ConnectHandler(platformView);
            platformView.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
#endif
}