﻿using imgur.ImgurAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace imgur
{
    public sealed partial class CommentControl : UserControl
    {
        public CommentControl()
        {
            this.InitializeComponent();
        }

        private void ShowReplies(object sender, PointerRoutedEventArgs e)
        {
            if (replies.Visibility == Visibility.Collapsed)
                replies.Visibility = Visibility.Visible;
            else
                replies.Visibility = Visibility.Collapsed;

            string stringContainingLinks = (DataContext as Comment).comment;
            foreach (Comment c in (DataContext as Comment).children)
            {
                c.mainPage = (DataContext as Comment).mainPage;
            }
        }

        private void repliesTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            int rc = (DataContext as Comment).repliesCount;
            if (rc == 1)
                repliesTextBlock.Text = "reply";
        }
        string link = "";
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string cap = (DataContext as Comment).comment;
            foreach (string s in cap.Split(' '))
            {
                if (!s.Contains("http://") && !s.Contains("https://"))
                    caption.Inlines.Add(new Run { Text = s + " " });
                else
                {
                    Hyperlink hyperLink = new Hyperlink();
                    link = s;
                    hyperLink.Click += OpenLink;
                    hyperLink.Inlines.Add(new Run { Text = s + " "});
                    caption.Inlines.Add(hyperLink);
                }
            }
        }

        private async void OpenLink(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            try {
                (DataContext as Comment).mainPage.OpenLink(link);
            }
            catch { }
        }
    }
}
