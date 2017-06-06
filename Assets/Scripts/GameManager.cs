using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static int turns;
    public static CharacterData selectedCharacterData;

	void Start () {
        selectedCharacterData = null;
        turns = 0;
	}

    void Update()
    {

    }
}
