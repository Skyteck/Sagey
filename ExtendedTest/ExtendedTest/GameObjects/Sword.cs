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
        Player parentPlayer;

        int mode = 1;
        int damage = 1;

        public Vector2 _SwordTip
        {

            get
            {
                float x = _Position.X;
                float y = _Position.Y;
                Vector2 newPos = _Position;

                x = newPos.X;
                y = newPos.Y;
                int radias = frameHeight;
                double mathSin = Math.Sin(_Rotation);
                double mathCos = Math.Cos(_Rotation);
                newPos.X = (float)(x + (radias * mathSin));
                newPos.Y = (float)(y - (radias * mathCos));

                return newPos;
            }
        }
        public enum SwordPoint
        {
            kNorth, kSouth, kWest, kEast, kNone
        }

        private SwordPoint _Pointing = SwordPoint.kNone;

        public override Vector2 _Center
        {
            get
            {
                if (_Pointing != SwordPoint.kNone)
                {
                    return new Vector2(frameWidth / 2, frameHeight);
                }
                else
                {
                    return Vector2.Zero;
                }
            }
            //return _Center;
        }

        public override Rectangle _BoundingBox
        {
            get
            {
                return new Rectangle((int)_Position.X, ((int)_Position.Y), frameWidth, frameHeight);
            }
        }

        public Sword(Player p)
        {
            parentPlayer = p;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            if(_CurrentState == SpriteState.kStateActive)
            {
                if(mode == 1)
                {
                    this._Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 30;
                    TTL = 0.15f;
                }
                else if(mode == 2)
                {
                    this._Rotation -= (float)gameTime.ElapsedGameTime.TotalSeconds * 30;
                    TTL = 0.15f;
                }
                else if(mode == 3)
                {
                    this._Rotation = 0;
                    this._Rotation = parentPlayer._Rotation;
                    TTL = 0.42f;
                }
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                base.UpdateActive(gameTime);
                if(currentTime >= TTL)
                {
                    Deactivate();
                    currentTime = 0f;
                }
                this._Position = parentPlayer._SwordAnchor;

                CollisionCheck();
            }
        }
        
        private void CollisionCheck()
        {

        }

        public void Attack1(SwordPoint dir)
        {
            mode = 1;
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
            Activate();
        }

        public void Attack2(SwordPoint dir)
        {
            mode = 2;
            _Pointing = dir;
            if (dir == SwordPoint.kEast)
            {
                _Rotation = 4f;
            }
            else if (dir == SwordPoint.kSouth)
            {
                //_Rotation = (float)(90 * (Math.PI / 180));
                _Rotation = -0.8f;
            }
            else if (dir == SwordPoint.kWest)
            {
                _Rotation = 1f;
            }
            else if (dir == SwordPoint.kNorth)
            {
                _Rotation = 2.2f;
            }
            currentTime = 0f;
            Activate();
        }

        public void Attack3()
        {
            mode = 3;
            currentTime = 0f;
            Activate();
        }
    }
}
