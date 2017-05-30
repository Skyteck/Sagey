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
        public List<WorldObject> parentList;
        public double respawnTimerStart = 15d;
        public double timeDead = 0d;

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
            if (_CurrentState == SpriteState.kStateInActive)
            {
                timeDead += gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timeDead >= respawnTimerStart)
            {
                Revive();
            }
            base.UpdateDead(gameTime);
        }

        public void Update(GameTime gameTime)
        {

        }

        public virtual void Revive()
        {
            this._CurrentState = SpriteState.kStateActive;
            this._Draw = true;
            timeDead = 0;
        }
        
    }
}
