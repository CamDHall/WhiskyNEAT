﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour {

    public int health, courage;
    public int currentMana, mana;
    public int currentNumberofMoves, moves;
    public int currentNumberofAttacks, numberofAttacks;

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
