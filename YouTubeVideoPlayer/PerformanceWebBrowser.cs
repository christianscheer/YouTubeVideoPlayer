using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;

namespace YouTubeVideoPlayer
{
    public class PerformanceWebBrowser : ContentControl, IDisposable
    {
        private readonly WebBrowser _wb = new WebBrowser();

        static PerformanceWebBrowser()
        {
            var exeFileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            var keys = new[] {
                @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_GPU_RENDERING"
            };
            keys
                .Select(k => Registry.CurrentUser.OpenSubKey(k, true))
                .ToList()
                .ForEach(sk => sk.SetValue(exeFileName, 1, RegistryValueKind.DWord));
        }

        public PerformanceWebBrowser()
        {
            Content = _wb;
        }

        public void Dispose()
        {
            _wb.Dispose();
        }

        public void NavigateToString(string text)
        {
            _wb.NavigateToString(text);
        }
    }
}
