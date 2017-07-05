using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterTeam { Friend, Enemy }
public enum Phase { Moving, Attacking }
public class GameManager : MonoBehaviour {

    // What team's turn it is and what phase they're on
    public static CharacterTeam characterTeam;
    public static Phase currentPhase;
    public static int haveGone; // Keep track of how many of the current enemies or friends have moved and attacked

    // Selected info
    public static GameObject selectedCharacter;
    public static CharacterData selectedCharacterData;
    public static BaseCharacter selectedBaseCharacter;

    // Targeting
    public GameObject selectedTarget;

    void Awake()
    {
        characterTeam = CharacterTeam.Friend;
        currentPhase = Phase.Moving;
    }

    void Start()
    {
        haveGone = 0;

        selectedCharacter = null;
        selectedCharacterData = null;
        selectedBaseCharacter = null;
    }

    public static void ChangeTeams()
    {
        if (characterTeam == CharacterTeam.Friend)
            characterTeam = CharacterTeam.Enemy;
        else
            characterTeam = CharacterTeam.Friend;

    }
}
