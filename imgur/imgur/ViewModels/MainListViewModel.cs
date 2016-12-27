using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Imgur.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;

namespace Imgur.ViewModels
{
    public class MainListViewModel : ViewModelBase
    {
        public static MainListViewModel SharedInstance;
        public List<SortType> SortTypes { get; set; }
        public Dictionary<ImgurImage, PostDetailViewModel> PostDetailViewModels;

        private SortType _currentSort;

        public SortType CurrenSort
        {
            get { return _currentSort; }
            set
            {
                if (value != CurrenSort)
                {
                    _currentSort = value;
                    RaisePropertyChanged();
                    LoadData();
                }
            }
        }

        public MainListViewModel()
        {
            SharedInstance = this;
            Posts = new ObservableCollection<ImgurImage>();
            SortTypes = new List<SortType>();
            SortTypes.Add(new SortType() {Value = (int)ImgurGallerySort.Viral, Name = "Popular"});
            SortTypes.Add(new SortType() {Value = (int)ImgurGallerySort.Time, Name = "Newest"});
            CurrenSort = SortTypes[0];

            RefreshCommand = new RelayCommand(ExecuteRefreshCommand);
            SetPopularCommand = new RelayCommand(ExecuteSetPopularCommand);
            SetNewestCommand = new RelayCommand(ExecuteSetNewestCommand);
            LoadData();
        }

        private bool isSelectionChangedCooledDown;

        async void LoadData()
        {
            isSelectionChangedCooledDown = true;
            if (ViewModelLocator.MainViewModel != null)
                ViewModelLocator.MainViewModel.IsBusy = true;
            Posts.Clear();
            var data = await App.postsDataService.GetPosts((ImgurGallerySort)CurrenSort.Value);
            Posts = new ObservableCollection<ImgurImage>(data);
            if (ViewModelLocator.MainViewModel != null)
                ViewModelLocator.MainViewModel.IsBusy = false;
            PostDetailViewModels = new Dictionary<ImgurImage, PostDetailViewModel>();
            foreach (var image in Posts)
            {
                PostDetailViewModels[image] = new PostDetailViewModel(image);
            }
            await Task.Delay(200);
            isSelectionChangedCooledDown = false;
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
                if (ViewModelLocator.MainViewModel.CurrentPage.GetType() != typeof(PostDetail) && !isSelectionChangedCooledDown)
                {
                    ViewModelLocator.PostViewModel = new PostViewModel(this);
                    ViewModelLocator.MainViewModel.CurrentPage = new PostDetail();
                }
                if (SelectedPost != null) PostDetailViewModels[SelectedPost].Load();
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

        public RelayCommand SetPopularCommand { get; private set; }

        private void ExecuteSetPopularCommand()
        {
            CurrenSort = SortTypes[0];
        }

        public RelayCommand SetNewestCommand { get; private set; }

        private void ExecuteSetNewestCommand()
        {
            CurrenSort = SortTypes[1];
        }
    }


    public class SortType
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
