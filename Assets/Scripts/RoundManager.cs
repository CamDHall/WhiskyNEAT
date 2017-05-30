using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turns { Enemy, Friendly, Idle};

public class RoundManager : MonoBehaviour {

    public static Turns whosTurn;
    MapData mapData;
    public GameObject map;
	void Start () {
        whosTurn = Turns.Friendly;
        mapData = map.GetComponent<MapData>();
	}
	
	void Update () {
		switch(whosTurn)
        {
            case Turns.Enemy:
                SetupEnemies();
                break;
            case Turns.Friendly:
                SetupFriends();
                break;
        }
	}

    void SetupEnemies()
    {
        foreach(GameObject friend in mapData.characters)
        {
            friend.GetComponent<Movement>().enabled = false;
            friend.GetComponent<Attacking>().enabled = false;
        }

        foreach(GameObject enemy in mapData.enemies)
        {
            enemy.GetComponent<Movement>().enabled = true;
            enemy.GetComponent<Attacking>().enabled = true;
        }
    }

    void SetupFriends()
    {
        foreach(GameObject friend in mapData.characters)
        {
            friend.GetComponent<Movement>().enabled = true;
            friend.GetComponent<Attacking>().enabled = true;
        }

        foreach(GameObject enemy in mapData.enemies)
        {
            enemy.GetComponent<Movement>().enabled = false;
            enemy.GetComponent<Attacking>().enabled = false;
        }
    }
}
