using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSchimdtAbilities : AbilitiesBase {

    void Update()
    {
        if (_abilityNumber == "AbilityOne")
            AbilityOne();
        else if (_abilityNumber == "AbilityTwo")
            AbilityTwo();
        else if (_abilityNumber == "AbilityThree")
            AbilityThree();
    }

    void AbilityOne()
    {
        Reset();
    }

    void AbilityTwo()
    {
        Reset();
    }

    void AbilityThree()
    {
        Reset();
    }
}
