using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.GameObjects.Gatherables.Plants
{
    class StrawberryPlant : Plant
    {
        public StrawberryPlant()
        {
            MyPlantType = PlantType.kStrawBerryType;
            _GrowTime = 10;
            SetHarvestTime(_GrowTime);
            this.Name = "Strawberry Plant";
        }
    }

    class PotatoPlant : Plant
    {
        public PotatoPlant()
        {
            MyPlantType = PlantType.kPotatoPlant;
            _GrowTime = 10;
            SetHarvestTime(_GrowTime);
            this.Name = "Potato Plant";
        }
    }

    class CornPlant : Plant
    {
        public CornPlant()
        {
            MyPlantType = PlantType.kCornPlant;
            _GrowTime = 30;
            SetHarvestTime(_GrowTime);
            this.Name = "Corn Plant";
        }
    }
}
