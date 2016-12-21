using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Imgur.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System;

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

            RefreshCommand = new RelayCommand(ExecuteRefreshCommand);
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

        private ImgurImage _selectedPost;

        public ImgurImage SelectedPost
        {
            get
            {
                return _selectedPost;
            }
            set
            {
                _selectedPost = value;
                RaisePropertyChanged();
                if (ViewModelLocator.MainViewModel.CurrentPage.GetType() != typeof(PostDetail))
                {
                    ViewModelLocator.PostViewModel = new PostViewModel(this);
                    ViewModelLocator.MainViewModel.CurrentPage = new PostDetail();
                }
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

        public RelayCommand RefreshCommand { get; private set; }

        private void ExecuteRefreshCommand()
        {
            ImgurImage value = Posts[1];
            Posts[1] = Posts[5];
            Posts[5] = value;
        }

    }


    public class SortType
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
