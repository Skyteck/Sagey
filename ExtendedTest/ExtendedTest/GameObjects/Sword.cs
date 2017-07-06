using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ExtendedTest.GameObjects
{
    class Sword : Sprite
    {
        float TTL= 0.15f;
        float currentTime = 0f;
        public enum SwordPoint
        {
            kNorth, kSouth, kWest, kEast, kNone
        }

        private SwordPoint _Pointing = SwordPoint.kNone;

        public override Vector2 _Center
        {
            get
            {
                if(_Pointing != SwordPoint.kNone)
                {
                    return new Vector2(frameWidth/2, frameHeight);
                }
                else
                {
                    return Vector2.Zero;
                }
            }
        }

        public Sword()
        {
        }

        public override void UpdateActive(GameTime gameTime)
        {
            if(_CurrentState == SpriteState.kStateActive)
            {
                this._Rotation += (float) gameTime.ElapsedGameTime.TotalSeconds * 30;
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                base.UpdateActive(gameTime);
                if(currentTime >= TTL)
                {
                    Deactivate();
                    currentTime = 0f;
                }
            }

        }

        public void PointTo(Vector2 pos, SwordPoint dir)
        {
            _Pointing = dir;
            if(dir == SwordPoint.kEast)
            {
                _Rotation = -0.8f;
            }
            else if (dir == SwordPoint.kSouth)
            {
                //_Rotation = (float)(90 * (Math.PI / 180));
                _Rotation = 1f;
            }
            else if (dir == SwordPoint.kWest)
            {
                _Rotation = 2.2f;
            }
            else if (dir == SwordPoint.kNorth)
            {
                _Rotation = 4f;
            }
            currentTime = 0f;
            Activate(pos);
        }
    }
}
