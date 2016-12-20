using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Imgur.Models;
using System.Collections.ObjectModel;

namespace Imgur.ViewModels
{
    public class MainListViewModel : ViewModelBase
    {
        public List<SortType> SortTypes { get; set; }
        private SortType _currentSort;

        public SortType CurrenSort
        {
            get { return _currentSort; }
            set { _currentSort = value; RaisePropertyChanged(); }
        }

        public MainListViewModel()
        {
            Posts = new ObservableCollection<ImgurImage>();
            SortTypes = new List<SortType>();
            SortTypes.Add(new SortType() {Value = (int)ImgurGallerySort.Viral, Name = "Popular"});
            SortTypes.Add(new SortType() {Value = (int)ImgurGallerySort.Time, Name = "Newest"});
            CurrenSort = SortTypes[0];
            LoadData();
        }

        async void LoadData()
        {
            var data = await App.postsDataService.GetPosts((ImgurGallerySort)CurrenSort.Value);
            Posts = new ObservableCollection<ImgurImage>(data);
        }

        private ObservableCollection<ImgurImage> _posts;

        public ObservableCollection<ImgurImage> Posts
        {
            get
            {
                return _posts;
            }
            set
            {
                _posts = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Topic> _topics = new ObservableCollection<Topic>();

        public ObservableCollection<Topic> Topics
        {
            get
            {
                return _topics;
            }
            set
            {
                _topics = value;
            }
        }
    }

    public class SortType
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
