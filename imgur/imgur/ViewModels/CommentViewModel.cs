using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Imgur.Models;

namespace Imgur.ViewModels
{
    public class CommentViewModel: ViewModelBase
    {
        public CommentViewModel(Comment comment)
        {
            CommentData = comment;

        }

        private Comment _commentData;

        public Comment CommentData
        {
            get
            {
                return _commentData;
            }
            set
            {
                _commentData = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand ExpandCommand { get; set; }
        public RelayCommand UpvoteCommand { get; set; }
        public RelayCommand DownvoteCommand { get; set; }
        public RelayCommand ReplyCommand { get; set; }

    }
}
