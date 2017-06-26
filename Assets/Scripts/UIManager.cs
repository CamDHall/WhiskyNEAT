using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    // Overlay
    public Image indicator;
    public Canvas worldCanvas;

    bool showingOverlay = false;
    List<Image> container;

    //
    public Text turn, whosText, phaseText;
    public GameObject startAttacking;

    public GameObject rangedButton, meleeButton, abilityBar;

    // Character info
    public static GameObject selectedCharacter;
    public GameObject infoPane;
    BaseCharacter data;
    Attacking selectedToAttack;

    public Text characterHealth, characterMoves, characterMeleeSTR, characterRangedSTR, characterCourage, characterName;

    void Start () {
        container = new List<Image>(); // Overlay

        whosText.text = RoundManager.whosTurn.ToString() + " Turn";
        phaseText.text = PhaseManager.characterPhase.ToString();
        startAttacking.SetActive(false);

        rangedButton.SetActive(false);
        meleeButton.SetActive(false);
        abilityBar.SetActive(false);
    }
	
	void Update () {    
        if(selectedCharacter == null)
        {
            OverlayOff();
            characterHealth.enabled = false;
            characterMoves.enabled = false;
            characterCourage.enabled = false;
            characterMeleeSTR.enabled = false;
            characterRangedSTR.enabled = false;
            characterName.enabled = false;
        } else
        {
            characterHealth.enabled = true;
            characterMoves.enabled = true;
            characterCourage.enabled = true;
            characterMeleeSTR.enabled = true;
            characterRangedSTR.enabled = true;
            characterName.enabled = true;
        }

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
                    data = selectedCharacter.GetComponent<BaseCharacter>();
                    selectedToAttack = selectedCharacter.GetComponent<Attacking>();
                } else
                {
                    selectedCharacter = null;
                }
            }
        }

        if (data != null && selectedCharacter != null)
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
                abilityBar.SetActive(true);
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
                abilityBar.SetActive(false);
            }
        }
    }

    public void StartAttackingButton()
    {
        if (selectedCharacter != null)
        {
            selectedCharacter.GetComponent<Movement>().ResetTiles();
        }
        PhaseManager.characterPhase = Phase.Attacking;
    }

    void DefaultActions(string type)
    {
        if (type == "Melee" || type == "Ranged")
        {
            if (selectedToAttack.gameObject.tag == "Friend")
                OverlayOn(selectedToAttack._enemiesInMeleeRange, selectedToAttack._enemiesInRange);
            else
                OverlayOn(selectedToAttack._friendsInMeleeRange, selectedToAttack._friendsInRange);
        } else
            selectedCharacter.GetComponent<AbilitiesBase>().SetInfo(type);

        selectedToAttack.characterSelected = true;
        selectedToAttack.typeOfAttack = type;
    }

    public void MeleeButton()
    {
        DefaultActions("Melee");
    }

    public void RangedButton()
    {
        DefaultActions("Ranged");
    }

    public void AbilityOne()
    {
        DefaultActions("AbilityOne");
    }

    public void AbilityTwo()
    {
        DefaultActions("AbilityTwo");
    }

    public void AbilityThree()
    {
        DefaultActions("AbilityThree");
    }

    void OverlayOn(List<GameObject> meleeTargets, List<GameObject> rangedTargets)
    {
        List<GameObject> allTargets = new List<GameObject>();
        if (meleeTargets != null)
        {
            foreach(GameObject mTarget in meleeTargets)
            {
                Debug.Log(mTarget.name);
                allTargets.Add(mTarget);
            }

            foreach(GameObject rTarget in rangedTargets)
            {
                if(!allTargets.Contains(rTarget))
                {
                    allTargets.Add(rTarget);
                }
            }
        }

        if (rangedTargets != null)
        {
            foreach(GameObject rTarget in rangedTargets)
            {
                if(!allTargets.Contains(rTarget))
                {
                    allTargets.Add(rTarget);
                }
            }

            foreach(GameObject target in allTargets)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                container.Add(img);
            }
        }
    }

    public void OverlayOff()
    {
        foreach (Image img in container)
        {
            Destroy(img);
        }

        container.Clear();
    }
}
