using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ExtendedTest.GameObjects.Gatherables
{
    public class Plant : Gatherable
    {
        public int _GrowTime;
        public double _CurrentTime;
        public int _Stage;
        public bool _Harvestable = false;
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
            //_PlantTime = DateTime.UtcNow;
            Animation planted = new Animation("Planted", 64, 64, 1, 1);
            AddAnimation(planted);
            Animation grown = new Animation("Grown", 64, 64, 1, 1);
            AddAnimation(grown);
        }

        public override void UpdateActive(GameTime gameTime)
        {
            if(!_Harvestable)
            {
                _CurrentTime += gameTime.ElapsedGameTime.TotalSeconds;

                if(_CurrentTime >= _GrowTime)
                {
                    ChangeAnimation("Grown");
                    _Harvestable = true;
                    Console.WriteLine(this.Name + " at " + this._Position + " Finished growing!");

                }
            }

            base.UpdateActive(gameTime);
        }

        public void GetPlanted()
        {
            _CurrentTime = 0;
            ChangeAnimation("Planted");
        }
        //public void SetHarvestTime(int GrowTime)
        //{
        //    _HarvestTime = _PlantTime;   
        //    _HarvestTime = _HarvestTime.AddSeconds(GrowTime);
        //}
        
    }
}
