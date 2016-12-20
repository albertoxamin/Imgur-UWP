using Imgur.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Imgur
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PostDetail : Page
    {
        public PostDetail()
        {
            this.InitializeComponent();
            try
            {
                var platformFamily = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;
                if (platformFamily != "Windows.Desktop")
                    HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
            catch { }
        }
        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            try
            {
                Frame.GoBack();
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
                e.Handled = true;
            }
            catch { }
        }
        List<ImgurImage> immagini;
        ObservableCollection<ImgurImage> loadedImages = new ObservableCollection<ImgurImage>();
        int selected;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            progressRing.IsActive = true;
            ArrayList img = e.Parameter as ArrayList;
            immagini = img[0] as List<ImgurImage>;
            selected = Convert.ToInt32(img[1]);
            foreach (ImgurImage i in immagini)
            {
                //i.mainPage = img[2] as MainPage;
                //i.Frame = Frame;
            }
            flipView.ItemsSource = immagini;
            //UpdateImages(0);
            flipView.SelectedIndex = selected;
            base.OnNavigatedTo(e);
            await Task.Delay(2000);
            progressRing.IsActive = false;

        }

        void UpdateImages(int skip)
        {
            IsEditingViews = true;
            selected += skip;
            if (selected < 0)
                selected = 0;
            if (loadedImages.Count == 3)
            {
                if (skip > 0)
                {
                    //loadedImages.Remove(loadedImages[0]);
                    if (!loadedImages.Contains(immagini[selected + 2]))
                        loadedImages.Add(immagini[selected + 2]);
                }
                else if (skip < 0)
                {
                    //loadedImages.Remove(loadedImages[2]);
                    if (!loadedImages.Contains(immagini[selected - 2]))
                        loadedImages.Add(immagini[selected - 2]);
                }
            }
            else
            {
                if (selected > 0)
                    loadedImages.Add(immagini[selected - 1]);
                loadedImages.Add(immagini[selected]);
                loadedImages.Add(immagini[selected + 1]);
            }
            IsEditingViews = false;

        }
        bool IsEditingViews = false;
        private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                (flipView.SelectedItem as ImgurImage).Load();
            }
            catch { }
            //try {
            //    //flipView.UpdateLayout();
            //    var container = this.flipView.ContainerFromIndex(flipView.SelectedIndex);
            //    (container as ImageImgurXAML).Load();
            //}
            //catch { }
            upvoted = false;
            //UpvoteAnimation.Stop();
            //FavAnimation.Stop();
        }
        bool upvoted = false;
        private void UpvoteTap(object sender, TappedRoutedEventArgs e)
        {
            //if (!upvoted)
            //    UpvoteAnimation.Begin();
            //else
            //    UpvoteAnimation.Stop();
            //upvoted = !upvoted;
        }
        bool fav = false;
        private void FavTap(object sender, TappedRoutedEventArgs e)
        {
            //if (!fav)
            //{
            //    FavAnimation.Begin();
            //    if (!upvoted)
            //    {
            //        UpvoteAnimation.Begin();
            //        upvoted = true;
            //    }

            //}
            //else
            //    FavAnimation.Stop();
            //fav = !fav;
        }
    }
}