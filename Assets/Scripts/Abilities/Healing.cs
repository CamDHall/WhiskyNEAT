using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : AbilitiesBase {

	public static void BasicHeal(CharacterData data)
    {
        data.health += 10;
    }

    public static void SlowHeal(CharacterData data)
    {
        data.health += 4;
    }

    public static void AOEHeal()
    {

    }

}
