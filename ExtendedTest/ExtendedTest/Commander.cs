using Comora;
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
        Game1 _Game;
        public String currentError = string.Empty;

        public Commander(Game1 tehGame)
        {
            _Game = tehGame;
        }

        public void Parsetext(String text)
        {
            List<String> parsedtext = text.Split(' ').ToList();
            if (parsedtext.Count != 4)
            {
                currentError = "command must be 4 words.";
                return;
            }
            String command = parsedtext[0];
            string target = parsedtext[1];
            NPC commandTarget;
            Camera cam =_Game.camera;
            if (target.Equals("PLAYER"))
            {
                //commandTarget = _Game.player;
            }
            else
            {
                commandTarget = _Game._NPCManager.FindNPCByName(target);
                if (commandTarget == null)
                {
                    _Game.kbHandler.Input = "NPC not found.";
                    return;
                }
            }

            Vector2 newPos;
            try
            {
                newPos = new Vector2(Convert.ToInt32(parsedtext[2]), Convert.ToInt32(parsedtext[3]));

            }
            catch
            {
                currentError = "Error parsing coordinates. Input format COMMAND OBJECT INT INT";
                return;
            }
            if (command.Equals("MOVE"))
            {
                if (parsedtext[1].Equals("PLAYER"))
                {

                    //_Game.player.setDestination(newPos);
                }
                else if(parsedtext[1].Equals("CAMERA"))
                {
                    cam.Position = newPos;
                }
                else
                {
                    //commandTarget.setDestination(newPos);
                }
            }
            else if(command.Equals("TELEPORT"))
            {
                if(target.Equals("PLAYER"))
                {
                    _Game.player._Position = newPos;
                }
                else
                {
                    //commandTarget._Position = newPos;
                }
            }
            else
            {
                currentError = "Unknown command";
            }
            currentError = string.Empty;
        }

    }
}
