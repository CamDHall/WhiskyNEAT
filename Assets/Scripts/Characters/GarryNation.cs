using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarryNation : BaseCharacter {

    public override void AbilityOne()
    {
        base.AbilityOne();
        EnterState(AbilityState.Start, "CourageBoostBasic");
        menu.ability1.GetComponent<AbilityButtonManager>().count = 3;
        menu.ability1.interactable = false;
    }

    public override void AbilityTwo()
    {
        base.AbilityTwo();
        EnterState(AbilityState.Start, "CourageForScaredEnemies");
        menu.ability2.GetComponent<AbilityButtonManager>().count = 3;
        menu.ability2.interactable = false;
    }

    public override void AbilityThree()
    {
        base.AbilityThree();
        EnterState(AbilityState.Start, "BasicCourageSubtract");
        menu.ability3.GetComponent<AbilityButtonManager>().count = 3;
        menu.ability3.interactable = false;
    }
}
