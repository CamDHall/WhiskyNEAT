using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemetriWolfgang : BaseCharacter {

    public override void AbilityOne()
    {
        base.AbilityOne();
        EnterState(AbilityState.Start, "EnemyWeakHEAL");
        Debug.Log(GetComponent<CharacterData>().currentNumberofAttacks);
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
