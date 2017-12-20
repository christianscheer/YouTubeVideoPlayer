using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace YouTubeVideoPlayer
{
    public class YouTubeVideoPlayer : ContentControl
    {
        private static string _html;
        private PerformanceWebBrowser _pwb = new PerformanceWebBrowser();

        #region DependencyProperty
        public string VideoId
        {
            get { return (string)GetValue(VideoIdProperty); }
            set { SetValue(VideoIdProperty, value); }
        }

        public static readonly DependencyProperty VideoIdProperty =
            DependencyProperty.Register("VideoId", typeof(string), typeof(YouTubeVideoPlayer), new PropertyMetadata(null, VideoIdChanged));

        private static void VideoIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((YouTubeVideoPlayer)d).SetVideoId((string)e.NewValue);
        }
        #endregion DependencyProperty

        public YouTubeVideoPlayer()
        {
            Content = _pwb;
        }

        public void SetVideoId(string videoId)
        {
            if (!string.IsNullOrWhiteSpace(videoId))
            {
                var html = GetHtml().Replace("{ytid}", videoId);
                _pwb.NavigateToString(html);
            }
        }

        private static string GetHtml()
        {
            if (_html == null)
            {
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("YouTubeVideoPlayer.Template.html"))
                using (var sr = new StreamReader(stream))
                {
                    _html = sr.ReadToEnd();
                }
            }
            return _html;
        }
    }
}
