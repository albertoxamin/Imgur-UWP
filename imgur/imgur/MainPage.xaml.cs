using imgur.ImgurAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace imgur
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// https://api.imgur.com/oauth2/authorize?client_id=YOUR_CLIENT_ID&response_type=REQUESTED_RESPONSE_TYPE&state=APPLICATION_STATE
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            string startURL = "https://api.imgur.com/oauth2/authorize?client_id="+API.Client_ID+"&response_type=token&state=login";
            string endURL = "https://xamin.it";

            System.Uri startURI = new System.Uri(startURL);
            System.Uri endURI = new System.Uri(endURL);

            string result;

            try
            {
                var webAuthenticationResult =
                    await Windows.Security.Authentication.Web.WebAuthenticationBroker.AuthenticateAsync(
                    Windows.Security.Authentication.Web.WebAuthenticationOptions.None,
                    startURI,
                    endURI);

                switch (webAuthenticationResult.ResponseStatus)
                {
                    case Windows.Security.Authentication.Web.WebAuthenticationStatus.Success:
                        // Successful authentication. 
                        result = webAuthenticationResult.ResponseData.ToString();
                        break;
                    case Windows.Security.Authentication.Web.WebAuthenticationStatus.ErrorHttp:
                        // HTTP error. 
                        result = webAuthenticationResult.ResponseErrorDetail.ToString();
                        break;
                    default:
                        // Other error.
                        result = webAuthenticationResult.ResponseData.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                // Authentication failed. Handle parameter, SSL/TLS, and Network Unavailable errors here. 
                result = ex.Message;
            }
            Windows.UI.Popups.MessageDialog md = new Windows.UI.Popups.MessageDialog(result);
            await md.ShowAsync();

            try {
                await StatusBar.GetForCurrentView().HideAsync();
            }
            catch { }
            frame.ContentTransitions = null;
            if (!Extensions.LocalSettingsExistsValue("nsfw"))
                frame.Navigate(typeof(Purity.NsfwContent), this);
            else
                frame.Navigate(typeof(MainList), this);
            base.OnNavigatedTo(e);
        }

        private void OpenBurger(object sender, TappedRoutedEventArgs e)
        {
            hamburger.IsPaneOpen = !hamburger.IsPaneOpen;
        }

        private async void ChangeSubReddit(object sender, RoutedEventArgs e)
        {
            categoryButton.Content = (sender as MenuFlyoutItem).Text;
            cSez = (ImgurGallerySection)int.Parse((sender as MenuFlyoutItem).Tag.ToString());
            await (frame.Content as MainList).UpdateMainList(cSez, cSort);
        }
        ImgurGallerySection cSez = ImgurGallerySection.Hot;
        ImgurGallerySort cSort = ImgurGallerySort.Viral;
        private async void ChangeSort(object sender, RoutedEventArgs e)
        {
            cSort = (ImgurGallerySort)int.Parse((sender as MenuFlyoutItem).Tag.ToString());
            await (frame.Content as MainList).UpdateMainList(cSez, cSort);
        }

        private void frame_Navigated(object sender, NavigationEventArgs e)
        {
            mainListCommands.Visibility = Visibility.Collapsed;
            postDetailCommands.Visibility = Visibility.Collapsed;
            frame.Margin = new Thickness(0, 60, 0, 0);
            if (e.SourcePageType == typeof(MainList))
            {
                mainListCommands.Visibility = Visibility.Visible;
            }
            else if (e.SourcePageType == typeof(PostDetail))
            {
                frame.Margin = new Thickness(0, 0, 0, 0);
                postDetailCommands.Visibility = Visibility.Visible;
            }
            else if (e.SourcePageType == typeof(UserProfile))
            {
                frame.Margin = new Thickness(0, 0, 0, 0);
            }
            else if (e.SourcePageType == typeof(Purity.NsfwContent))
            {
                frame.Margin = new Thickness(0, 0, 0, 0);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            frame.GoBack();
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.Properties.Title = "Share post";
            request.Data.Properties.Description = "You are going to share this post";
            request.Data.SetText("Hello world!");
        }

        public void OpenLink(string link)
        {
            openLink.Visibility = Visibility.Visible;
            webView.Navigate(new Uri(link));
            openLinkAnimation.Begin();
        }

        async void CloseLink()
        {
            closeLinkAnimation.Begin();
            webView.Navigate(new Uri("about:blank"));
            await System.Threading.Tasks.Task.Delay(1100);
            openLink.Visibility = Visibility.Collapsed;
        }

        private void CloseLink_Tap(object sender, TappedRoutedEventArgs e)
        {
            CloseLink();
        }

        private async void OpenWithEdge(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(webView.Source);
        }
        public void SetBusy(bool b)
        {
            progRing.IsActive = b;
        }

        private void categoryButton_Click(object sender, RoutedEventArgs e)
        {
            (frame.Content as MainList).ShowTopicsGrid();
        }
    }
}
