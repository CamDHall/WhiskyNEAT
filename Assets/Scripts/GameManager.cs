using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterTeam { Friend, Enemy }
public enum Confirmation { Idle, Awaiting, Ready }
public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    // General info
    public static int turns;
    public static CharacterTeam currentTeam;

    public static Confirmation confirmationState;

    // What team's turn it is and what phase they're on
    public static CharacterTeam characterTeam;
    public static int haveGone; // Keep track of how many of the current enemies or friends have moved and attacked

    // Selected info
    public static GameObject selectedCharacter;
    public static CharacterData selectedCharacterData;
    public static BaseCharacter selectedBaseCharacter;

    // Targeting
    public GameObject selectedTarget;
    public static Attacking currentAttackingObj;

    // UI round managment
    public GameObject finishScreen;

    void Awake()
    {
        characterTeam = CharacterTeam.Friend;
        currentTeam = CharacterTeam.Friend;

    }

    void Start()
    {
        Instance = this;
        haveGone = 0;
        confirmationState = Confirmation.Idle;

        turns = 0;
        selectedCharacter = null;
        selectedCharacterData = null;
        selectedBaseCharacter = null;
    }

    void Update()
    {
        if(currentTeam == CharacterTeam.Friend && haveGone == MapData.friends.Count)
        {
            ChangeTeams();
        } else if(currentTeam == CharacterTeam.Enemy && haveGone == MapData.enemies.Count)
        {
            ChangeTeams();
        }


    }

    static void CallReset(CharacterMenu menu, CharacterData characterData)
    {
        menu.ability1.GetComponent<AbilityButtonManager>().ResetButtons(characterData);
        menu.ability2.GetComponent<AbilityButtonManager>().ResetButtons(characterData);
        menu.ability3.GetComponent<AbilityButtonManager>().ResetButtons(characterData);
    }

    public static void ChangeTeams()
    {
        haveGone = 0;
        if (currentTeam == CharacterTeam.Friend)
        {
            MapData.FriendInfo();
            characterTeam = CharacterTeam.Enemy;
            currentTeam = CharacterTeam.Enemy;
        
            foreach (GameObject enemy in MapData.enemies)
            {
                // Reset every enemy to idle
                enemy.GetComponent<BaseCharacter>().EnterState(State.Idle);
                // Run in-progress abilties for every enemy IF THERE ARE ANY
                if (enemy.GetComponent<AbilitiesBase>().wipAbilities.Count > 0)
                    enemy.GetComponent<AbilitiesBase>().RunWIP();
            }

            // Reset Buttons
            foreach(GameObject friend in MapData.friends)
            {
                CallReset(friend.GetComponent<CharacterMenu>(), friend.GetComponent<CharacterData>());
            }
        }
        else
        {
            MapData.EnemyInfo();
            characterTeam = CharacterTeam.Friend;
            currentTeam = CharacterTeam.Friend;

            foreach(GameObject friend in MapData.friends)
            {
                // Reset every friend to idle
                friend.GetComponent<BaseCharacter>().EnterState(State.Idle);
                // Reset in-progress abilities for every friend IF THERE ARE ANY
                if (friend.GetComponent<AbilitiesBase>().wipAbilities.Count > 0)
                {
                    friend.GetComponent<AbilitiesBase>().RunWIP();
                }
            }

            foreach(GameObject enemy in MapData.enemies)
            {
                CallReset(enemy.GetComponent<CharacterMenu>(), enemy.GetComponent<CharacterData>());
            }

            turns++;
        }
    }

    public void EndGame(CharacterTeam winningTeam)
    {
        finishScreen.SetActive(true);
        finishScreen.GetComponentInChildren<Text>().text = winningTeam.ToString() + " won this round.";
    }
}
