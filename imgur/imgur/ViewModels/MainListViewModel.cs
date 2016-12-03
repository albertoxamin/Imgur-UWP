using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imgur.Models;
using GalaSoft.MvvmLight.Command;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;

namespace Imgur.ViewModels
{
    public class MainListViewModel : ViewModelBase
    {
        public MainListViewModel()
        {
            Posts = new ObservableCollection<ImgurImage>(App.postsDataService.GetPosts());
        }

        private ObservableCollection<ImgurImage> posts;

        public ObservableCollection<ImgurImage> Posts
        {
            get
            {
                return posts;
            }
            set
            {
                posts = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Topic> topics;

        public ObservableCollection<Topic> Topics
        {
            get
            {
                return topics;
            }
            set
            {
                topics = value;
            }
        }

    }
}
