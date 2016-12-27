using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imgur.Models;
using GalaSoft.MvvmLight.Command;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Imgur.View;

namespace Imgur.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //private ImgurGallerySection _currentSection = ImgurGallerySection.Hot;
        //private ImgurGallerySort _currentSort = ImgurGallerySort.Viral;

        //public ImgurGallerySection CurrentSection
        //{
        //    get { return _currentSection; }
        //    set
        //    {
        //        _currentSection = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //public ImgurGallerySort CurrentSort
        //{
        //    get
        //    {
        //        return _currentSort;
        //    }
        //    set
        //    {
        //        _currentSort = value;
        //        RaisePropertyChanged();
        //    }
        //}

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

        

        //private Thickness _pageMargin;

        //public Thickness PageMargin
        //{
        //    get { return _pageMargin; }
        //    set
        //    {
        //        _pageMargin = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private Visibility _mainListCommandsVisibility = Visibility.Collapsed;
        //public Visibility MainListCommandsVisibility
        //{
        //    get
        //    {
        //        return _mainListCommandsVisibility;
        //    }
        //    set
        //    {
        //        _mainListCommandsVisibility = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private Visibility _postDetailCommandsVisibility = Visibility.Collapsed;
        //public Visibility PostDetailCommandsVisibility
        //{
        //    get
        //    {
        //        return _postDetailCommandsVisibility;
        //    }
        //    set
        //    {
        //        _postDetailCommandsVisibility = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //#region Navigation
        //private Page _currentPage;
        //public Page CurrentPage
        //{
        //    get
        //    {
        //        return _currentPage;
        //    }
        //    set
        //    {
        //        _currentPage = value;
        //        MainListCommandsVisibility = Visibility.Collapsed;
        //        PostDetailCommandsVisibility = Visibility.Collapsed;
        //        var pageType = _currentPage.GetType();
        //        if (pageType == typeof(MainList))
        //        {
        //            PageMargin = new Thickness(0, 60, 0, 0);
        //            MainListCommandsVisibility = Visibility.Visible;
        //        }
        //        else if (pageType == typeof(PostDetail))
        //        {
        //            PageMargin = new Thickness(0, 0, 0, 0);
        //            PostDetailCommandsVisibility = Visibility.Visible;
        //        }
        //        else if (pageType == typeof(UserProfile))
        //        {
        //            PageMargin = new Thickness(0, 0, 0, 0);
        //        }
        //        else if (pageType == typeof(Purity.NsfwContent))
        //        {
        //            PageMargin = new Thickness(0, 0, 0, 0);
        //        }
        //        RaisePropertyChanged();
        //    }
        //}

        //private RelayCommand _backCommand;
        //public RelayCommand BackCommand => _backCommand ?? (_backCommand = new RelayCommand(ExecuteBackCommand));

        //private void ExecuteBackCommand()
        //{
            
        //}

        //#endregion

        private ObservableCollection<Tab> _tabs;
        public ObservableCollection<Tab> Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                RaisePropertyChanged();
            }
        }

        private Tab _selectedTab;
        public Tab SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                CurrentPage = SelectedTab.CurrentPage();
                RaisePropertyChanged();
            }
        }

        private Page _currentPage;

        public Page CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (value != CurrentPage) { 
                    _currentPage = value;
                    SelectedTab.NavigationStack.Push(_currentPage);
                    RaisePropertyChanged();
                }
            }
        }

        public void NavigateBack()
        {
            CurrentPage = SelectedTab.NavigateBack();
        }

        public bool CanGoBack()
        {
            return SelectedTab.CanGoBack();
        }


        public MainViewModel()
        {
            IsBusy = true;
            //CurrentPage = new MainList();
            Tabs = new ObservableCollection<Tab>();
            Tabs.Add(new Tab() { Title = "Home", Icon = "" , Page = new MainList()});
            Tabs.Add(new Tab() { Title = "Messages", Icon = "", Page = new ChatsView()});
            Tabs.Add(new Tab() { Title = "Camera", Icon = "" });
            Tabs.Add(new Tab() { Title = "Notifications", Icon = "", Page = new NotificationsView()});
            Tabs.Add(new Tab() { Title = "Profile", Icon = "", Page = new UserProfile()});
            SelectedTab = Tabs[0];
        }



    }
    public class Tab
    {
        public string Title
        {
            get; set;
        }
        public string Icon
        {
            get; set;
        }
        public Page Page
        {
            get; set;
        }

        public Page NavigateBack()
        {
            if (NavigationStack.Count > 0)
                NavigationStack.Pop();
            return CurrentPage();
        }

        public bool CanGoBack()
        {
            return (NavigationStack.Count == 0)?((CurrentPage() != Page)):true;
        }

        public Page CurrentPage()
        {
            if (NavigationStack.Count > 0)
                return NavigationStack.Peek();
            else
                return Page;
        }

        public Stack<Page> NavigationStack = new Stack<Page>();
    }
}
