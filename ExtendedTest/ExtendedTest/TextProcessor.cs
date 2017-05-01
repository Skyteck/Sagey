using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class TextProcessor
    {
        readonly Player _player;
        public TextProcessor(Player player)
        {
            _player = player;
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
            }
        }

    }
}
