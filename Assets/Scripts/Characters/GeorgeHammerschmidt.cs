using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeHammerschmidt : BaseCharacter {

    int timesAbilityCalled = 0;

    public override void AbilityOne()
    {
        base.AbilityOne();
        EnterState(AbilityState.Start, "BasicHeal");
    }

    public override void AbilityTwo()
    {
        base.AbilityTwo();
        EnterState(AbilityState.Start, "SlowHealLV1");
    }

    public override void AbilityThree()
    {
        base.AbilityThree();
        EnterState(AbilityState.Start, "AOEHealLV1");
    }
}
