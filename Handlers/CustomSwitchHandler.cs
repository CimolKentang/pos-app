using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Handlers;

namespace inovasyposmobile.Handlers
{
    public class CustomSwitchHandler : SwitchHandler
    {
#if ANDROID
        protected override void ConnectHandler(AndroidX.AppCompat.Widget.SwitchCompat platformView)
        {
            base.ConnectHandler(platformView);

            var trackStates = new Android.Content.Res.ColorStateList(
            new int[][] {
                new int[] { -Android.Resource.Attribute.StateChecked } // OFF state
            },
            new int[] {
                Android.Graphics.Color.ParseColor("#E5E5E5")  // OFF color
            });

            platformView.TrackTintList = trackStates;
            platformView.TrackTintMode = Android.Graphics.PorterDuff.Mode.SrcAtop;
        }
#endif

#if IOS || MACCATALYST
        protected override void ConnectHandler(UISwitch platformView)
        {
            base.ConnectHandler(platformView);

            platformView.BackgroundColor = UIColor.FromRGB(158, 158, 158);
        }
#endif
    }
}