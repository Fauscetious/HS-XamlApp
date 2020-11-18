using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentProject.Models
{
    //Model was made based on json info provided by destiny.plumbing
    public class DestinyInventoryItem
    {
        [ImplementPropertyChanged]
        public class DisplayProperties : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public string description { get; set; }
            public string name { get; set; }
            public string icon { get; set; }
            public bool hasIcon { get; set; }
        }

        public class BackgroundColor
        {
            public double? colorHash { get; set; }
            public double? red { get; set; }
            public double? green { get; set; }
            public double? blue { get; set; }
            public double? alpha { get; set; }
        }

        public class Inventory
        {
            public double? maxStackSize { get; set; }
            public long bucketTypeHash { get; set; }
            public double? recoveryBucketTypeHash { get; set; }
            public double? tierTypeHash { get; set; }
            public bool isInstanceItem { get; set; }
            public bool nonTransferrableOriginal { get; set; }
            public string tierTypeName { get; set; }
            public double? tierType { get; set; }
            public string expirationTooltip { get; set; }
            public string expiredInActivityMessage { get; set; }
            public string expiredInOrbitMessage { get; set; }
            public bool suppressExpirationWhenObjectivesComplete { get; set; }
        }

        public class ItemList
        {
            public double? trackingValue { get; set; }
            public double? itemHash { get; set; }
        }

        public class SetData
        {
            public List<ItemList> itemList { get; set; }
            public double? trackingUnlockValueHash { get; set; }
            public double? abandonmentUnlockHash { get; set; }
            public bool requireOrderedSetItemAdd { get; set; }
            public bool setIsFeatured { get; set; }
            public string setType { get; set; }
        }

        public class Source
        {
            public double? level { get; set; }
            public double? minQuality { get; set; }
            public double? maxQuality { get; set; }
            public double? minLevelRequired { get; set; }
            public double? maxLevelRequired { get; set; }
            public double? exclusivity { get; set; }
            public Dictionary<object, object> computedStats { get; set; }
            public List<object> sourceHashes { get; set; }
        }

        public class SourceData
        {
            public List<object> sourceHashes { get; set; }
            public List<Source> sources { get; set; }
            public double? exclusive { get; set; }
            public List<object> vendorSources { get; set; }
        }

        public class DestinyInventoryItemRootObject
        {
            public DisplayProperties displayProperties { get; set; }
            public string secondaryIcon { get; set; }
            public string secondaryOverlay { get; set; }
            public string secondarySpecial { get; set; }
            public BackgroundColor backgroundColor { get; set; }
            public string screenshot { get; set; }
            public string itemTypeDisplayName { get; set; }
            public string uiItemDisplayStyle { get; set; }
            public string itemTypeAndTierDisplayName { get; set; }
            public string displaySource { get; set; }
            public Inventory inventory { get; set; }
            public SetData setData { get; set; }
            public SourceData sourceData { get; set; }
            public double? acquireRewardSiteHash { get; set; }
            public double? acquireUnlockHash { get; set; }
            public List<object> investmentStats { get; set; }
            public List<object> perks { get; set; }
            public bool allowActions { get; set; }
            public bool doesPostmasterPullHaveSideEffects { get; set; }
            public bool nonTransferrable { get; set; }
            public List<double?> itemCategoryHashes { get; set; }
            public double? specialItemType { get; set; }
            public double? itemType { get; set; }
            public double? itemSubType { get; set; }
            public double? classType { get; set; }
            public bool equippable { get; set; }
            public double? defaultDamageType { get; set; }
            public long hash { get; set; }
            public double? index { get; set; }
            public bool redacted { get; set; }
            public bool blacklisted { get; set; }
        }
    }
    
}
