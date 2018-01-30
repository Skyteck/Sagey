using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey
{
    public class Delegates
    {
        public delegate void NPCDyingDelegate(NPC theNPC);
        public delegate void NPCInteractDelegate(Enums.InteractType interactType, string interactID);
    }
}
