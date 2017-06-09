using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text turn, whosText, phaseText;
    public GameObject startAttacking;

    public GameObject rangedButton, meleeButton;

    // Character info
    public static GameObject selectedCharacter;
    CharacterData data;
    Attacking selectedToAttack;

    public Text characterHealth, characterMoves, characterMeleeSTR, characterRangedSTR, characterCourage, characterName;

    void Start () {
        whosText.text = RoundManager.whosTurn.ToString() + " Turn";
        phaseText.text = PhaseManager.characterPhase.ToString();
        startAttacking.SetActive(false);

        rangedButton.SetActive(false);
        meleeButton.SetActive(false);
    }
	
	void Update () {
        turn.text = "Turn: " + GameManager.turns.ToString();

        // Attack button
        if (PhaseManager.characterPhase == Phase.Moving && startAttacking.activeSelf == false)
        {
            startAttacking.SetActive(true);
        }

        if (PhaseManager.characterPhase == Phase.Attacking && startAttacking.activeSelf == true)
        {
            startAttacking.SetActive(false);
        }

        // Show character info when you click on one
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Enemy" || hit.transform.gameObject.tag == "Friend")
                {
                    selectedCharacter = hit.transform.gameObject;
                    data = selectedCharacter.GetComponent<CharacterData>();
                    selectedToAttack = selectedCharacter.GetComponent<Attacking>();
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

            if (PhaseManager.characterPhase == Phase.Attacking && 
                (selectedCharacter.GetComponent<Attacking>()._enemiesInRange.Count > 0 
                || selectedCharacter.GetComponent<Attacking>()._friendsInRange.Count > 0
                || selectedCharacter.GetComponent<Attacking>()._enemiesInMeleeRange.Count > 0
                || selectedCharacter.GetComponent<Attacking>()._friendsInMeleeRange.Count > 0))
            {
                if (selectedToAttack._enemiesInRange.Count > 0 || selectedToAttack._friendsInRange.Count > 0)
                {
                    rangedButton.SetActive(true);
                }
                else
                {
                    rangedButton.SetActive(false);
                }

                if (selectedToAttack._enemiesInMeleeRange.Count > 0 || selectedToAttack._friendsInMeleeRange.Count > 0)
                {
                    meleeButton.SetActive(true);
                }
                else
                {
                    meleeButton.SetActive(false);
                }
            } else
            {
                meleeButton.SetActive(false);
                rangedButton.SetActive(false);
            }
        }
    }

    public void StartAttackingButton()
    {
        PhaseManager.characterPhase = Phase.Attacking;
    }

    public void MeleeButton()
    {
        selectedToAttack.SelectTarget();
        selectedToAttack.typeOfAttack = "Melee";
        selectedToAttack.characterSelected = true;
    }

    public void RangedButton()
    {
        selectedToAttack.SelectTarget();
        selectedToAttack.typeOfAttack = "Ranged";
        selectedToAttack.characterSelected = true;
    }
}
