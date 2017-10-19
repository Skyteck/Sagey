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
        Managers.DialogManager _DialogManager;
        List<string> DialogList;
        int CurrentIndex = 0;
        Vector2 offset = new Vector2(10, 10);
        int currentStringLength = 0;

        private Vector2 _characterSize
        {
            get
            {
                return count.MeasureString(new StringBuilder("W", 1));
            }
        } 

        public DialogPanel(Managers.DialogManager dm)
        {
            DialogList = new List<string>();
            _Resizable = false;
            adjustedHeight = 100;
            adJustedWidth = 450;
            _DialogManager = dm;
            _DialogManager.DialogPlayed += HandleDialogPlayed;
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
                else if(InputHelper.MiddleButtonClicked)
                {
                    Console.WriteLine(adJustedWidth + " " + adjustedHeight);
                }

            }
        }

        public void HandleDialogPlayed(object sender, EventArgs args)
        {
            DialogList.Clear();
            foreach(string msg in _DialogManager.CurrentDialog.textList)
            {
                DialogList.Add(msg);
            }
            parentManager.ShowPanel("Dialog");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            // https://github.com/Andrux51/MonoGame-Tutorial-DialogBox/blob/master/MonoGame-Tutorial-DialogBox/DialogBox.cs
            base.Draw(spriteBatch);
            Vector2 drawPos = new Vector2(_Position.X + offset.X, _Position.Y + offset.Y);
            spriteBatch.DrawString(count, DialogList[CurrentIndex], drawPos, Color.White);
        }


    }

    class WordInfo
    {
        int wordLength = 0;
        StringBuilder word;
        public WordInfo(String w)
        {
            word = new StringBuilder(w);
        }
    }
}
