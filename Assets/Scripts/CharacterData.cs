using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour {

    public List<Button> buttonsIP = new List<Button>();

    public int health, courage;
    public int currentNumberofMoves, moves;
    public int currentNumberofAttacks, numberofAttacks;

    public string characterName;

    // Abilities
    public string nameAbility1, nameAbility2, nameAbility3;

    public int rangedDistance, meleeDistance;
    public int rangedStrength, meleeStrength;

    void Awake()
    {
        currentNumberofMoves = moves;
    }

    void Update()
    {

    }
}
