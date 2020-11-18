using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndependentProject.Models;

namespace IndependentProject.ViewModels
{
    [ImplementPropertyChanged]
    public class ItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        public DestinyInventoryItem.DisplayProperties DisplayProperties { get; set; }
        public string SecondaryIcon { get; set; }
        public string SecondaryOverlay { get; set; }
        public string SecondarySpecial { get; set; }
        public string Screenshot { get; set; }
        public string ItemTypeDisplayName { get; set; }
        public string UiItemDisplayStyle { get; set; }
        public string ItemTypeAndTierDisplayName { get; set; }
        public string DisplaySource { get; set; }
        public List<object> InvestmentStats { get; set; }
        public List<object> Perks { get; set; }
        public double? SpecialItemType { get; set; }
        public double? ItemType { get; set; }
        public double? ItemSubType { get; set; }
        public double? ClassType { get; set; }
        public bool Equippable { get; set; }
        public double? DefaultDamageType { get; set; }
        public long Hash { get; set; }
        public double? Index { get; set; }
        public bool Redacted { get; set; }
        public bool Blacklisted { get; set; }
    }
}
