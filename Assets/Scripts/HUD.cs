using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public GameObject targetingInfo;
    public Text _name, health, courage;

	public void DisplayTargetInfo(CharacterData characterData)
    {
        targetingInfo.SetActive(true);
        _name.text = "<size=36>Name: </size>\t" + characterData.characterName.ToString();
        health.text = "<size=36>Health: </size>\t" + characterData.health.ToString();
        courage.text = "<size=36>Courage: </size>\t" + characterData.courage.ToString();
    }

    public void OffTargetingInfo()
    {
        targetingInfo.SetActive(false);
    }
}
