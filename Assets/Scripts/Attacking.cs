using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour {

    // List of targets in Melee Range
    public List<GameObject> _friendsInMeleeRange = new List<GameObject>();
    public List<GameObject> _enemiesInMeleeRange = new List<GameObject>();

    bool determined = false;

    // List of targets in Ranged Range
    public List<GameObject> _friendsInRangedRange = new List<GameObject>();
    public List<GameObject> _enemiesInRangedRange = new List<GameObject>();

    CharacterData characterData;
    BaseCharacter baseCharacter;

    public bool isAttacking = false;
    public string type;

    void Awake()
    {
        characterData = GetComponent<CharacterData>();
        baseCharacter = GetComponent<BaseCharacter>();
    }

    void Update () {
		if(baseCharacter.currentState == State.Attacking && isAttacking)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    if(type == "Melee")
                    {
                        if(gameObject.tag == "Friend")
                        {
                            if(_enemiesInMeleeRange.Contains(hit.transform.gameObject))
                            {
                                AttackTypes.Damage("Melee", gameObject, hit.transform.gameObject);
                            }
                        } else
                        {
                            if(_friendsInMeleeRange.Contains(hit.transform.gameObject))
                            {
                                AttackTypes.Damage("Melee", gameObject, hit.transform.gameObject);
                            }
                        }
                    } else
                    {
                        if(_enemiesInRangedRange.Contains(hit.transform.gameObject))
                        {
                            AttackTypes.Damage("Ranged", gameObject, hit.transform.gameObject);
                        } else
                        {
                            if(_friendsInRangedRange.Contains(hit.transform.gameObject))
                            {
                                AttackTypes.Damage("Ranged", gameObject, hit.transform.gameObject);
                            }
                        }
                    }
                }
            }
        }
	}
}
