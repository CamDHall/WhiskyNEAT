using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour {

    public int health;
    public int moves;
    public int numberOfAttacks;

    void Update()
    {
        if(gameObject.name == "George Hammerschmidt")
            Debug.Log(moves);
    }
}
