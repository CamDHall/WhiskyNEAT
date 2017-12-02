using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour {

    public List<Button> buttonsIP = new List<Button>();

    public int health, courage;
    public int currentNumberofMoves, moves;
    public int currentNumberofAttacks, numberofAttacks;

	public CharacterType cType;

    public string characterName, _description1, _description2, _description3;

    // Abilities
    public string nameAbility1, nameAbility2, nameAbility3;

    public int rangedDistance, meleeDistance;
    public int rangedStrength, meleeStrength;

    // Captured
    public List<GameObject> capturedIcons = new List<GameObject>();

    void Awake()
    {
        currentNumberofMoves = moves;
    }
}
