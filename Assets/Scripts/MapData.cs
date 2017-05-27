using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour {

    public Dictionary<Vector3, float> tileInfo;
    public Dictionary<Vector3, float> enenmyInfo;
    public Dictionary<Vector3, float> characterInfo;

    public GameObject[] tiles;
    public GameObject[] enemies;
    public GameObject[] characters;

	void Start () {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        tileInfo = new Dictionary<Vector3, float>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enenmyInfo = new Dictionary<Vector3, float>();

        characters = GameObject.FindGameObjectsWithTag("Character");
        characterInfo = new Dictionary<Vector3, float>();

        // Enemy
        foreach (GameObject enemy in enemies)
        {
            enenmyInfo.Add(enemy.transform.position, enemy.transform.position.y);
        }

        // Character
        foreach(GameObject character in characters)
        {
            characterInfo.Add(character.transform.position, character.transform.position.y);
        }

        // Tiles
        foreach (GameObject tile in tiles)
        {
            tileInfo.Add(tile.transform.position, 1 + tile.transform.position.y);
        }
    }
	
	void Update () {
		
	}
}
