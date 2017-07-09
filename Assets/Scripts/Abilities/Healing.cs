using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing {

	public static void BasicHeal(CharacterData characterData)
    {
        characterData.health += 10;
    }

}
