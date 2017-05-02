using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class Commander
    {
        readonly Player _player;
        Game1 _Game;

        public Commander(Game1 tehGame)
        {
            _Game = tehGame;
        }

        public void Parsetext(String text)
        {
            List<String> parsedtext = text.Split(' ').ToList();
            if (parsedtext.Count != 4) return;

            if(parsedtext[0].Equals("MOVE"))
            {
                if(parsedtext[1].Equals("PLAYER"))
                {
                    Vector2 newPos = new Vector2(Convert.ToInt32(parsedtext[2]), Convert.ToInt32(parsedtext[3]));
                    _player.setDestination(newPos);
                }
                else
                {
                    var obj = _Game._NPCManager.findNPC(parsedtext[1]);
                }
            }
        }

    }
}
