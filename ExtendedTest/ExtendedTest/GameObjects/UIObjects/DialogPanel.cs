using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ExtendedTest.GameObjects.UIObjects
{
    public class DialogPanel : UIPanel
    {
        List<string> DialogList;
        int CurrentIndex = 0;
        Vector2 offset = new Vector2(15, 15);
        public DialogPanel()
        {
            DialogList = new List<string>();
            string test = "This is a string! You better hope it's not very long though!";
            string test2 = "Testing!";
            string test3 = "For good measure.";
            DialogList.Add(test);
            DialogList.Add(test2);
            DialogList.Add(test3);
        }

        protected override void UpdateActive(GameTime gt)
        {
            base.UpdateActive(gt);
            
            if(_BoundingBox.Contains(InputHelper.MouseScreenPos))
            {
                if(InputHelper.LeftButtonClicked)
                {
                    CurrentIndex++;
                    if(CurrentIndex >= DialogList.Count)
                    {
                        CurrentIndex = DialogList.Count - 1;
                    }
                }
                else if(InputHelper.RightButtonClicked)
                {
                    CurrentIndex--;
                    if(CurrentIndex < 0)
                    {
                        CurrentIndex = 0;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            // https://github.com/Andrux51/MonoGame-Tutorial-DialogBox/blob/master/MonoGame-Tutorial-DialogBox/DialogBox.cs
            base.Draw(spriteBatch);
            Vector2 drawPos = new Vector2(_Position.X + offset.X, _Position.Y + offset.Y);
            spriteBatch.DrawString(count, DialogList[CurrentIndex], drawPos, Color.White);
        }


    }
}
