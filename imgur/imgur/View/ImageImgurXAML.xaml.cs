using Imgur.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Imgur.ViewModels;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Imgur
{
    public sealed partial class ImageImgurXAML : UserControl
    {
        public ImageImgurXAML()
        {
            this.InitializeComponent();
            Loaded += UserControl_Loaded;
        }

        private PostDetailViewModel dataContext;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var img = DataContext as ImgurImage;
            if (img == null)
                return;
            dataContext = MainListViewModel.SharedInstance.PostDetailViewModels[img];
            DataContext = dataContext;
        }
        public async void Load()
        {
            loaded = true;
            //this.FindName("immagini");
            this.FindName("immaginiList");
            dataContext.PostData.description = System.Net.WebUtility.HtmlDecode(dataContext.PostData.description);
            //if (dataContext.PostData.AccountUrl.Length > 15)
            //    accountID.Text = dataContext.PostData.AccountUrl.Substring(0, 24) + "...";
            //scrollableArea.Margin = new Thickness(0, topPart.ActualHeight-60, 0, 60);
            await LoadComments();
        }

        async Task LoadComments()
        {
            List<Comment> iid = await App.ServiceClient.GetComments((dataContext.PostData.IsAlbum)?"album": "gallery", dataContext.PostData.ID);
            try
            {
                foreach (Comment c in iid)
                {
                    c.comment = System.Net.WebUtility.HtmlDecode(c.comment);
                    //c.mainPage = dataContext.PostData.mainPage;
                }
            }
            catch { }
            //commentsNoParent.Sort();
            commenti.Items.Clear();

            var dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
            int i = 0;
            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
              {
                  foreach (Comment c in iid)
                  {
                      if (i < 10)
                          commenti.Items.Add(c);
                      else
                          break;
                      i++;
                  }
               });
        }

        bool loaded = false;

        private void MediaElement_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MediaElement me = sender as MediaElement;
            me.AreTransportControlsEnabled = !me.AreTransportControlsEnabled;
        }
        #region Save or share Image/Video
        string current;
        private void Image_Holding(object sender, HoldingRoutedEventArgs e)
        {
            current = ((sender as Image).Source as BitmapImage).UriSource.AbsolutePath;
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }
        private void Image_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            current = ((sender as Image).Source as BitmapImage).UriSource.AbsolutePath;
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }
        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("http://i.imgur.com" + current);
            var picker = new FileSavePicker();

            // set appropriate file types
            picker.FileTypeChoices.Add(".jpg Image", new List<string> { ".jpg" });
            picker.DefaultFileExtension = ".jpg";

            var file = await picker.PickSaveFileAsync();
            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                var client = new HttpClient();
                var httpStream = await client.GetStreamAsync(uri);
                await httpStream.CopyToAsync(fileStream);
                fileStream.Dispose();
            }
        }
        private async void SaveAsVideoClick(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri(current);
            var picker = new FileSavePicker();

            // set appropriate file types
            picker.FileTypeChoices.Add(".mp4 Video", new List<string> { ".mp4" });
            picker.DefaultFileExtension = ".mp4";

            var file = await picker.PickSaveFileAsync();
            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                var client = new HttpClient();
                var httpStream = await client.GetStreamAsync(uri);
                await httpStream.CopyToAsync(fileStream);
                fileStream.Dispose();
            }
        }
        private void MediaElement_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            current = (sender as MediaElement).Source.AbsoluteUri.ToString();
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }
        private void MediaElement_Holding(object sender, HoldingRoutedEventArgs e)
        {
            current = (sender as MediaElement).Source.AbsoluteUri.ToString();
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }
        #endregion
        #region Scroll To Top Button
        private void ScrollToTop(object sender, TappedRoutedEventArgs e)
        {
            scrollViewer.ChangeView(null, 0, null, false);
        }
        void CheckIfHasToShowScrollToTopButton()
        {
            if (scrollViewer.VerticalOffset > 400 && dataContext.PostData.IsAlbum)
            {
                scrollToTopButton.Visibility = Visibility.Visible;
                scrollToTopButton.Opacity = scrollViewer.VerticalOffset/2400d;
            }
            else
                scrollToTopButton.Visibility = Visibility.Collapsed;

        }
        private void scrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            CheckIfHasToShowScrollToTopButton();
            if (scrollViewer.VerticalOffset < topPartFull.ActualHeight-60)
                topPart.Visibility = Visibility.Collapsed;
            else
                topPart.Visibility = Visibility.Visible;
        }
        #endregion

        private async void accountID_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //dataContext.PostData.mainPage.SetBusy(true);
            ImgurAccount.Account op = await App.ServiceClient.GetAccount(dataContext.PostData.AccountUrl);
            //dataContext.PostData.Frame.Navigate(typeof(UserProfile), op);
            //dataContext.PostData.mainPage.SetBusy(false);
        }

        private void UpvoteDoubleTap(object sender, DoubleTappedRoutedEventArgs e)
        {
            UpvoteAnimation.Begin();
        }

        private void Upvote(object sender, RoutedEventArgs e)
        {

        }

        private void Downvote(object sender, RoutedEventArgs e)
        {

        }

        private void Favorite(object sender, RoutedEventArgs e)
        {

        }

    }
}
