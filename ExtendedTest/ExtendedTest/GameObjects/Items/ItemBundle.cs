namespace Sagey.GameObjects.Items
{
    public class ItemBundle
    {
        public Enums.ItemID outputID;
        public int odds;
        public int amount;

        public ItemBundle()
        {
            outputID = Enums.ItemID.kItemNone;
        }
    }
}
