using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{
    class ChemistryManager
    {
        InventoryManager _InvenManager;
        WorldObjectManager _WorldObjectManager;
        NPCManager _NPCManager;
        public ChemistryManager(InventoryManager invenM, WorldObjectManager WOM, NPCManager NPCM)
        {
            _InvenManager = invenM;
            _WorldObjectManager = WOM;
            _NPCManager = NPCM;
        }
    }
}
