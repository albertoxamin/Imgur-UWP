using Imgur.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;

namespace Imgur.ViewModels
{
    public class ViewModelLocator
    {
        public static MainViewModel MainViewModel { get; set; } = new MainViewModel();
        public static PostViewModel PostViewModel { get; set; }

    }
}
