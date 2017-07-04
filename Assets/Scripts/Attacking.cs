using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour {

    // List of targets in Melee Range
    public List<GameObject> _friendsInMeleeRange = new List<GameObject>();
    public List<GameObject> _enemiesInMeleeRange = new List<GameObject>();

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
		if(baseCharacter.currentState == State.Attacking && GameManager.selectedCharacter == gameObject)
        {
            Targeting.DetermineTargets(gameObject.tag.ToString(), characterData.rangedDistance, characterData.meleeDistance, this.gameObject);
            Debug.Log(_enemiesInRangedRange.Count);
        }
	}
}
