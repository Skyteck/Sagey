using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    public class WorldObject : Sprite
    {
        public int difficulty;
        public List<WorldObject> parentList;
        public double respawnTimerStart = 15d;
        public double timeDead = 0d;
        public List<Item.ItemType> OutputItems;
        public enum WorldObjectTag
        {
            kFireTag,
            kTreeTag,
            kRockTag,
            kFishingHoleTag,
            kNoneTag
        }

        public WorldObjectTag myWorldObjectTag;
        public WorldObject()
        {
        }


        public override void UpdateDead(GameTime gameTime)
        {
            base.UpdateDead(gameTime);
        }

        public void Update(GameTime gameTime)
        {

        }



        
    }
}
