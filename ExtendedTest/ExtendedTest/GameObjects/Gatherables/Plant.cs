using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ExtendedTest.GameObjects
{
    public class Plant : Gatherables.Gatherable
    {
        public int _GrowTime;
        public double _CurrentTime;
        public int _Stage;
        public bool _Harvestable = false;
        public DateTime _PlantTime;
        public DateTime _HarvestTime;
        public enum PlantType
        {
            kStrawBerryType,
            kNoneType,
            kCornPlant,
            kPotatoPlant
        }

        private PlantType myPlantType = PlantType.kNoneType;

        public PlantType MyPlantType { get => myPlantType; set => myPlantType = value; }

        public Plant()
        {
            _PlantTime = DateTime.UtcNow;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            if(!_Harvestable)
            {
                if(DateTime.UtcNow > _HarvestTime)
                {
                    _Harvestable = true;
                    Console.WriteLine(this.Name + " at " + this._Position + " Finished growing!");

                }
            }

            base.UpdateActive(gameTime);
        }

        public void SetHarvestTime(int GrowTime)
        {
            _HarvestTime = _PlantTime;   
            _HarvestTime = _HarvestTime.AddSeconds(GrowTime);
        }
        
    }
}
