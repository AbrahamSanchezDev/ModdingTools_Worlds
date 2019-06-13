namespace Worlds.InventorySystem
{
    [System.Serializable]
    public class ItemBaseData : ObjecIdentifier
    {
        public int MaxStacks;
        public int BuyPrice;
        public ItemRarety Rarety;
        public ItemCategory MyItemCategory;
    }
}