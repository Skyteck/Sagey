namespace ExtendedTest.GameObjects.Items
{
    public class ItemBundle
    {
        public Item.ItemType output;
        public int odds;
        public int amount;

        public ItemBundle()
        {
            output = Item.ItemType.kItemNone;
        }
    }
}
