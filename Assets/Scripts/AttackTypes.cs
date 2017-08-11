using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTypes {

	public static int Damage(string type, GameObject attacker, GameObject target)
    {
        int amount = 0;
        if (type == "Melee")
        {
            amount = attacker.GetComponent<CharacterData>().meleeStrength;
        }
        else
        {
            amount = attacker.GetComponent<CharacterData>().rangedStrength;
        }

        target.GetComponent<CharacterData>().health -= amount;
        attacker.GetComponent<CharacterData>().currentNumberofAttacks--;
        attacker.GetComponent<CharacterMenu>().OverlayOff();

        GameManager.selectedCharacter = null;
        if(target.GetComponent<CharacterData>().health <= 0)
        {
            target.GetComponent<BaseCharacter>().Death(attacker);
        }

        return amount;
    }
}
