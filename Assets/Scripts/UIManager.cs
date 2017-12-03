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
    // Top
    public Text turns, currentTeam, unusedCharacters, captured;
    public RectTransform pieces;

    public Text s_characterInfo, s_characterName;
    public GameObject infoContainer;

    // Team highlighters
    public GameObject highlighterFriend, highlighterEnemy;

    // Captures
    [HideInInspector]
    public List<GameObject> t1_captures = new List<GameObject>();
    public List<GameObject> t2_captures = new List<GameObject>();

    private void Start()
    {
        Instance = this;
        abilityInfo.SetActive(false);

        GameObject pf_HighlighterFriend = Resources.Load("FriendHighlight") as GameObject;
        GameObject pf_EnemyHighlighter = Resources.Load("EnemyHighlight") as GameObject;
        highlighterEnemy = Instantiate(pf_EnemyHighlighter);
        highlighterFriend = Instantiate(pf_HighlighterFriend);
    }

    void Update () {
        // Top info
        turns.text = "Turn: " + GameManager.Instance.turns.ToString();
        if (GameManager.Instance.currentTeam == CharacterTeam.Friend)
        {
            currentTeam.text = "Player 1's Turn";
            unusedCharacters.text = "Unused Characters: " + (MapData.friends.Count - GameManager.Instance.haveGone);
        }
        else
        {
            currentTeam.text = "Player 2's Turn";
            unusedCharacters.text = "Unused Characters: " + (MapData.enemies.Count - GameManager.Instance.haveGone);
        }

        // Selected Character info
        if(GameManager.Instance.selectedCharacterData != null)
        {
            // Check if turned off
            if(!infoContainer.activeSelf)
            {
                infoContainer.SetActive(true);
            }
            s_characterName.text = GameManager.Instance.selectedCharacterData.characterName;
            s_characterInfo.text = 
                "\n<size=30><color=#3784C5FF>Health:\t\t\t\t\t\t\t\t</color></size><size=24>" + GameManager.Instance.selectedCharacterData.health + "</size>" +
                "\n<size=30><color=#3784C5FF>Attacks:\t\t\t\t\t\t\t</color></size><size=24>" + GameManager.Instance.selectedCharacterData.currentNumberofAttacks +
                "\n</size><size=30><color=#3784C5FF>Courage:\t\t\t\t\t\t</color></size><size=24>" + GameManager.Instance.selectedCharacterData.courage +
                "\n</size><size=30><color=#3784C5FF>Melee Strength:\t\t\t</color></size><size=24>" + GameManager.Instance.selectedCharacterData.meleeStrength +
                "\n</size><size=30><color=#3784C5FF>Ranged Strength:\t\t</color></size><size=24>" + GameManager.Instance.selectedCharacterData.rangedStrength +
                "\n</size><size=30><color=#3784C5FF>Ranged Distance:\t\t</color></size><size=24>" + GameManager.Instance.selectedCharacterData.rangedDistance + "</size>";
        } else
        {
            // Turn off
            if(infoContainer.activeSelf)
            {
                infoContainer.SetActive(false);
            }
        }

        // Update highlighter position, disable if null or doesn't match team color
        if (GameManager.Instance.selectedCharacter != null && (GameManager.Instance.selectedCharacterData.currentNumberofMoves > 0 ||
            (GameManager.Instance.selectedCharacterData.currentNumberofAttacks > 0 
            && GameManager.Instance.selectedBaseCharacter.currentState == State.Attacking)))
        {
            if (GameManager.Instance.selectedCharacter.tag == "Friend")
            {
                
                highlighterFriend.transform.parent = GameManager.Instance.selectedCharacter.transform.parent; // Tile not character
                highlighterFriend.transform.localPosition = new Vector3(0, 0.497f, 0);
                highlighterFriend.SetActive(true);
            } else
            {
                highlighterFriend.SetActive(false);
                highlighterEnemy.transform.parent = GameManager.Instance.selectedCharacter.transform.parent;
                highlighterEnemy.transform.localPosition = new Vector3(0, 0.497f, 0);
                highlighterEnemy.SetActive(true);
            }
        } else
        {
            highlighterFriend.SetActive(false);
            highlighterEnemy.SetActive(false);
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
                    }
                }
            }
        }
	}

    public void NothingSelected()
    {
        Paths.ResetTiles();
        if (GameManager.Instance.selectedCharacterData.tag == "Friend")
        {
            CapturedOff(t1_captures);
        } else
        {
            CapturedOff(t2_captures);
        }
        if (GameManager.Instance.selectedCharacter != null)
        {
            GameManager.Instance.selectedCharacter.GetComponent<CharacterMenu>().DisplayOff();
            GameManager.Instance.selectedCharacter = null;
            GameManager.Instance.selectedCharacterData = null;
            GameManager.Instance.selectedBaseCharacter = null;
            highlighterFriend.SetActive(false);
            highlighterEnemy.SetActive(false);
        }
    }

    void CharacterSelected(string team, CharacterData info, GameObject hit)
    {
        if (hit != GameManager.Instance.selectedCharacter || 
            (hit.GetComponent<CharacterData>().currentNumberofMoves < hit.GetComponent<CharacterData>().moves && hit.GetComponent<CharacterData>().currentNumberofMoves > 0))
        {
            if (team == GameManager.Instance.characterTeam.ToString())
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

        // Display pieces from selected character data
        if (GameManager.Instance.selectedCharacterData.tag == "Friend")
        {
            CapturedPieces(t1_captures);
        } else
        {
            CapturedPieces(t2_captures);
        }
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

    public void CapturedPieces(List<GameObject> icons)
    {
        foreach(GameObject icon in icons)
        {
            icon.SetActive(true);
        }
    }

    // Update caputred
    public void CapturedPieces(GameObject selected, GameObject captured)
    {
        Vector3 Pos;
        if(selected.tag == "Friend")
        {
            Pos = new Vector3(0 + (t1_captures.Count * 75), 0, 0);
        } else
        {
            Pos = new Vector3(0 + (t2_captures.Count * 75), 0, 0);
        }
        string c_name = captured.GetComponent<CharacterData>().characterName;
        Debug.Log(Resources.Load("Screenshots/" + c_name));
        GameObject temp = Instantiate(Resources.Load("Screenshots/" + c_name) as GameObject);
        temp.transform.SetParent(pieces.transform);
        temp.GetComponent<RectTransform>().localPosition = Pos;

        if (selected.tag == "Friend")
        {
            t1_captures.Add(temp);
        }
        else
        {
            t2_captures.Add(temp);
        }
    }

    void CapturedOff(List<GameObject> icons)
    {
        foreach(GameObject icon in icons)
        {
            icon.SetActive(false);
        }
    }

}
