using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    public class Player : Character
    {
        Vector2 Destination = Vector2.Zero;
        bool atDestination = true;
        InventoryManager invenManager;
        
        public enum CurrentAction
        {
            kActionWC,
            kActionMine,
            kActionNone
        }

        private CurrentAction action = CurrentAction.kActionNone;

        public Player(InventoryManager manager)
        {
            this.startHP = 10;
            _HP = 10;
            invenManager = manager;
            defense = 5;
            attack = 6;
            attackCD = 2;
            attackSpeed = 2;
            _Opacity = 0.5f;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            Vector2 originalPos = _Position;
            movingX = false;
            movingY = false;
            handleInput(gameTime);
            _Opacity = (float)_HP / 10;
            base.UpdateActive(gameTime);
        }

        private void handleInput(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(!atDestination)
            {
                base.findPath();
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

            //LockInBounds();
        }

        public override void setDestination(Vector2 dest)
        {
            base.setDestination(dest);
            action = CurrentAction.kActionNone;
        }

        private Sprite collisionCheck(List<Character> gameObjectList)
        {
            foreach (Character sprite in gameObjectList)
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
            if (item != null)
            {
                invenManager.AddItem(item);
                action = CurrentAction.kActionNone;
            }
        }
        private void Mine(Rock rock)
        {
            Item item = rock.getChopped();
            if (item != null)
            {
                invenManager.AddItem(item);
                action = CurrentAction.kActionNone;
            }
        }

        public void stopAction()
        {
            action = CurrentAction.kActionNone;
        }
    }
}
