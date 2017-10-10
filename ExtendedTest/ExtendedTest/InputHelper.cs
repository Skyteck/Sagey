using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    public static class InputHelper
    {
        static MouseState PrevMouseState;
        static MouseState CurrentMouseState;
        static KeyboardState PrevKBState;
        static KeyboardState CurrentKBState;

        static InputHelper()
        {
            PrevKBState = new KeyboardState();
            CurrentKBState = Keyboard.GetState();
            PrevMouseState = new MouseState();
            CurrentMouseState = Mouse.GetState();
        }

        public static void Update()
        {
            PrevKBState = CurrentKBState;
            PrevMouseState = CurrentMouseState;

            CurrentKBState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
        }


    }
}
