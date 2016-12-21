using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Imgur.Models;
using Imgur.ViewModels;

namespace Imgur.ViewModels
{
    public class PostViewModel : ViewModelBase
    {
        private MainListViewModel parentViewModel;
        public MainListViewModel ParentViewModel
        {
            get
            {
                return parentViewModel;
            }

            set
            {
                parentViewModel = value;
                RaisePropertyChanged();
            }
        }

        public PostViewModel(MainListViewModel parentViewModel)
        {
            ParentViewModel = parentViewModel;
            BackCommand = new RelayCommand(ExecuteBackCommand);
            ShareCommand = new RelayCommand(ExecuteShareCommand);
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

        public ImgurImage SelectedPost
        {
            get
            {
                return ParentViewModel.SelectedPost;
            }
        }

        public RelayCommand BackCommand { get; private set; }

        private void ExecuteBackCommand()
        {
            ViewModelLocator.MainViewModel.NavigateBack();
        }

        public RelayCommand ShareCommand { get; private set; }

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
            request.Data.SetText(SelectedPost.Title + System.Environment.NewLine + SelectedPost.link);
            request.Data.SetBitmap(RandomAccessStreamReference.CreateFromUri(new Uri(SelectedPost.Thumbnail, UriKind.Absolute)));
        }
    }
}
