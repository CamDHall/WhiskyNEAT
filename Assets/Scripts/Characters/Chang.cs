using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chang : BaseCharacter {

    public override void AbilityOne()
    {
        base.AbilityOne();
        EnterState(AbilityState.Start, "NobleFear");
    }

    public override void AbilityThree()
    {
        base.AbilityTwo();
    }

    public override void AbilityTwo()
    {
        base.AbilityThree();
    }
}
