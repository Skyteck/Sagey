using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest
{
    class Character : Sprite
    {
        double leftBoundary;
        double rightBoundary;
        double bottomBoundary;
        double topBoundary;
        
        double currentMoveTimer = 6f;
        Vector2 Destination;
        bool atDestination = true;
        bool movingX = false;
        bool movingY = false;

        bool Agressive = false;
        Rectangle huntZone;

        Sprite target;
        public Character(double lX, double rx, double by, double ty)
        {
            leftBoundary = lX;
            rightBoundary = rx;
            bottomBoundary = by;
            topBoundary = ty;
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            this.rightBoundary += this._Position.X;
            this.rightBoundary -= this._Texture.Width;
            this.bottomBoundary += this._Position.Y;
            this.bottomBoundary -= this._Texture.Height;
            this.leftBoundary += this._Texture.Width;
            this.topBoundary += this._Texture.Height;
        }

        public override void Update(GameTime gameTime, List<Sprite> gameObjectList)
        {
            
            if(this.Agressive && this.target._BoundingBox.Intersects(huntZone))
            {
                HuntPlayer();
                //if(this._BoundingBox.Intersects(target._BoundingBox))
                //{
                //    target.ReceiveDamage(1);
                //}
            }
            else
            {
                Patrol(gameTime);
            }


            if (!atDestination)
            {
                findPath();
            }
            base.Update(gameTime, gameObjectList);
        }

        public void SetTarget(Sprite target)
        {
            this.target = target;
            this.Agressive = true;
            huntZone = new Rectangle((int)this.leftBoundary, (int)this.topBoundary, (int)(this.rightBoundary-this.leftBoundary), (int)(this.bottomBoundary-this.topBoundary));
        }

        private void Patrol(GameTime gameTime)
        {
            currentMoveTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (currentMoveTimer <= 0)
            {
                Random num = new Random((int)this._Position.X);
                bool move = (num.Next() % 2 == 0) ? true : false;
                if (move)
                {
                    float newX = num.Next((int)this.leftBoundary, (int)this.rightBoundary);
                    float newY = num.Next((int)this.topBoundary, (int)this.bottomBoundary);
                    this.setDestination(new Vector2(newX, newY));
                }

                currentMoveTimer += num.Next(0, 6);
            }

        }

        private void HuntPlayer()
        {
            this.setDestination(this.target._Position);
        }

        private void findPath()
        {
            float maxSpeed = 5f;
            if (Math.Abs(Destination.X - _Position.X) > maxSpeed)
            {
                if (Destination.X > _Position.X)
                {
                    _Position.X += maxSpeed;
                    movingX = true;
                }
                else if (Destination.X < _Position.X)
                {
                    _Position.X -= maxSpeed;
                    movingX = true;
                }

            }

            if (Math.Abs(Destination.Y - _Position.Y) > maxSpeed)
            {
                if (Destination.Y > _Position.Y)
                {
                    _Position.Y += maxSpeed;
                    movingY = true;
                }
                else if (Destination.Y < _Position.Y)
                {
                    _Position.Y -= maxSpeed;
                    movingY = true;
                }
            }


            if (Vector2.Distance(Destination, _Position) <= maxSpeed)
            {
                //_Position = Destination;
                atDestination = true;
            }
        }

        public void setDestination(Vector2 dest)
        {
            Destination = dest;
            atDestination = false;
        }
    }
}
