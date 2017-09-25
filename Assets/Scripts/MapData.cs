using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour {

    public static Dictionary<Vector3, float> tileInfo;
    public static Dictionary<Vector3, float> enenmyInfo = new Dictionary<Vector3, float>();
    public static Dictionary<Vector3, float> friendsInfo = new Dictionary<Vector3, float>();

    public static GameObject[] tiles;

    // For spawning
    public List<Transform> p1_startingTiles, p2_startingTiles;
    [SerializeField]public static List<GameObject> enemies;
    [SerializeField]public static List<GameObject> friends;

	void Start() {
        // Declare and clear statics
        enemies = new List<GameObject>();
        friends = new List<GameObject>();

        tiles = GameObject.FindGameObjectsWithTag("Tile");
        tileInfo = new Dictionary<Vector3, float>();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy);
        }

        foreach(GameObject friend in GameObject.FindGameObjectsWithTag("Friend"))
        {
            friends.Add(friend);
        }

        // Init info
        FriendInfo();
        EnemyInfo();

        // Tiles
        foreach (GameObject tile in tiles)
        {
            tileInfo.Add(tile.transform.position, 1 + tile.transform.position.y);
        }
    }

    public static void EnemyInfo()
    {
        enenmyInfo.Clear();
        foreach (GameObject enemy in enemies)
        {
            enenmyInfo.Add(enemy.transform.position, enemy.transform.position.y + 1);
        }
    }

    public static void FriendInfo()
    {
        friendsInfo.Clear();
        foreach (GameObject character in friends)
        {
            friendsInfo.Add(character.transform.position, character.transform.position.y + 1);
        }
    }
}
