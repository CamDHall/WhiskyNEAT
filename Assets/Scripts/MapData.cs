﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour {

    public Dictionary<Vector3, float> tileInfo;
    public Dictionary<Vector3, float> enenmyInfo;
    public Dictionary<Vector3, float> friendsInfo;

    public GameObject[] tiles;
    public List<GameObject> enemies;
    public List<GameObject> friends;

	void Start () {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        tileInfo = new Dictionary<Vector3, float>();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy);
        }

        enenmyInfo = new Dictionary<Vector3, float>();

        foreach(GameObject friend in GameObject.FindGameObjectsWithTag("Friend"))
        {
            friends.Add(friend);
        }

        friendsInfo = new Dictionary<Vector3, float>();

        // Enemy
        foreach (GameObject enemy in enemies)
        {
            enenmyInfo.Add(enemy.transform.position, enemy.transform.position.y);
        }

        // Character
        foreach(GameObject character in friends)
        {
            friendsInfo.Add(character.transform.position, character.transform.position.y);
        }

        // Tiles
        foreach (GameObject tile in tiles)
        {
            tileInfo.Add(tile.transform.position, 1 + tile.transform.position.y);
        }
    }
}
