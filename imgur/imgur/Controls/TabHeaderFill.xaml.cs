using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Imgur.Controls
{
    public sealed partial class TabHeaderFill : UserControl
    {
        public TabHeaderFill()
        {
            this.InitializeComponent();
            DataContext = this;
        }
        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register("Glyph", typeof(string), typeof(TabHeaderFill), null);

        public string Glyph
        {
            get { return GetValue(GlyphProperty) as string; }
            set { SetValue(GlyphProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TabHeaderFill), null);

        public string Label
        {
            get { return GetValue(LabelProperty) as string; }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("FillPercent", typeof(double), typeof(TabHeaderFill), null);

        double w = 100;
        double h = 35;
        public double FillPercent
        {
            get
            {
                return fillPart.Rect.Width / w * 100d;
            }
            set
            {
                double perc = value;
                if (perc >= 0)
                {
                    if (perc > 100)
                        return;
                    fillPart.Rect = new Rect(0, 0, perc / 100 * w, h);
                    unfillPart.Rect = new Rect(perc / 100 * w, 0, w - (perc / 100 * w), h);
                }
                else
                {
                    perc *= -1;
                    if (perc > 100)
                        return;
                    unfillPart.Rect = new Rect(0, 0, perc / 100 * w, h);
                    fillPart.Rect = new Rect(perc / 100 * w, 0, w - (perc / 100 * w), h);
                }
            }
        }
    }
}
