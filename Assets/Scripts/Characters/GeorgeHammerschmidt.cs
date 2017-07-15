using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeHammerschmidt : BaseCharacter {

    int timesAbilityCalled = 0;

    public override void AbilityOne()
    {
        EnterState(AbilityState.Start, "BasicHeal");
        //Healing.BasicHeal(characterData);
    }

    public override void AbilityTwo()
    {
        EnterState(AbilityState.Start, "SlowHeal");
        //abilitiesBase.SlowHealManager();
    }

    public override void AbilityThree()
    {
       
    }
}
