using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ExtendedTest.GameObjects
{
    class Character : Sprite
    {
        double leftBoundary;
        double rightBoundary;
        double bottomBoundary;
        double topBoundary;

        float moveTimer = 600f; //frames on average before moving

        Vector2 Destination;
        bool atDestination = true;
        bool movingX = false;
        bool movingY = false;
        public Character(float lX, float rx, float by, float ty)
        {
            leftBoundary = lX;
            rightBoundary = rx;
            bottomBoundary = by;
            topBoundary = ty;
        }

        public override void Update(GameTime gameTime, List<Sprite> gameObjectList)
        {
            findPath();
            base.Update(gameTime, gameObjectList);
        }

        private void findPath()
        {
            float maxSpeed = 5f;
            if (Math.Abs(Destination.X - _Position.X) > 5)
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

            if (Math.Abs(Destination.Y - _Position.Y) > 5)
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


            if (Vector2.Distance(Destination, _Position) <= 5)
            {
                //_Position = Destination;
                atDestination = true;
            }
        }
    }
}
