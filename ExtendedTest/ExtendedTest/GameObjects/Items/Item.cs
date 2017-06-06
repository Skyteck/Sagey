using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExtendedTest
{
    public class Item
    {
        public String _Name { get; set; }
        public double _Weight { get; set; }
        public int _SaleValue { get; set; }
        public Texture2D itemtexture { get; set; }
        public bool _Stackable { get => stackable; set => stackable = value; }
        public int _Uses { get => uses; set => uses = value; }
        public ItemType _Type { get => type; set => type = value; }

        private bool stackable = false;

        private int uses = 1;

        public enum ItemType
        {
            kItemLog = 1,
            kItemOre,
            kItemFish,
            kItemFishNet,
            kItemMatches,
            kItemFishStick,
            kItemNone,
            kItemError
        }

        private ItemType type = ItemType.kItemNone;


        public void Draw(SpriteBatch spritebatch, Vector2 Pos)
        {
            spritebatch.Draw(itemtexture, Pos, Color.White);
        }
    }
}
