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
using IndependentProject.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IndependentProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            this.InitializeComponent();
        }
        private ItemViewModel item;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            item = (ItemViewModel)e.Parameter;
            ItemName.Text = item.DisplayProperties.name;
            ItemDescription.Text = item.DisplayProperties.description;
            ItemTypeAndTierName.Text = item.ItemTypeAndTierDisplayName;
            if (item.ItemTypeAndTierDisplayName.Contains("Uncommon"))
            {
                ItemTypeAndTierName.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
            }else if (item.ItemTypeAndTierDisplayName.Contains("Rare"))
            {
                ItemTypeAndTierName.Foreground = new SolidColorBrush(Windows.UI.Colors.LightBlue);
            }else if (item.ItemTypeAndTierDisplayName.Contains("Legendary"))
            {
                ItemTypeAndTierName.Foreground = new SolidColorBrush(Windows.UI.Colors.Purple);
            }else if (item.ItemTypeAndTierDisplayName.Contains("Exotic"))
            {
                ItemTypeAndTierName.Foreground = new SolidColorBrush(Windows.UI.Colors.Gold);
            }
            else
            {
                ItemTypeAndTierName.Foreground = new SolidColorBrush(Windows.UI.Colors.Gray);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = FindParent<Frame>(this);
            frame.Navigate(typeof(ItemPage));
        }

        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }
    }
}
