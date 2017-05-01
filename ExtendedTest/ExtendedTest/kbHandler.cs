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
        private Keys[] lastPressedKeys;
        public bool typingMode = false;
        public String Input = "";
        public KbHandler()
        {
            lastPressedKeys = new Keys[0];
        }

        public void Update()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            if(!typingMode)
            {
                if(pressedKeys.Contains(Keys.OemTilde) && !lastPressedKeys.Contains(Keys.OemTilde))
                {
                    typingMode = true;
                }
            }
            else if(typingMode)
            {
                //check if any of the previous update's keys are no longer pressed
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
            if(key == Keys.OemTilde)
            {
                if(typingMode)
                {
                    typingMode = false;
                    Input = "";
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
                    Input += "0";
                }
                else if (key == Keys.D1)
                {
                    Input += "1";
                }
                else if (key == Keys.D2)
                {
                    Input += "2";
                }
                else if (key == Keys.D3)
                {
                    Input += "3";
                }
                else if (key == Keys.D4)
                {
                    Input += "4";
                }
                else if (key == Keys.D5)
                {
                    Input += "5";
                }
                else if (key == Keys.D6)
                {
                    Input += "6";
                }
                else if (key == Keys.D7)
                {
                    Input += "7";
                }
                else if (key == Keys.D8)
                {
                    Input += "8";
                }
                else if (key == Keys.D9)
                {
                    Input += "9";
                }
                else if(key==Keys.Back)
                {
                    if(Input.Length > 0)
                    {
                        Input = Input.Substring(0, Input.Length - 1);

                    }
                }
                else if(key==Keys.Enter)
                {
                    Input = " ";
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
