using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public GameObject targetingInfo, damagedInflicted;
    public Text _name, health, courage, moves, meleeSTR, rangedSTR, rangedDistance;

	public void DisplayTargetInfo(CharacterData characterData)
    {
        targetingInfo.SetActive(true);
        _name.text = "Name: " + characterData.name.ToString();
        health.text = "Health: " + characterData.health.ToString();
        courage.text = "Courage: " + characterData.courage.ToString();
        moves.text = "Moves: " + characterData.moves.ToString();
        meleeSTR.text = "MeleeSTR: " + characterData.meleeStrength.ToString();
        rangedSTR.text = "RangedSTR: " + characterData.rangedStrength.ToString();
        rangedDistance.text = "Ranged Distance: " + characterData.rangedDistance.ToString();
    }

    public void OffTargetingInfo()
    {
        targetingInfo.SetActive(false);
    }

    public void AttackInfo(string type, Attacking data)
    {
        damagedInflicted.SetActive(true);
        damagedInflicted.GetComponentInChildren<Text>().text = "Damage Inflicted: " + data.damageAmount;
    }

    public void AttackInfoOfF()
    {
        damagedInflicted.SetActive(false);
    }
}
