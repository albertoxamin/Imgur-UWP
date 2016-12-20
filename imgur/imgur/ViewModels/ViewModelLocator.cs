using Imgur.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imgur.ViewModels
{
    public class ViewModelLocator
    {
        public static MainViewModel MainViewModel { get; set; } = new MainViewModel();

        //public static MainListViewModel MainListViewModel { get; set; } = new MainListViewModel();
    }
}
