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
        _name.text = "Name: " + characterData.characterName.ToString();
        health.text = "Health: " + characterData.health.ToString();
        courage.text = "Courage: " + characterData.courage.ToString();
    }

    public void OffTargetingInfo()
    {
        targetingInfo.SetActive(false);
    }
}
