using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest
{
    /// <summary>
    /// A character is any object in the game world that can move around using its own logic. FindPath is provided to get the character where it's going.
    /// For example the player's destination is set by clicking on the world. NPCs use their logic to determien their destination
    /// findPath gets things where they need to be.
    /// </summary>
    class Character : Sprite
    {

        Vector2 Destination;
        bool atDestination = true;
        public bool movingX = false;
        public bool movingY = false;



        public double attackSpeed = 3.0;
        public double attackCD = 0;
        public int startHP = 1;
        public int _HP;

        public Character()
        {
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
        }

        public override void Update(GameTime gameTime, List<Sprite> gameObjectList)
        {

            if (!atDestination)
            {
                findPath();
            }
            base.Update(gameTime, gameObjectList);
        }

        public void findPath()
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

        public virtual void setDestination(Vector2 dest)
        {
            Destination = dest;
            atDestination = false;
        }

        public virtual void ReceiveDamage(int amt)
        {
            _HP -= amt;
            if (_HP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _CurrentState = SpriteState.kStateInActive;
            _Draw = false;
        }
    }
}
