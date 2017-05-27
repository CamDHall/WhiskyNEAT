using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour {

    public Dictionary<Vector3, float> tileInfo;
    public Dictionary<Vector3, float> enenmyInfo;

    public GameObject[] tiles;
    public GameObject[] enemies;

	void Start () {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        tileInfo = new Dictionary<Vector3, float>();
        enenmyInfo = new Dictionary<Vector3, float>();

        // Enemy
        foreach(GameObject enemy in enemies)
        {
            enenmyInfo.Add(enemy.transform.position, enemy.transform.position.y);
        }

        foreach(GameObject tile in tiles)
        {
            tileInfo.Add(tile.transform.position, 1 + tile.transform.position.y);
        }
	}
	
	void Update () {
		
	}
}
