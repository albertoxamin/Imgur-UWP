using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Imgur.Controls
{
    public class MyGridView : GridView
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            dynamic model = item;
            try
            {
                element.SetValue(Windows.UI.Xaml.Controls.VariableSizedWrapGrid.ColumnSpanProperty, model.ColSpan);
                element.SetValue(Windows.UI.Xaml.Controls.VariableSizedWrapGrid.RowSpanProperty, model.RowSpan);
            }
            catch
            {
                element.SetValue(Windows.UI.Xaml.Controls.VariableSizedWrapGrid.ColumnSpanProperty, 1);
                element.SetValue(Windows.UI.Xaml.Controls.VariableSizedWrapGrid.RowSpanProperty, 1);
            }
            finally
            {
                element.SetValue(VerticalContentAlignmentProperty, VerticalAlignment.Stretch);
                element.SetValue(HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch);
                base.PrepareContainerForItemOverride(element, item);
            }
        }

        // refresh the variablesizedwrapgrid layout
        public void Update()
        {
            if (!(this.ItemsPanelRoot is VariableSizedWrapGrid)) 
                throw new ArgumentException("ItemsPanel is not VariableSizedWrapGrid");

            foreach (var container in this.ItemsPanelRoot.Children.Cast<GridViewItem>())
            {
                dynamic data = container.Content;
                VariableSizedWrapGrid.SetRowSpan(container, data.RowSpan);
                VariableSizedWrapGrid.SetColumnSpan(container, data.ColSpan);
            }
             
            this.ItemsPanelRoot.InvalidateMeasure();
        }
    }
}
