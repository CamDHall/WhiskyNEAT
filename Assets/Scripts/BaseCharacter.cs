using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {

    public float health = 3;

    MapData mapData;
    public GameObject map;

    void Start () {
        mapData = map.GetComponent<MapData>();
	}
	
	// Update is called once per frame
	void Update () {
        if(health <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                mapData.enemies.Remove(gameObject);
                PhaseManager.numEnemies--;
                Destroy(gameObject);
            } else
            {
                mapData.characters.Remove(gameObject);
                PhaseManager.numFriendlies--;
                Destroy(gameObject);
            }
        }
	}
}
