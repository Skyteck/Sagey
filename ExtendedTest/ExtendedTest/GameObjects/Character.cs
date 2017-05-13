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
    public class Character : AnimatedSprite
    {

        Vector2 Destination;
        public bool atDestination = true;
        public bool movingX = false;
        public bool movingY = false;

        //Combat values
        public CombatManager _CBManager;
        public double attackSpeed = 3.0;
        public double attackCD = 0;
        public int startHP = 1;
        public int _HP;
        public int defense;
        public int attack;
        public float attackRange = 64f; // tileWidth

        public float _Speed = 5f;

        List<Tile> myPath;

        public Character(CombatManager cbManager)
        {
            _CBManager = cbManager;
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            SetupAnimation(1, 1, 1, false);
        }

        public override void UpdateActive(GameTime gameTime)
        {

            if (!atDestination)
            {
                findPath();
            }
            base.UpdateActive(gameTime);
        }

        public void SetPath(List<Tile> path)
        {
            myPath = path;
            setDestination(myPath[0].tileCenter);
        }

        public void findPath()
        {
            if (Math.Abs(Destination.X - _Position.X) > _Speed)
            {
                if (Destination.X > _Position.X)
                {
                    _Position.X += _Speed;
                    movingX = true;
                }
                else if (Destination.X < _Position.X)
                {
                    _Position.X -= _Speed;
                    movingX = true;
                }

            }

            if (Math.Abs(Destination.Y - _Position.Y) > _Speed)
            {
                if (Destination.Y > _Position.Y)
                {
                    _Position.Y += _Speed;
                    movingY = true;
                }
                else if (Destination.Y < _Position.Y)
                {
                    _Position.Y -= _Speed;
                    movingY = true;
                }
            }

            float distance = Vector2.Distance(Destination, _Position);
            if (distance <= _Speed + 1)
            {
                _Position = Destination;
                atDestination = true;
                if(myPath!= null)
                {
                    if (myPath.Count > 0)
                    {
                        setDestination(myPath[0].tileCenter);
                        myPath.RemoveAt(0);
                    }
                }

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
