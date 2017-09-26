﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    public class WorldObject : AnimatedSprite
    {
        private int difficulty;
        private List<WorldObject> parentList;
        private double respawnTimerStart = 15d;
        private double timeDead = 0d;
        private bool isGatherable = false;
        public bool _Detector = false;
        public bool _Walkable = false;
        public enum WorldObjectTag
        {
            kFireTag,
            kTreeTag,
            kRockTag,
            kFishingHoleTag,
            kDirtTag,
            kNoneTag
        }

        private WorldObjectTag myWorldObjectTag;

        public List<WorldObject> ParentList { get => parentList; set => parentList = value; }
        public double RespawnTimerStart { get => respawnTimerStart; set => respawnTimerStart = value; }
        public double TimeDead { get => timeDead; set => timeDead = value; }
        public bool IsGatherable { get => isGatherable; set => isGatherable = value; }
        public int Difficulty { get => difficulty; set => difficulty = value; }
        public WorldObjectTag MyWorldObjectTag { get => myWorldObjectTag; set => myWorldObjectTag = value; }

        public WorldObject()
        {
        }


        protected override void UpdateDead(GameTime gameTime)
        {
            base.UpdateDead(gameTime);
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }

        public virtual void DoThing()
        {

        }

        protected void Interact()
        {

        }
    }
}
