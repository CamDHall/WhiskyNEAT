﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Canvas screenCanvas;

    public GameObject nextPhase;
    public Text turns, currentTeam;
    public Text health, movement, courage, meleeSTR, rangedSTR, rangedDistance, name;
	
	void Update () {
        // Turn change phase button on/off and change text
        if (GameManager.selectedBaseCharacter != null)
        {
            nextPhase.SetActive(true);
            if (GameManager.selectedBaseCharacter.currentState == State.Moving)
            {
                nextPhase.GetComponentInChildren<Text>().text = "Attack";
            } else if(GameManager.selectedBaseCharacter.currentState == State.Attacking)
            {
                nextPhase.GetComponentInChildren<Text>().text = "End Turn";
            } else if(GameManager.selectedBaseCharacter.currentState == State.Done)
            {
                nextPhase.SetActive(false);
            }
        } else
        {
            nextPhase.SetActive(false);
        }

        // Display character info
        if (GameManager.selectedCharacterData != null)
        {
            turns.text = "Turn: " + GameManager.turns.ToString();
            currentTeam.text = GameManager.currentTeam.ToString() + "'s Turn";
            health.text = "Health: " + GameManager.selectedCharacterData.health.ToString();
            movement.text = "Movement: " + GameManager.selectedCharacterData.moves.ToString();
            courage.text = "Courage: " + GameManager.selectedCharacterData.courage.ToString();
            meleeSTR.text = "Melee Strength: " + GameManager.selectedCharacterData.meleeStrength.ToString();
            rangedSTR.text = "Ranged Strength: " + GameManager.selectedCharacterData.ToString();
            rangedDistance.text = "Ranged Distance: " + GameManager.selectedCharacterData.rangedDistance.ToString();
            name.text = "Name: " + GameManager.selectedCharacterData.name.ToString();
        }

		if(Input.GetMouseButtonDown(0))
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Enemy" || hit.transform.gameObject.tag == "Friend")
                    {
                        CharacterSelected(hit.transform.gameObject.tag.ToString(), hit.transform.gameObject.GetComponent<CharacterData>(), hit.transform.gameObject);
                    }
                    else if (!Paths.reachableTiles.Contains(hit.transform.gameObject))
                    {
                        Debug.Log(hit.transform.gameObject.tag);
                        NothingSelected();
                        Paths.ResetTiles();
                    }
                }
            }
        }
	}

    void NothingSelected()
    {
        GameManager.selectedCharacter.GetComponent<CharacterMenu>().DisplayOff();
        GameManager.selectedCharacter = null;
        GameManager.selectedCharacterData = null;
        GameManager.selectedBaseCharacter = null;
    }

    void CharacterSelected(string team, CharacterData info, GameObject hit)
    {
        if (hit != GameManager.selectedCharacter)
        {
            if (team == GameManager.characterTeam.ToString())
            {
                GameManager.selectedCharacter = hit;
                GameManager.selectedCharacterData = hit.transform.gameObject.GetComponent<CharacterData>();
                GameManager.selectedBaseCharacter = GameManager.selectedCharacter.GetComponent<BaseCharacter>();
                BaseCharacter baseCharacter = GameManager.selectedBaseCharacter;

                baseCharacter.EnterState(baseCharacter.currentState);
            }
        }

        GameManager.selectedCharacterData = info;
    }
}
