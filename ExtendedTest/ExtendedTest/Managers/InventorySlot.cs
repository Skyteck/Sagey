using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    public class InventorySlot
    {
        private Item itemInSlot;
        private int amount;
        public Vector2 _Position;
        public Texture2D activeBG;
        public Rectangle myRect
        {
            get
            {
                return new Rectangle((int)_Position.X-8, (int)_Position.Y-8, 32, 32);
            }
        }

        public Item ItemInSlot { get => itemInSlot; set => itemInSlot = value; }
        public int Amount { get => amount; set => amount = value; }

        public InventorySlot()
        {

        }
    }
}
