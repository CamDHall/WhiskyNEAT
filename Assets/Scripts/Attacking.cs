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

    void Awake()
    {
        characterData = GetComponent<CharacterData>();
        baseCharacter = GetComponent<BaseCharacter>();
    }

    void Update () {
		if(baseCharacter.currentState == State.Attacking)
        {
            //Debug.Log(_enemiesInMeleeRange.Count);
        }
	}
}
