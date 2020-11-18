using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndependentProject.ViewModels;

namespace IndependentProject
{
    //Quicksort algorithm in c# based on https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-9.php
    public class ItemQuickSort
    {
        public static int SortByHash = 0;
        public static int SortByName = 1;
        public static int SortByRarity = 2;

        public int currentHeuristic;
        public ItemViewModel[] arr;

        public ItemQuickSort(ObservableCollection<ItemViewModel> input, int heuristic)
        {
            currentHeuristic = heuristic;
            arr = input.ToArray();
            Sort(0, arr.Length - 1);
        }

        public long Compare(ItemViewModel dis, ItemViewModel dat)
        {
            if(currentHeuristic == SortByHash)
            {
                return dis.Hash - dat.Hash;
            }else if (currentHeuristic == SortByName)
            {
                return dis.DisplayProperties.name.ToLower().CompareTo(dat.DisplayProperties.name.ToLower());
            }else if (currentHeuristic == SortByRarity)
            {
                int disVal = Rarity(dis.ItemTypeAndTierDisplayName);
                int datVal = Rarity(dat.ItemTypeAndTierDisplayName);
                if(disVal == datVal)
                {
                    return dis.DisplayProperties.name.CompareTo(dat.DisplayProperties.name);
                }
                else
                {
                    return disVal - datVal;
                }
            }
            return 0;
        }

        public int Rarity(string input)
        {
            int rarity = 0;
            if (input.Contains("Exotic"))
            {
                rarity = 4;
            }
            else if (input.Contains("Legendary"))
            {
                rarity = 3;
            }
            else if (input.Contains("Rare"))
            {
                rarity = 2;
            }
            else if (input.Contains("Uncommon"))
            {
                rarity = 1;
            }
            return rarity;
        }

        private void Sort(int left, int right)
        {
            if(left < right)
            {
                int pivot = Partition(left, right);
                if(pivot > 1)
                {
                    Sort(left, pivot - 1);
                }
                if(pivot+1 < right)
                {
                    Sort(pivot + 1, right);
                }
            }
        }

        private int Partition(int left, int right)
        {
            ItemViewModel pivot = arr[left];
            while (true)
            {
                while(Compare(arr[left], pivot) < 0)
                {
                    left++;
                }
                while(Compare(arr[right], pivot) > 0)
                {
                    right--;
                }
                if(left < right)
                {
                    if(Compare(arr[left], arr[right]) == 0)
                    {
                        return right;
                    }
                    ItemViewModel temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }

        public ObservableCollection<ItemViewModel> getSorted()
        {
            ObservableCollection<ItemViewModel> returned = new ObservableCollection<ItemViewModel>();
            foreach(ItemViewModel item in arr)
            {
                returned.Add(item);
            }
            return returned;
        }
    }
}
