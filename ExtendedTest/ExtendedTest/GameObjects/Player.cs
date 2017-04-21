using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class Player : Sprite
    {
        Vector2 Destination = Vector2.Zero;
        bool atDestination = true;
        bool movingX = false;
        bool movingY = false;
        MouseState previousMouseState;
        public List<Item> inventory;
        Object currentTarget = null;

        public enum CurrentAction
        {
            kActionWC,
            kActionMine,
            kActionNone
        }

        private CurrentAction action = CurrentAction.kActionNone;

        public Player()
        {
            inventory = new List<Item>();
        }

        public override void Update(GameTime gameTime, List<Sprite> gameObjectList)
        {
            Vector2 originalPos = _Position;
            movingX = false;
            movingY = false;
            handleInput(gameTime);
            var collidedWith = collisionCheck(gameObjectList);
            if (collidedWith != null)
            {
                _Position = originalPos;
                atDestination = true;
                if(collidedWith._Tag == SpriteType.kTreeType)
                {
                    action = CurrentAction.kActionWC;
                    currentTarget = (Tree)collidedWith;

                }

            }
            if(action == CurrentAction.kActionWC)
            {
                Chop(currentTarget as Tree);
            }
            foreach(Item item in inventory)
            {
                Console.Write(item._Name + " ");

            }
            Console.WriteLine("");
            base.Update(gameTime, gameObjectList);
        }

        private void handleInput(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouseState = Mouse.GetState();


            //if(mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed)
            //{
            //    Destination = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            //    atDestination = false;
            //    action = CurrentAction.kActionNone;
            //}

            if(!atDestination)
            {
                findPath();
            }

            #region Keyboard State
            //KeyboardState state = Keyboard.GetState();
            //if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            //{
            //    _Position.X -= maxSpeed;
            //}
            //else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            //{
            //    _Position.X += maxSpeed;
            //}
            //if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            //{
            //    _Position.Y -= maxSpeed;
            //}
            //else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            //{
            //    _Position.Y += maxSpeed;
            //}
            #endregion
            #region Gamepad state
            /* GamePad Stuff
            GamePadCapabilities cap = GamePad.GetCapabilities(PlayerIndex.One);

            if (cap.IsConnected && cap.HasLeftXThumbStick && cap.HasLeftYThumbStick && cap.HasRightXThumbStick && cap.HasRightYThumbStick)
            {
                GamePadState gpState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
                _Position.X += (maxSpeed * gpState.ThumbSticks.Left.X);
                _Position.Y += (maxSpeed * -gpState.ThumbSticks.Left.Y);
                if (gpState.ThumbSticks.Right.X == 0 && gpState.ThumbSticks.Right.Y == 0)
                {
                }
                
            } 
            */
            #endregion

            previousMouseState = mouseState;
            //LockInBounds();
        }

        public void setDestination(Vector2 dest)
        {
            Destination = dest;
            atDestination = false;
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

        private Sprite collisionCheck(List<Sprite> gameObjectList)
        {
            foreach (Sprite sprite in gameObjectList)
            {
                if (sprite._CurrentState == SpriteState.kStateActive)
                {
                    if (_BoundingBox.Intersects(sprite._BoundingBox))
                    {
                        if(movingX)
                        {
                            if((_BoundingBox.Right > sprite._BoundingBox.Left) && (_BoundingBox.Left < sprite._BoundingBox.Left))
                            {
                                Console.WriteLine("On player right?");
                            }
                            else if ((_BoundingBox.Left < sprite._BoundingBox.Right) && (_BoundingBox.Right > sprite._BoundingBox.Right))
                            {
                                Console.WriteLine("On player left?");
                            }
                        }

                        if(movingY)
                        {
                            if ((_BoundingBox.Bottom > sprite._BoundingBox.Top) && (_BoundingBox.Top < sprite._BoundingBox.Top))
                            {
                                Console.WriteLine("On player bottom?");
                            }
                            else if ((_BoundingBox.Top < sprite._BoundingBox.Bottom) && (_BoundingBox.Bottom > sprite._BoundingBox.Bottom))
                            {
                                Console.WriteLine("On player top?");
                            }
                        }

                        return sprite;
                    }
                }
            }
            return null;
        }

        private void Chop(Tree tree)
        {
            Item item = tree.getChopped();
            if(item != null)
            {
                inventory.Add(item);
            }
        }

        public void stopAction()
        {
            action = CurrentAction.kActionNone;
        }
    }
}
