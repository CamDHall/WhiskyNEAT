using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSchimdtAbilities : AbilitiesBase {

    BaseCharacter data;

    void Start()
    {
        data = GetComponent<BaseCharacter>();
    }

    public void AbilityOne()
    {
        Mana(5);
        data.health += 5;
        data.ability1_used = true;

    }

    public void AbilityTwo()
    {
        //Reset();
    }

    public void AbilityThree()
    {
        //Reset();
    }
}
