using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Imgur.Models;

namespace Imgur.ViewModels
{
    public class PostDetailViewModel: ViewModelBase
    {
        public PostDetailViewModel(ImgurImage image)
        {
            PostData = image;
            UpvoteCommand = new RelayCommand(Upvote);
            DownvoteCommand = new RelayCommand(Downvote);
            FavoriteCommand = new RelayCommand(Favorite);
            DownloadCommentsCommand = new RelayCommand(DownloadComments, () => !seeAllComments);
        }

        private ImgurImage _postData;

        public ImgurImage PostData
        {
            get
            {
                return _postData;
            }
            set
            {
                _postData = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<AlbumImage> _postImages;

        public ObservableCollection<AlbumImage> PostImages
        {
            get
            {
                return _postImages;
            }
            set
            {
                _postImages = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Comment> _comments;

        public ObservableCollection<Comment> Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                RaisePropertyChanged();
                RaisePropertyChanged("LoadedComments");
            }
        }

        private bool seeAllComments = false;
        public ObservableCollection<Comment> LoadedComments
        {
            get
            {
                if (Comments != null)
                {
                    if (!seeAllComments)
                        return new ObservableCollection<Comment>(Comments.Take(5));
                    else
                        return Comments;
                }
                else
                {
                    return null;
                }
            }
        }

        public RelayCommand UpvoteCommand { get; set; }
        public RelayCommand DownvoteCommand { get; set; }
        public RelayCommand FavoriteCommand { get; set; }
        public RelayCommand DownloadCommentsCommand { get; set; }

        private bool loaded = false;
        public async void Load()
        {
            if (loaded)
                return;

            loaded = true;
            if (PostData.IsAlbum)
                PostImages = new ObservableCollection<AlbumImage>(await App.ServiceClient.GetAlbumImages(PostData.ID));
            else
                PostImages = new ObservableCollection<AlbumImage>() {new AlbumImage()
                {
                    animated = PostData.IsAnimated,
                    description = PostData.description,
                    link = PostData.link,
                    mp4 = PostData.mp4
                } };
            Comments = new ObservableCollection<Comment>(await App.ServiceClient.GetComments((PostData.IsAlbum) ? "album" : "gallery", PostData.ID));
        }

        public void Unload()
        {
            loaded = false;
            PostImages = null;
            Comments = null;
            GC.Collect();
        }

        async void DownloadComments()
        {
            seeAllComments = true;
            RaisePropertyChanged("LoadedComments");
        }

        void Upvote()
        {
            
        }

        void Downvote()
        {
            
        }

        void Favorite()
        {
            
        }
    }
}
