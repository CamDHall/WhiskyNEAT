using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeHammerschmidt : BaseCharacter {

	void Update () {
	}

    public override void AbilityOne()
    {
        Healing.BasicHeal(characterData);
    }

    public override void AbilityThree()
    {
        Debug.Log("TWO");
    }

    public override void AbilityTwo()
    {
        Debug.Log("THREE");
    }
}
