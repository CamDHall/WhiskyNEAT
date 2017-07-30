using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarryNation : BaseCharacter {

    public override void AbilityOne()
    {
        base.AbilityOne();
        EnterState(AbilityState.Start, "CourageBoostBasic");
    }

    public override void AbilityThree()
    {
        throw new NotImplementedException();
    }

    public override void AbilityTwo()
    {
        throw new NotImplementedException();
    }
}
