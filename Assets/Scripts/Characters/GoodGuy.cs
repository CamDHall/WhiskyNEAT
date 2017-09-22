using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoodGuy : BaseCharacter {

    int timesAbilityCalled = 0;
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
        EnterState(AbilityState.Start, "BasicHeal");
        menu.ability1.GetComponent<AbilityButtonManager>().count = 3;
        menu.ability1.interactable = false;
    }

    public override void AbilityTwo()
    {
        base.AbilityTwo();
        EnterState(AbilityState.Start, "SlowHealLV1");
        GetComponent<CharacterData>().buttonsIP.Add(menu.ability2);
        menu.ability2.interactable = false;
    }

    public override void AbilityThree()
    {
        base.AbilityThree();
        EnterState(AbilityState.Start, "AOEHealLV1");
        menu.ability3.GetComponent<AbilityButtonManager>().count = 3;
        menu.ability3.interactable = false;
    }
}
