using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TestHelper
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        /// <summary>The app on startup.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void AppOnStartup(object sender, StartupEventArgs e)
        {
            SetWebBrowserVersion(11001);
        }

        /// <summary>
        /// WebBrowser의 IE 버전 설정하기
        /// </summary>
        /// <param name="key">
        /// 11001 (0x2AF9) - Internet Explorer 11. Webpages are displayed in IE11 edge mode, regardless of the !DOCTYPE directive.
        /// 11000 (0x2AF8) - Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 edge mode.Default value for IE11.
        /// 10001 (0x2711)- Internet Explorer 10. Webpages are displayed in IE10 Standards mode, regardless of the !DOCTYPE directive.
        /// 10000 (0x2710)- Internet Explorer 10. Webpages containing standards-based  !DOCTYPE directives are displayed in IE10 Standards mode.Default value for Internet Explorer 10.
        /// 9999 (0x270F) - Internet Explorer 9. Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.
        /// 9000 (0x2328) - Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.
        /// 8888 (0x22B8) - Webpages are displayed in IE8 Standards mode, regardless of the !DOCTYPE directive.
        /// 8000 (0x1F40) - Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.
        /// 7000 (0x1B58) - Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.
        /// </param>
        private void SetWebBrowserVersion(int key)
        {
            var appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";

            var Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);
            Key.SetValue(appName, key, RegistryValueKind.DWord);
            Key.Close();
        }
    }
}
