using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static int turns;

    // Character info
    GameObject selectedCharacter;
    CharacterData data;

    public Text characterHealth, characterMoves, characterMeleeSTR, characterRangedSTR, characterCourage, characterName;

	void Start () {
        turns = 0;
        selectedCharacter = null;
	}

    void Update()
    {
        // Show character info when you click on one
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Enemy" || hit.transform.gameObject.tag == "Character")
                {
                    selectedCharacter = hit.transform.gameObject;
                    data = selectedCharacter.GetComponent<CharacterData>();
                }
            }
        }

        if (data != null)
        {
            characterHealth.text = "Health: " + data.health.ToString();
            characterMoves.text = "Moves: " + data.moves.ToString();
            characterMeleeSTR.text = "Melee Strength: " + data.meleeStrength.ToString();
            characterRangedSTR.text = "Ranged Strength: " + data.rangedStrength.ToString();
            characterCourage.text = "Courage: " + data.courage.ToString();
            characterName.text = data.name.ToString();
        }
    }
}
