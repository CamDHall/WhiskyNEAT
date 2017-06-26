using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Phase { Idle, Moving, Attacking };
public class PhaseManager : MonoBehaviour {

    public static Phase characterPhase;
    public static int numEnemies, numFriendlies;

    // Track how many have moved and attacked for this phase
    public static int numMoved, numAttacked;
    void Awake () {
        numMoved = 0;
        numAttacked = 0;
        characterPhase = Phase.Moving;

        // How many enenmies and friends are in this level
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        numFriendlies = GameObject.FindGameObjectsWithTag("Friend").Length;
	}
	
	void Update () {
        // Debug.Log(RoundManager.whosTurn + ": " + characterPhase);

		if(RoundManager.whosTurn == Turns.Friend)
        {
            if(numMoved == numFriendlies)
            {
                characterPhase = Phase.Attacking;
            }

            if(numAttacked == numFriendlies)
            {
                characterPhase = Phase.Moving;
                RoundManager.whosTurn = Turns.Enemy;
                numMoved = 0;
                numAttacked = 0;
            }
        }

        if(RoundManager.whosTurn == Turns.Enemy)
        {
            if(numMoved == numEnemies)
            {
                characterPhase = Phase.Attacking;
            }

            if(numAttacked == numEnemies)
            {
                characterPhase = Phase.Moving;
                RoundManager.whosTurn = Turns.Friend;
                numMoved = 0;
                numAttacked = 0;
                GameManager.turns++;
            }
        }
	}
}
