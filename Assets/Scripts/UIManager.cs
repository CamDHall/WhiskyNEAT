using System.Collections;
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

    private void Start()
    {
        Instance = this;
        abilityInfo.SetActive(false);
    }

    void Update () {
        if (GameManager.confirmationState == Confirmation.Idle)
        {
            // Hovering
            Ray hoveringRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hoveringHit;

            if (Physics.Raycast(hoveringRay, out hoveringHit))
            {
                if ((hoveringHit.transform.tag == "Friend" || hoveringHit.transform.tag == "Enemy") && GameManager.selectedCharacter != null && 
                    GameManager.selectedCharacter.tag != hoveringHit.transform.tag && 
                    hoveringHit.transform.gameObject.GetComponent<BaseCharacter>() != GameManager.selectedBaseCharacter)
                {
                    hud.DisplayTargetInfo(hoveringHit.transform.gameObject.GetComponent<CharacterData>());
                }
                else
                {
                    hud.OffTargetingInfo();
                }
            }

            // Turn change phase button on/off and change text
            if (GameManager.selectedBaseCharacter != null)
            {
                nextPhase.SetActive(true);
                if (GameManager.selectedBaseCharacter.currentState == State.Moving)
                {
                    nextPhase.GetComponentInChildren<Text>().text = "Attack";
                }
                else if (GameManager.selectedBaseCharacter.currentState == State.Attacking)
                {
                    nextPhase.GetComponentInChildren<Text>().text = "End Turn";
                }
                else if (GameManager.selectedBaseCharacter.currentState == State.Done)
                {
                    nextPhase.SetActive(false);
                }
            }
            else
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
                nameText.text = "Name: " + GameManager.selectedCharacterData.characterName.ToString();
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
        if (GameManager.selectedCharacter != null)
        {
            GameManager.selectedCharacter.GetComponent<CharacterMenu>().DisplayOff();
            GameManager.selectedCharacter = null;
            GameManager.selectedCharacterData = null;
            GameManager.selectedBaseCharacter = null;
        }
    }

    void CharacterSelected(string team, CharacterData info, GameObject hit)
    {
        if (hit != GameManager.selectedCharacter || 
            (hit.GetComponent<CharacterData>().currentNumberofMoves < hit.GetComponent<CharacterData>().moves && hit.GetComponent<CharacterData>().currentNumberofMoves > 0))
        {
            if (team == GameManager.characterTeam.ToString())
            {
                if (GameManager.selectedCharacter != null)
                {
                    GameManager.selectedCharacter.GetComponent<CharacterMenu>().DisplayOff();
                    Paths.ResetTiles();
                }
                // Assignments
                GameManager.selectedCharacter = hit;
                GameManager.selectedCharacterData = hit.transform.gameObject.GetComponent<CharacterData>();
                GameManager.selectedBaseCharacter = GameManager.selectedCharacter.GetComponent<BaseCharacter>();
                BaseCharacter baseCharacter = GameManager.selectedBaseCharacter;
                baseCharacter.EnterState(baseCharacter.currentState);

                GameManager.selectedCharacter.GetComponent<Movement>().CheckMovement();
            }
        }

        GameManager.selectedCharacterData = info;
    }

    // Confirmation Window    
    public void ConfirmationWindow()
    {
        GameManager.confirmationState = Confirmation.Awaiting;
        confirmationWindow.SetActive(true);
        // Show Info
        CharacterData targetData = GameManager.currentAttackingObj.targetObject.GetComponent<CharacterData>();

        confirmInfo.text = targetData.health.ToString() + " --> " +
            (targetData.health - GameManager.currentAttackingObj.damageAmount).ToString();
    }

    // public virtual void FirstAbility() { }

}
