using Imgur.Models;
using System;
using System.Collections;
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

namespace Imgur
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainList : Page
    {
        public MainList()
        {
            this.InitializeComponent();
        }
        //MainPage parent;
        //List<ImgurImage> immagini = new List<ImgurImage>();
        //List<Topic> topics = new List<Topic>();
        //int page = 0;
        //protected override async void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    //if (e.NavigationMode != NavigationMode.Back)
        //    //{
        //    //var filtro = from a in data.Images
        //    //             where a.IsAlbum
        //    //             select a;
        //    await UpdateMainList(0,0);
        //    parent = e.Parameter as MainPage;
        //    //}
        //    //else
        //    //{
        //    if (last != null)
        //        mailList.ScrollIntoView(last);
        //    //}
        //    topics = await App.ServiceClient.GetTopics();
        //    topics.Insert(0, new Topic(immagini[0].ID,"Most Viral", "Today's most popular posts"));
        //    int i = 0;
        //    foreach (Topic t in topics)
        //    {
        //        t.index = i;
        //        if (i != 0)
        //        t.selected = Visibility.Collapsed;
        //        i++;
        //    }
        //    topicGrid.ItemsSource = topics;
        //    base.OnNavigatedTo(e);
        //}
        //bool open;
        //public void ShowTopicsGrid()
        //{
        //    if (open)
        //        HideTopics.Begin();
        //    else
        //        ShowTopics.Begin();
        //    open = !open;
        //}

        //public async System.Threading.Tasks.Task UpdateMainList(ImgurGallerySection section, ImgurGallerySort sort)
        //{
        //    ImgurImageData data = await App.ServiceClient.GetMainGalleryImages(section, sort, 0);
        //    if (Extensions.GetLocalSettingsValue<string>("nsfw") == "hide")
        //        data.Images.RemoveAll(x => x.nsfw);
        //    immagini = data.Images.ToList();
        //    foreach (ImgurImage ii in immagini)
        //    {
                
        //        ii.Width = Convert.ToInt16(Window.Current.Bounds.Width / 2);
        //        if (ii.Width > 200)
        //            ii.Width = 200;
        //    }
        //    mailList.ItemsSource = immagini;
        //}

        //public object last;
        //private void mailList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    last = sender;
        //    Frame.Navigate(typeof(PostDetail), new ArrayList() { immagini, mailList.SelectedIndex, parent });
        //}

        //private void mailList_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    VariableSizedWrapGrid appItemsPanel = (VariableSizedWrapGrid)(sender as GridView).ItemsPanelRoot;

        //    double optimizedWidth = 200;
        //    double margin = 0.0;
        //    var number = (int)e.NewSize.Width / (int)optimizedWidth;
        //    appItemsPanel.ItemWidth = (e.NewSize.Width - margin) / (double)number;
        //}

        //private void topicGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    ItemsWrapGrid appItemsPanel = (ItemsWrapGrid)(sender as GridView).ItemsPanelRoot;
        //    double optimizedWidth = 200;
        //    double margin = 0.0;
        //    var number = (int)e.NewSize.Width / (int)optimizedWidth;
        //    appItemsPanel.ItemWidth = (e.NewSize.Width - margin) / (double)number;
        //    appItemsPanel.ItemHeight = appItemsPanel.ItemWidth;

        //}

        //private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    Grid g = (sender as Grid);
        //    for (int i = 0; i < topics.Count; i++)
        //    {
        //        topics[i].selected = Visibility.Collapsed;
        //        if (int.Parse(g.Tag.ToString())==i)
        //            topics[i].selected = Visibility.Visible;
        //    }
        //    ShowTopicsGrid();
        //    topicGrid.ItemsSource = null;
        //    topicGrid.ItemsSource = topics;
        //}
    }
}
