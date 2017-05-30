using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Phase { Idle, Moving, Attacking };
public class PhaseManager : MonoBehaviour {

    public static Phase characterPhase;
    int numEnemies, numFriendlies;

    // Track how many have moved and attacked for this phase
    public static int numMoved, numAttacked;
    void Start () {
        numMoved = 0;
        numAttacked = 0;
        characterPhase = Phase.Moving;

        // How many enenmies and friends are in this level
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        numFriendlies = GameObject.FindGameObjectsWithTag("Character").Length;
	}
	
	void Update () {
		if(RoundManager.whosTurn == Turns.Friendly)
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
                RoundManager.whosTurn = Turns.Friendly;
                numMoved = 0;
                numAttacked = 0;
            }
        }
	}
}
