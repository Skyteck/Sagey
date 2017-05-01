using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    public class KbHandler
    {
        private List<Keys> lastPressedKeys;
        List<Keys> badKeys;
        public bool typingMode = false;
        private bool shiftHeld = false;

        public String Input = "";

        List<Keys> GoodKeys = new List<Keys>
        {
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
            Keys.K, Keys.L, Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
            Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z, Keys.D0, Keys.D1, Keys.D2, Keys.D3,
            Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.Space, Keys.NumPad0, Keys.NumPad1, Keys.NumPad2,
            Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9, Keys.Enter,
            Keys.OemQuestion
        };

        public KbHandler()
        {
            lastPressedKeys = new List<Keys>();
        }

        public void Update()
        {
            KeyboardState kbState = Keyboard.GetState();
            List<Keys> pressedKeys = kbState.GetPressedKeys().ToList<Keys>();

            if(!typingMode)
            {
                if(kbState.IsKeyDown(Keys.Enter) && !lastPressedKeys.Contains(Keys.Enter))
                {
                    typingMode = true;
                }
            }
            else if(typingMode)
            {
                if(pressedKeys.Count>=1)
                {
                    pressedKeys = pressedKeys.Intersect(GoodKeys).ToList();
                }
                //check if any of the previous update's keys are no longer pressed
                if (kbState.IsKeyDown(Keys.LeftShift) || kbState.IsKeyDown(Keys.RightShift))
                {
                    shiftHeld = true;
                }
                else
                {
                    shiftHeld = false;
                }

                foreach (Keys key in lastPressedKeys)
                {
                    if (!pressedKeys.Contains(key))
                        OnKeyUp(key);
                }

                //check if the currently pressed keys were already pressed
                foreach (Keys key in pressedKeys)
                {
                    if (!lastPressedKeys.Contains(key))
                        OnKeyDown(key);
                }
            }


            //save the currently pressed keys so we can compare on the next update
            lastPressedKeys = pressedKeys;
        }

        private void OnKeyDown(Keys key)
        {

            if(key == Keys.Enter)
            {
                if(typingMode)
                {
                    typingMode = false;
                }
            }
            else
            {
                if(key==Keys.Space)
                {
                    Input += " ";
                }
                else if (key == Keys.D0)
                {
                    if(shiftHeld)
                    {
                        Input += ")";
                    }
                    else
                    {
                        Input += "0";
                    }
                }
                else if (key == Keys.D1)
                {
                    if (shiftHeld)
                    {
                        Input += "!";
                    }
                    else
                    {
                        Input += "1";
                    }
                }
                else if (key == Keys.D2)
                {
                    if (shiftHeld)
                    {
                        Input += "@";
                    }
                    else
                    {
                        Input += "2";
                    }
                }
                else if (key == Keys.D3)
                {
                    if (shiftHeld)
                    {
                        Input += "#";
                    }
                    else
                    {
                        Input += "3";
                    }
                }
                else if (key == Keys.D4)
                {
                    if (shiftHeld)
                    {
                        Input += "$";
                    }
                    else
                    {
                        Input += "4";
                    }
                }
                else if (key == Keys.D5)
                {
                    if (shiftHeld)
                    {
                        Input += "%";
                    }
                    else
                    {
                        Input += "5";
                    }
                }
                else if (key == Keys.D6)
                {
                    if (shiftHeld)
                    {
                        Input += "^";
                    }
                    else
                    {
                        Input += "6";
                    }
                }
                else if (key == Keys.D7)
                {
                    if (shiftHeld)
                    {
                        Input += "&";
                    }
                    else
                    {
                        Input += "7";
                    }
                }
                else if (key == Keys.D8)
                {
                    if (shiftHeld)
                    {
                        Input += "*";
                    }
                    else
                    {
                        Input += "8";
                    }
                }
                else if (key == Keys.D9)
                {
                    if (shiftHeld)
                    {
                        Input += "(";
                    }
                    else
                    {
                        Input += "9";
                    }
                }
                else if(key==Keys.Back)
                {
                    if(Input.Length > 0)
                    {
                        Input = Input.Substring(0, Input.Length - 1);

                    }
                }
                else if(key==Keys.OemQuestion)
                {
                    if(shiftHeld)
                    {
                        Input += "?";
                    }
                    else
                    {
                        Input += "/";
                    }
                }
                else
                {
                    Input += key.ToString();
                }
            }
            //do stuff
        }

        private void OnKeyUp(Keys key)
        {
            //do stuff
        }
    }
}
