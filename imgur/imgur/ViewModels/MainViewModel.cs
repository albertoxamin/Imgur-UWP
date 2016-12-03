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

namespace Imgur.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ImgurGallerySection _currentSection = ImgurGallerySection.Hot;
        private ImgurGallerySort _currentSort = ImgurGallerySort.Viral;

        public ImgurGallerySection CurrentSection
        {
            get { return _currentSection; }
            set
            {
                _currentSection = value;
                RaisePropertyChanged();
            }
        }

        public ImgurGallerySort CurrentSort
        {
            get
            {
                return _currentSort;
            }
            set
            {
                _currentSort = value;
                RaisePropertyChanged();
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        #region Sharing
        private RelayCommand _shareCommand;
        public RelayCommand ShareCommand => _shareCommand ?? (_shareCommand = new RelayCommand(ExecuteShareCommand));

        private void ExecuteShareCommand()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
            DataTransferManager.ShowShareUI();
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.Properties.Title = "Share post";
            request.Data.Properties.Description = "You are going to share this post";
            request.Data.SetText("Hello world!");
        }
        #endregion

        private Thickness _pageMargin;

        public Thickness PageMargin
        {
            get { return _pageMargin; }
            set
            {
                _pageMargin = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _mainListCommandsVisibility = Visibility.Collapsed;
        public Visibility MainListCommandsVisibility
        {
            get
            {
                return _mainListCommandsVisibility;
            }
            set
            {
                _mainListCommandsVisibility = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _postDetailCommandsVisibility = Visibility.Collapsed;
        public Visibility PostDetailCommandsVisibility
        {
            get
            {
                return _postDetailCommandsVisibility;
            }
            set
            {
                _postDetailCommandsVisibility = value;
                RaisePropertyChanged();
            }
        }

        #region Navigation
        private Page _currentPage;
        public Page CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                MainListCommandsVisibility = Visibility.Collapsed;
                PostDetailCommandsVisibility = Visibility.Collapsed;
                var pageType = _currentPage.GetType();
                if (pageType == typeof(MainList))
                {
                    PageMargin = new Thickness(0, 60, 0, 0);
                    MainListCommandsVisibility = Visibility.Visible;
                }
                else if (pageType == typeof(PostDetail))
                {
                    PageMargin = new Thickness(0, 0, 0, 0);
                    PostDetailCommandsVisibility = Visibility.Visible;
                }
                else if (pageType == typeof(UserProfile))
                {
                    PageMargin = new Thickness(0, 0, 0, 0);
                }
                else if (pageType == typeof(Purity.NsfwContent))
                {
                    PageMargin = new Thickness(0, 0, 0, 0);
                }
                RaisePropertyChanged();
            }
        }

        private RelayCommand _backCommand;
        public RelayCommand BackCommand => _backCommand ?? (_backCommand = new RelayCommand(ExecuteBackCommand));

        private void ExecuteBackCommand()
        {
            
        }

        #endregion

        public MainViewModel()
        {
            IsBusy = true;
            //CurrentPage = new MainList();
        }
    }
}
