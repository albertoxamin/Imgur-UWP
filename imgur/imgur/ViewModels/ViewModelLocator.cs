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
        private static MainViewModel mainViewModel = new MainViewModel();

        public static MainViewModel MainViewModel
        {
            get
            {
                return mainViewModel;
            }

            set
            {
                mainViewModel = value;
            }
        }
    }
}
