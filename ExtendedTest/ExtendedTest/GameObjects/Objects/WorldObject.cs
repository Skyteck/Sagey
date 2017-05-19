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
                this._CurrentState = SpriteState.kStateActive;
                this._Draw = true;
                timeDead = 0;
            }
            base.UpdateDead(gameTime);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
