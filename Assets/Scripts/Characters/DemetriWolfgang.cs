using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemetriWolfgang : BaseCharacter {

    public override void AbilityOne()
    {
        base.AbilityOne();
        EnterState(AbilityState.Start, "EnemyWeakHEAL");
    }

    public override void AbilityTwo()
    {
        base.AbilityTwo();
        EnterState(AbilityState.Start, "ScaredAllyHeal");
    }

    public override void AbilityThree()
    {
        base.AbilityThree();
    }
}
