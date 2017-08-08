using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemetriWolfgang : BaseCharacter {

    public override void AbilityOne()
    {
        base.AbilityOne();
        EnterState(AbilityState.Start, "EnemyWeakHEAL");
        menu.ability1.GetComponent<AbilityButtonManager>().count = 3;
        menu.ability1.interactable = false;
    }

    public override void AbilityTwo()
    {
        base.AbilityTwo();
        EnterState(AbilityState.Start, "ScaredAllyHeal");
        menu.ability2.GetComponent<AbilityButtonManager>().count = 3;
        menu.ability2.interactable = false;
    }

    public override void AbilityThree()
    {
        EnterState(AbilityState.Start, "CourageBomb");
        GetComponent<CharacterData>().buttonsIP.Add(menu.ability3);
        menu.ability3.interactable = false;
    }
}
