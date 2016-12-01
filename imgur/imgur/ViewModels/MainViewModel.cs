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

namespace Imgur.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ImgurGallerySection currentSection = ImgurGallerySection.Hot;
        private ImgurGallerySort currentSort = ImgurGallerySort.Viral;

        public ImgurGallerySection CurrentSection
        {
            get { return currentSection; }
            set
            {
                currentSection = value;
                RaisePropertyChanged();
            }
        }

        public ImgurGallerySort CurrentSort
        {
            get { return currentSort; }
            set
            {
                currentSort = value;
                RaisePropertyChanged();
            }
        }

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged();
            }
        }

        #region Sharing
        private RelayCommand shareCommand;
        public RelayCommand ShareCommand
        {
            get
            {
                return shareCommand ?? (shareCommand = new RelayCommand(ExecuteShareCommand));
            }
        }

        private void ExecuteShareCommand()
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
        #endregion

        #region Navigation

        private Page currentPage;
        public Page CurrentPage
        {
            get
            {
                return currentPage;
            }

            set
            {
                currentPage = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RelayCommand(ExecuteBackCommand));
            }
        }

        private void ExecuteBackCommand()
        {
            
        }

        #endregion

        public MainViewModel()
        {
            IsBusy = true;
        }
    }
}
