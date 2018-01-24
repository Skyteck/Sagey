using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.Enums
{
    public enum MonsterTypes
    {
        kMonsterSlime,
        kMonsterWolf
    }

    public enum GatherTypes
    {
        kGatherTree,
        kGatherRock,
        kGatherEnergy,
        kGatherFIshingHole
    }

    public enum EffectTypes
    {
        kEffectStun,
        kEffectFreeze,
        kEffectBurn,
        kEffectPoison,
        kEffectNone
    }

    public enum ItemID
    {
        kItemLog = 1,
        kItemOre,
        kItemFish,
        kItemFishNet,
        kItemMatches,
        kItemFishStick,
        kItemNone,
        kItemError,
        kItemStrawberry,
        kItemMilk,
        kItemTest,
        kItemBucket,
        kItemFlour,
        kItemWheat,
        kItemSlimeGoo,
        kItemGoldPieces,
        kItemFireKit,
        kItemStrawberryDough,
        kItemBlueberry,
        kItemBlueberryDough,
        kItemEggs
    }

    public enum EventTypes
    {
        kEventNPCDying = 1,
        kEventNPCInteract
    }
}
