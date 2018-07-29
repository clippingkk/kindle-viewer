using SharpRaven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.QueryStringDotNET;
using System.Diagnostics;

namespace kindle_viewer.Misc
{
    class SentryLogger
    {
        private static RavenClient logger = new RavenClient("https://76acd61ea02341739aa86941f5a931be@sentry.io/1251804");

        public static void Log(Exception e)
        {
#if DEBUG
            Debug.WriteLine(e.ToString());
#else
            logger.CaptureAsync(new SharpRaven.Data.SentryEvent(e));
#endif
            ShowErrorToast(e);
        }

        private static void ShowErrorToast(Exception e)
        {
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children = {
                        new AdaptiveText()
                        {
                            Text = "糟了，出错了！"
                        },
                        new AdaptiveText() {
                            Text = e.Message
                        }
                    },
                },
            };

            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
            };

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

    }
}
