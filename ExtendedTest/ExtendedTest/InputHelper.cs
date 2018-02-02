using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey
{
    public static class InputHelper
    {
        static MouseState PrevMouseState;
        static MouseState CurrentMouseState;
        static KeyboardState PrevKBState;
        static KeyboardState CurrentKBState;
        //static Camera camera;
        public static void Init()
        {
            PrevKBState = new KeyboardState();
            CurrentKBState = Keyboard.GetState();
            PrevMouseState = new MouseState();
            CurrentMouseState = Mouse.GetState();
            //camera = c;
        }

        public static void Update()
        {
            PrevKBState = CurrentKBState;
            PrevMouseState = CurrentMouseState;

            CurrentKBState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
        }

        #region Keyboard Stuff
        public static bool IsKeyDown(Keys key)
        {
            if(CurrentKBState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }


        public static bool IsKeyPressed(Keys key)
        {
            if (CurrentKBState.IsKeyDown(key) && PrevKBState.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsKeyReleased(Keys key)
        {
            if (CurrentKBState.IsKeyUp(key) && PrevKBState.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsKeyHeld(Keys key)
        {
            if (CurrentKBState.IsKeyDown(key) && PrevKBState.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Mouse Stuff
        #region General mouse
        //public static Vector2 MouseWorldPos
        //{
        //    get
        //    {
        //        return camera.ToWorld(HelperFunctions.PointToVector(CurrentMouseState.Position));
        //    }
        //}

        //public static Vector2 PrevMouseWorldPos
        //{
        //    get
        //    {
        //        return camera.ToWorld(HelperFunctions.PointToVector(PrevMouseState.Position));
        //    }
        //}

        public static Vector2 MouseScreenPos
        {
            get
            {
                return HelperFunctions.PointToVector(CurrentMouseState.Position);
            }
        }

        public static Vector2 PrevMouseScreenPos
        {
            get
            {
                return HelperFunctions.PointToVector(PrevMouseState.Position);
            }
        }

        public static bool MouseScrolledUp
        {
            get
            {
                if (CurrentMouseState.ScrollWheelValue > PrevMouseState.ScrollWheelValue)
                {
                    return true;
                }
                return false;
            }

        }

        public static bool MouseScrolledDown
        {
            get
            {
                if (CurrentMouseState.ScrollWheelValue < PrevMouseState.ScrollWheelValue)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool MouseScrolled
        {
            get
            {
                if(MouseScrolledDown || MouseScrolledUp)
                {
                    return true;
                }
                return false;
            }
        }
#endregion
        #region Left Button

        public static bool LeftButtonDown
        {
            get
            {
                if (CurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool LeftButtonClicked
        {
            get
            {
                if (LeftButtonDown && PrevMouseState.LeftButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool LeftButtonUp
        {
            get
            {
                if (CurrentMouseState.LeftButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
            
        }

        public static bool LeftButtonHeld
        {
            get
            {
                if (LeftButtonDown && PrevMouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
            
        }

        public static bool LeftButtonReleased
        {
            get
            {
                if (LeftButtonUp && PrevMouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }

        }

        #endregion
        #region Right Button 
        public static bool RightButtonDown
        {
            get
            {
                if (CurrentMouseState.RightButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }

        }

        public static bool RightButtonClicked
        {
            get
            {
                if (RightButtonDown && PrevMouseState.RightButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool RightButtonUp
        {
            get
            {
                if (CurrentMouseState.RightButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool RightButtonHeld
        {
            get
            {
                if (RightButtonDown && PrevMouseState.RightButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool RightButtonReleased
        {
            get
            {
                if (RightButtonUp && PrevMouseState.RightButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }

        }

        #endregion
        #region Middle Mouse Button
        public static bool MiddleButtonDown
        {
            get
            {
                if (CurrentMouseState.MiddleButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }

        }

        public static bool MiddleButtonClicked
        {
            get
            {
                if (MiddleButtonDown && PrevMouseState.MiddleButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool MiddleButtonUp
        {
            get
            {
                if (CurrentMouseState.MiddleButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool MiddleButtonHeld
        {
            get
            {
                if (MiddleButtonDown && PrevMouseState.MiddleButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool MiddleButtonReleased
        {
            get
            {
                if (MiddleButtonUp && PrevMouseState.MiddleButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }

        }
#endregion


        #endregion
    }

}
