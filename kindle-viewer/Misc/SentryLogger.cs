using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.QueryStringDotNET;
using System.Diagnostics;
using Sentry;
using Windows.System.Profile;

namespace kindle_viewer.Misc {
    class SentryLogger {
        private static IDisposable instance;  

        public static void init() {
            SentryLogger.instance = SentrySdk.Init("https://76acd61ea02341739aa86941f5a931be@sentry.io/1251804");
            setupSystemInfo();
        }

        private static void setupSystemInfo() {
            string sv = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            ulong v = ulong.Parse(sv);
            ulong v1 = (v & 0xFFFF000000000000L) >> 48;
            ulong v2 = (v & 0x0000FFFF00000000L) >> 32;
            ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
            ulong v4 = (v & 0x000000000000FFFFL);
            string version = $"{v1}.{v2}.{v3}.{v4}";
            SentrySdk.ConfigureScope(scope => {
                scope.SetExtra("sysVersion", version);
                scope.SetExtra("sysDevice", AnalyticsInfo.VersionInfo.DeviceFamily);
            });
        }

        public static void Log(Exception e) {
#if DEBUG
            Debug.WriteLine(e.ToString());
#else
            SentrySdk.CaptureException(e);
#endif
            ShowErrorToast(e);
        }

        private static void ShowErrorToast(Exception e) {
            ToastVisual visual = new ToastVisual() {
                BindingGeneric = new ToastBindingGeneric() {
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

            ToastContent toastContent = new ToastContent() {
                Visual = visual,
            };

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public static void destroy() {
            instance.Dispose();
        }

    }
}
