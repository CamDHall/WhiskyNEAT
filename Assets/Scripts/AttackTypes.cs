using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTypes {

	public static void Damage(string type, GameObject attacker, GameObject target)
    {
        if (type == "Melee")
        {
            target.GetComponent<CharacterData>().health -= attacker.GetComponent<CharacterData>().meleeStrength;
        }
        else
        {
            target.GetComponent<CharacterData>().health -= attacker.GetComponent<CharacterData>().rangedStrength;
        }
        attacker.GetComponent<CharacterData>().currentNumberofAttacks--;
        attacker.GetComponent<CharacterMenu>().OverlayOff();

        GameManager.selectedCharacter = null;
        if(target.GetComponent<CharacterData>().health <= 0)
        {
            target.GetComponent<BaseCharacter>().Death(attacker);
        }
    }
}
