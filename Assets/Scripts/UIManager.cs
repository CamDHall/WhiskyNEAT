﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;

    public GameObject abilityInfo;

    public Canvas screenCanvas;
    public HUD hud;

    // Confirm
    public GameObject confirmationWindow;
    public Text confirmInfo;

    public GameObject nextPhase;
    public Text turns, currentTeam;
    public Text health, movement, courage, meleeSTR, rangedSTR, rangedDistance, nameText;

    // Team highlighters
    GameObject highlighterFriend, highlighterEnemy; 

    private void Start()
    {
        Instance = this;
        abilityInfo.SetActive(false);

        GameObject pf_HighlighterFriend = Resources.Load("Highlight") as GameObject;
        highlighterFriend = Instantiate(pf_HighlighterFriend);
    }

    void Update () {
        if(GameManager.Instance.selectedCharacter != null)
        {
            Debug.Log(highlighterFriend);
            highlighterFriend.transform.parent = GameManager.Instance.selectedCharacter.transform;
            highlighterFriend.transform.localPosition = Vector3.zero;
            highlighterFriend.SetActive(true);
        }

        if (GameManager.confirmationState == Confirmation.Idle)
        {
            // Hovering
            Ray hoveringRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hoveringHit;

            if (Physics.Raycast(hoveringRay, out hoveringHit))
            {
                if ((hoveringHit.transform.tag == "Friend" || hoveringHit.transform.tag == "Enemy") && GameManager.Instance.selectedCharacter 
                    != null && GameManager.Instance.selectedCharacter.tag != hoveringHit.transform.tag && 
                    hoveringHit.transform.gameObject.GetComponent<BaseCharacter>() != GameManager.Instance.selectedBaseCharacter)
                {
                    hud.DisplayTargetInfo(hoveringHit.transform.gameObject.GetComponent<CharacterData>());
                }
                else
                {
                    hud.OffTargetingInfo();
                }
            }

            // Turn change phase button on/off and change text
            if (GameManager.Instance.selectedBaseCharacter != null)
            {
                nextPhase.SetActive(true);
                if (GameManager.Instance.selectedBaseCharacter.currentState == State.Moving)
                {
                    nextPhase.GetComponentInChildren<Text>().text = "Attack";
                }
                else if (GameManager.Instance.selectedBaseCharacter.currentState == State.Attacking)
                {
                    nextPhase.GetComponentInChildren<Text>().text = "End Turn";
                }
                else if (GameManager.Instance.selectedBaseCharacter.currentState == State.Done)
                {
                    nextPhase.SetActive(false);
                }
            }
            else
            {
                nextPhase.SetActive(false);
            }

            // Display character info
            if (GameManager.Instance.selectedCharacterData != null)
            {
                turns.text = "Turn: " + GameManager.turns.ToString();
                currentTeam.text = GameManager.currentTeam.ToString() + "'s Turn";
                health.text = "Health: " + GameManager.Instance.selectedCharacterData.health.ToString();
                movement.text = "Movement: " + GameManager.Instance.selectedCharacterData.moves.ToString();
                courage.text = "Courage: " + GameManager.Instance.selectedCharacterData.courage.ToString();
                meleeSTR.text = "Melee Strength: " + GameManager.Instance.selectedCharacterData.meleeStrength.ToString();
                rangedSTR.text = "Ranged Strength: " + GameManager.Instance.selectedCharacterData.ToString();
                rangedDistance.text = "Ranged Distance: " + GameManager.Instance.selectedCharacterData.rangedDistance.ToString();
                nameText.text = "Name: " + GameManager.Instance.selectedCharacterData.characterName.ToString();
            }

            // General Selections
            if (Input.GetMouseButtonDown(0))
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
                            NothingSelected();
                        }
                    }
                }
            }
        }
	}

    void NothingSelected()
    {
        Paths.ResetTiles();
        if (GameManager.Instance.selectedCharacter != null)
        {
            GameManager.Instance.selectedCharacter.GetComponent<CharacterMenu>().DisplayOff();
            GameManager.Instance.selectedCharacter = null;
            GameManager.Instance.selectedCharacterData = null;
            GameManager.Instance.selectedBaseCharacter = null;
        }
    }

    void CharacterSelected(string team, CharacterData info, GameObject hit)
    {
        if (hit != GameManager.Instance.selectedCharacter || 
            (hit.GetComponent<CharacterData>().currentNumberofMoves < hit.GetComponent<CharacterData>().moves && hit.GetComponent<CharacterData>().currentNumberofMoves > 0))
        {
            if (team == GameManager.characterTeam.ToString())
            {
                if (GameManager.Instance.selectedCharacter != null)
                {
                    GameManager.Instance.selectedCharacter.GetComponent<CharacterMenu>().DisplayOff();
                    Paths.ResetTiles();
                }
                // Assignments
                GameManager.Instance.selectedCharacter = hit;
                GameManager.Instance.selectedCharacterData = hit.transform.gameObject.GetComponent<CharacterData>();
                GameManager.Instance.selectedBaseCharacter = GameManager.Instance.selectedCharacter.GetComponent<BaseCharacter>();
                BaseCharacter baseCharacter = GameManager.Instance.selectedBaseCharacter;
                baseCharacter.EnterState(baseCharacter.currentState);

                GameManager.Instance.selectedCharacter.GetComponent<Movement>().CheckMovement();
            }
        }

        GameManager.Instance.selectedCharacterData = info;
    }

    // Confirmation Window    
    public void ConfirmationWindow()
    {
        GameManager.confirmationState = Confirmation.Awaiting;
        confirmationWindow.SetActive(true);
        // Show Info
        CharacterData targetData = GameManager.Instance.currentAttackingObj.targetObject.GetComponent<CharacterData>();

        confirmInfo.text = targetData.health.ToString() + " --> " +
            (targetData.health - GameManager.Instance.currentAttackingObj.damageAmount).ToString();
    }

    // public virtual void FirstAbility() { }

}
