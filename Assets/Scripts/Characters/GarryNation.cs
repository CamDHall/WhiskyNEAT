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

    public override void AbilityTwo()
    {
        base.AbilityTwo();
        EnterState(AbilityState.Start, "CourageForScaredEnemies");
    }

    public override void AbilityThree()
    {
        throw new NotImplementedException();
    }
}
