using IndependentProject.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static IndependentProject.Models.DestinyInventoryItem;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IndependentProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ItemPage : Page
    {
        public ItemPageViewModel ViewModel { get; set; } = new ItemPageViewModel();
        private ObservableCollection<ItemViewModel> itemsArr;
        private ObservableCollection<ItemViewModel> sortedArr;
        private int itemsIndex = 0;
        private int lastHeuristic = -1;
        private int allDataCount = 0;
        private bool sentinel = true;


        public ItemPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (sentinel)
            {
                while (ViewModel.Items.Count > 0)
                {
                    ViewModel.Items.RemoveAt(0);
                }
                for (int i = 0; i < 45; i++)
                {
                    ViewModel.Items.Add(new ItemViewModel());
                    ItemViewModel curr = ViewModel.Items[i];
                    curr.DisplayProperties = new DisplayProperties
                    {
                        icon = "https://upload.wikimedia.org/wikipedia/commons/3/3a/Gray_circles_rotate.gif",
                        name = "Loading..."
                    };
                }
                await UpdateItems();
                SortItems(0);
                sentinel = false;
            }
            
        }

        private async Task UpdateItems()
        {
            await DatabaseReader.GetManifest();
            DatabaseReader.InitializeDatabase();
            Dictionary<string, Dictionary<string, string>> allData = DatabaseReader.GetData();
            ObservableCollection<ItemViewModel> newItems = new ObservableCollection<ItemViewModel>();
            int count = 0;
            for (int i = 0; i < allData["DestinyInventoryItemDefinition"].Count; i++)
            {
                KeyValuePair<string, string> item = allData["DestinyInventoryItemDefinition"].ElementAt<KeyValuePair<string, string>>(i);
                DestinyInventoryItemRootObject itemModel = JsonConvert.DeserializeObject<DestinyInventoryItemRootObject>(item.Value);
                if (itemModel.equippable && !itemModel.redacted && !itemModel.displayProperties.name.Equals(""))
                {
                    newItems.Add(new ItemViewModel());
                    newItems[count].DisplayProperties = new DisplayProperties
                    {
                        name = itemModel.displayProperties.name,
                        icon = "https://www.bungie.net" + itemModel.displayProperties.icon,
                        hasIcon = itemModel.displayProperties.hasIcon,
                        description = itemModel.displayProperties.description
                    };
                    newItems[count].SecondaryIcon = itemModel.secondaryIcon;
                    newItems[count].SecondaryOverlay = itemModel.secondaryOverlay;
                    newItems[count].SecondarySpecial = itemModel.secondarySpecial;
                    newItems[count].Screenshot = "https://www.bungie.net" + itemModel.screenshot;
                    newItems[count].ItemTypeDisplayName = itemModel.itemTypeDisplayName;
                    newItems[count].UiItemDisplayStyle = itemModel.uiItemDisplayStyle;
                    newItems[count].ItemTypeAndTierDisplayName = itemModel.itemTypeAndTierDisplayName;
                    newItems[count].DisplaySource = itemModel.displaySource;
                    newItems[count].InvestmentStats = itemModel.investmentStats;
                    newItems[count].Perks = itemModel.perks;
                    newItems[count].SpecialItemType = itemModel.specialItemType;
                    newItems[count].ItemType = itemModel.itemType;
                    newItems[count].ItemSubType = itemModel.itemSubType;
                    newItems[count].ClassType = itemModel.classType;
                    newItems[count].Equippable = itemModel.equippable;
                    newItems[count].DefaultDamageType = itemModel.defaultDamageType;
                    newItems[count].Hash = itemModel.hash;
                    newItems[count].Index = itemModel.index;
                    newItems[count].Redacted = itemModel.redacted;
                    newItems[count].Blacklisted = itemModel.blacklisted;
                    count++;
                }
            }
            allDataCount = newItems.Count;
            itemsArr = newItems;
            string a = "lol";
        }

        private void RepopulateItems()
        {
            int temp = 0;
            int index = itemsIndex;
            while (temp < 45)
            {
                ItemViewModel itemModel;
                if (index + temp < allDataCount)
                {
                    itemModel = sortedArr.ElementAt(index + temp);
                }
                else
                {
                    itemModel = new ItemViewModel
                    {
                        DisplayProperties = new DisplayProperties()
                    };
                    itemModel.DisplayProperties.icon = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRDrP10CXh1Gkh0DVi0fWEUSdpLc4rpXqO_E9evEBsWTyfWLKa_";
                }
                ViewModel.Items[temp].DisplayProperties.name = itemModel.DisplayProperties.name;
                ViewModel.Items[temp].DisplayProperties.icon = itemModel.DisplayProperties.icon;
                ViewModel.Items[temp].DisplayProperties.hasIcon = itemModel.DisplayProperties.hasIcon;
                ViewModel.Items[temp].DisplayProperties.description = itemModel.DisplayProperties.description;
                ViewModel.Items[temp].SecondaryIcon = itemModel.SecondaryIcon;
                ViewModel.Items[temp].SecondaryOverlay = itemModel.SecondaryOverlay;
                ViewModel.Items[temp].SecondarySpecial = itemModel.SecondarySpecial;
                if (itemModel.Screenshot != null && itemModel.Screenshot.Equals("https://www.bungie.net"))
                {
                    ViewModel.Items[temp].Screenshot = itemModel.DisplayProperties.icon;
                }
                else
                {
                    ViewModel.Items[temp].Screenshot = itemModel.Screenshot;
                }
                ViewModel.Items[temp].ItemTypeDisplayName = itemModel.ItemTypeDisplayName;
                ViewModel.Items[temp].UiItemDisplayStyle = itemModel.UiItemDisplayStyle;
                ViewModel.Items[temp].ItemTypeAndTierDisplayName = itemModel.ItemTypeAndTierDisplayName;
                ViewModel.Items[temp].DisplaySource = itemModel.DisplaySource;
                ViewModel.Items[temp].InvestmentStats = itemModel.InvestmentStats;
                ViewModel.Items[temp].Perks = itemModel.Perks;
                ViewModel.Items[temp].SpecialItemType = itemModel.SpecialItemType;
                ViewModel.Items[temp].ItemType = itemModel.ItemType;
                ViewModel.Items[temp].ItemSubType = itemModel.ItemSubType;
                ViewModel.Items[temp].ClassType = itemModel.ClassType;
                ViewModel.Items[temp].Equippable = itemModel.Equippable;
                ViewModel.Items[temp].DefaultDamageType = itemModel.DefaultDamageType;
                ViewModel.Items[temp].Hash = itemModel.Hash;
                ViewModel.Items[temp].Index = itemModel.Index;
                ViewModel.Items[temp].Redacted = itemModel.Redacted;
                ViewModel.Items[temp].Blacklisted = itemModel.Blacklisted;
                temp++;
            }
            string a = "lol";
        }

        private void SortItems(int heuristic)
        {
            if (heuristic != lastHeuristic)
            {
                lastHeuristic = heuristic;
                itemsIndex = 0;
            }
            else
            {
                return;
            }
            ItemQuickSort quickSort = new ItemQuickSort(itemsArr, heuristic);
            sortedArr = quickSort.getSorted();
            RepopulateItems();
        }

        private void SortHash(object sender, RoutedEventArgs e)
        {
            SortItems(0);
        }

        private void SortName(object sender, RoutedEventArgs e)
        {
            SortItems(1);
        }

        private void SortRarity(object sender, RoutedEventArgs e)
        {
            SortItems(2);
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (itemsIndex >= 45)
            {
                itemsIndex -= 45;
            }
            else
            {
                return;
            }
            RepopulateItems();
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (itemsIndex + 45 > allDataCount)
            {
                return;
            }
            else
            {
                itemsIndex += 45;
            }
            RepopulateItems();
        }

        //FindParent<T> was taken from a stackoverflow page https://stackoverflow.com/questions/42173070/uwp-navigation-from-child-page-frame-to-main-page-frame
        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frame = FindParent<Frame>(this);
            ItemViewModel test = (ItemViewModel)e.ClickedItem;
            if(test.DisplayProperties.name == null)
            {
                return;
            }
            frame.Navigate(typeof(DetailsPage), e.ClickedItem);
        }
    }
}
