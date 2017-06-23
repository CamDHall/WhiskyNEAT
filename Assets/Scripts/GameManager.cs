using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static int turns;
    public static BaseCharacter selectedBaseCharacter;

	void Start () {
        selectedBaseCharacter = null;
        turns = 0;
	}

    void Update()
    {

    }
}
