using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GarryNation : BaseCharacter {

    public static UnityAction action1, action2, action3;

    private void Awake()
    {
        // Button click actions for abilities
        action1 = new UnityAction(AbilityOne);
        action2 = new UnityAction(AbilityTwo);
        action3 = new UnityAction(AbilityThree);
    }

    public override void AbilityOne()
    {
        base.AbilityOne();
        EnterState(AbilityState.Start, "BloodCry");
        menu.ability1.GetComponent<AbilityButtonManager>().count = 3;
        menu.ability1.interactable = false;
    }

    public override void AbilityTwo()
    {
        base.AbilityTwo();
        EnterState(AbilityState.Start, "MeleeBuffTeam");
        menu.ability2.GetComponent<AbilityButtonManager>().count = 3;
        menu.ability2.interactable = false;
    }

    public override void AbilityThree()
    {
        base.AbilityThree();
        EnterState(AbilityState.Start, "BasicCourageBoost");
        menu.ability3.GetComponent<AbilityButtonManager>().count = 3;
        menu.ability3.interactable = false;
    }
}
