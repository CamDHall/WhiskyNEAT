using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour {

    // List of targets in Melee Range
    public List<GameObject> _friendsInMeleeRange = new List<GameObject>();
    public List<GameObject> _enemiesInMeleeRange = new List<GameObject>();

    bool determined = false;

    UIManager uiManager;

    // List of targets in Ranged Range
    public List<GameObject> _friendsInRangedRange = new List<GameObject>();
    public List<GameObject> _enemiesInRangedRange = new List<GameObject>();
    public List<GameObject> _allTargets = new List<GameObject>();

    CharacterData characterData;
    BaseCharacter baseCharacter;

    public bool isAttacking = false;
    public string type;
    public int damageAmount;
    GameObject targetObject;

    void Awake()
    {
        characterData = GetComponent<CharacterData>();
        baseCharacter = GetComponent<BaseCharacter>();
        characterData.currentNumberofAttacks = characterData.numberofAttacks;
    }

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    void Update () {
        // Check if out of attacks
        if (characterData.currentNumberofAttacks <= 0 && baseCharacter.currentState == State.Attacking)
        {
            baseCharacter.ExitState(State.Attacking);
        }

        if(baseCharacter.currentState == State.Attacking && Input.GetMouseButtonDown(0) && characterData.currentNumberofAttacks > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.position == gameObject.transform.position)
                {
                    GetComponent<CharacterMenu>().DisplayActionBar();
                }
            }
        }

		if(baseCharacter.currentState == State.Attacking && isAttacking && characterData.currentNumberofAttacks > 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (GameManager.confirmationState == Confirmation.Idle && _allTargets.Contains(hit.transform.gameObject))
                    {
                        Debug.Log(isAttacking);
                        uiManager.ConfirmationWindow();
                        targetObject = hit.transform.gameObject;
                        GameManager.currentAttackingObj = this;
                    }
                }
            }
        }
	}

    public void ExecuteAttack()
    {
        if (type == "Melee")
        {
            if (gameObject.tag == "Friend")
            {
                if (_enemiesInMeleeRange.Contains(targetObject.transform.gameObject))
                {
                    damageAmount = AttackTypes.Damage("Melee", gameObject, targetObject.transform.gameObject);
                }
            }
            else
            {
                if (_friendsInMeleeRange.Contains(targetObject.transform.gameObject))
                {
                    damageAmount = AttackTypes.Damage("Melee", gameObject, targetObject.transform.gameObject);
                }
            }
        }
        else
        {
            if (_enemiesInRangedRange.Contains(targetObject.transform.gameObject))
            {
                damageAmount = AttackTypes.Damage("Ranged", gameObject, targetObject.transform.gameObject);
            }
            else
            {
                if (_friendsInRangedRange.Contains(targetObject.transform.gameObject))
                {
                    damageAmount = AttackTypes.Damage("Ranged", gameObject, targetObject.transform.gameObject);
                }
            }
        }

        // Reset everything
        targetObject = null;
        GameManager.confirmationState = Confirmation.Idle;
    }
}
