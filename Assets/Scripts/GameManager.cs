using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterTeam { Friend, Enemy }
public class GameManager : MonoBehaviour {

    // General info
    public static int turns;
    public static string currentTeam;

    // What team's turn it is and what phase they're on
    public static CharacterTeam characterTeam;
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
    }

    void Start()
    {
        haveGone = 0;

        turns = 0;
        currentTeam = "Friends";
        selectedCharacter = null;
        selectedCharacterData = null;
        selectedBaseCharacter = null;
    }

    void Update()
    {
       if(haveGone == MapData.enemies.Count)
        {
            ChangeTeams();
            haveGone = 0;
        }
    }

    public static void ChangeTeams()
    {
        haveGone = 0;
        if (characterTeam == CharacterTeam.Friend)
        {
            characterTeam = CharacterTeam.Enemy;
        
            foreach (GameObject enemy in MapData.enemies)
            {
                // Reset every enemy to idle
                enemy.GetComponent<BaseCharacter>().EnterState(State.Idle);
                // Run in-progress abilties for every enemy
                enemy.GetComponent<AbilitiesBase>().HandleAbility();
            }

        }
        else
        {
            characterTeam = CharacterTeam.Friend;
            foreach(GameObject friend in MapData.friends)
            {
                // Reset every friend to idle
                friend.GetComponent<BaseCharacter>().EnterState(State.Idle);
                // Reset in-progress abilities for every friend
                friend.GetComponent<AbilitiesBase>().HandleAbility();
            }

            turns++;
        }
    }
}
