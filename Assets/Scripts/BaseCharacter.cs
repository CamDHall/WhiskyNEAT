using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {

    MapData mapData;
    public GameObject map;

    CharacterData characterData;

    void Start () {
        mapData = map.GetComponent<MapData>();
        characterData = GetComponent<CharacterData>();
	}
	
	void Update () {
        if(characterData.health <= 0)
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
