using Sagey.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.GameObjects.NPCs
{
    public class DairyCow : NPC
    {
        public DairyCow(NPCManager nm) : base(nm)
        {
            _Interactable = true;
        }

        public override void Interact()
        {
            base.Interact();

        }
    }
}
