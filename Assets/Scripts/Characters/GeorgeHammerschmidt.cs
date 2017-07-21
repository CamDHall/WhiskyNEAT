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
        //Healing.BasicHeal(characterData);
    }

    public override void AbilityTwo()
    {
        EnterState(AbilityState.Start, "SlowHealLV1");
        //abilitiesBase.SlowHealManager();
    }

    public override void AbilityThree()
    {
        EnterState(AbilityState.Start, "AOEHealLV1");
    }
}
