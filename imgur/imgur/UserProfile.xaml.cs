using imgur.ImgurAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace imgur
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserProfile : Page
    {
        public UserProfile()
        {
            this.InitializeComponent();
            lowerPart.Height = Window.Current.Bounds.Height-110;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var account = (e.Parameter as ImgurAPI.ImgurAccount.Account);
            DataContext = account;
            if (e.NavigationMode != NavigationMode.Back)
            {
                ImgurAPI.ImgurAccount.AccountAwards aw = await App.ServiceClient.GetGalleryProfile(account.url);
                repResume.Text += " • " + aw.trophies.Count() + ((aw.trophies.Count() != 1) ? " Trophies" : "Trophy");

                List<Comment> userCom = await App.ServiceClient.GetAccountComments(account.url);
                comments.ItemsSource = userCom;
                if (userCom.Count != 0)
                    noComments.Visibility = Visibility.Collapsed;
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        #region Pivot header animation
        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewValue.Value = ((sender as Pivot).SelectedIndex + 1) * 100;
            Animation.Begin();
        }
        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            double v = e.NewValue;
            if (v <= 100)
            {
                tabh1.FillPercent = v;
                tabh2.FillPercent = .1;
                tabh3.FillPercent = .1;
            }
            else if (v > 100 && v <= 200)
            {
                tabh2.FillPercent = v - 100;
                tabh1.FillPercent = (v - 100) * -1;
                tabh3.FillPercent = .1;
            }
            else if (v > 200)
            {
                tabh3.FillPercent = v - 200;
                tabh2.FillPercent = (v - 200) * -1;
                tabh1.FillPercent = .1;
            }
        }
        #endregion

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            double vOff = (sender as ScrollViewer).VerticalOffset;
            topPart.Opacity = (100d-vOff)/100d;
            topPart.Margin = new Thickness(0, 20d + vOff / 1.5d, 0, 0);
            topUsername.Margin = new Thickness(60, -20 + vOff / 2.5d, 0, 0);
            topUsername.Opacity = 1d-(100d - vOff) / 100d;

        }
    }
}
