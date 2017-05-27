using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Phase { Idle, Moving, Attacking};
public class Movement : MonoBehaviour {

    Phase characterPhase;

    public GameObject indicator;

    MapData mapData;
    public GameObject Map;
    public List<GameObject> reachableTiles, inRange;
    int moves = 2, range = 1;

	void Start () {
        characterPhase = Phase.Idle;
        mapData = Map.GetComponent<MapData>();
        reachableTiles = new List<GameObject>();
        inRange = new List<GameObject>();
	}
	
	void Update () {
        if(characterPhase == Phase.Moving)
        {
            Move();
        } else if(characterPhase == Phase.Attacking)
        {
            foreach(GameObject tile in reachableTiles)
            {
                tile.GetComponent<Renderer>().material.color = Color.white;
            }
            Attack();
        }
        if(Input.GetMouseButtonDown(0) && characterPhase == Phase.Idle)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
                if(hit.transform.position == transform.position)
                {
                    foreach(GameObject tile in mapData.tiles)
                    {
                        if (tile.transform.position.y == 0)
                            mapData.tileInfo[tile.transform.position] = Mathf.Abs(tile.transform.position.x - transform.position.x) + Mathf.Abs(tile.transform.position.z - transform.position.z);
                        else if (tile.transform.position.y == 0.25f)
                            mapData.tileInfo[tile.transform.position] = Mathf.Abs(tile.transform.position.x - transform.position.x) + Mathf.Abs(tile.transform.position.z - transform.position.z) + 0.25f;
                        else if (tile.transform.position.y == 0.5f)
                            mapData.tileInfo[tile.transform.position] = Mathf.Abs(tile.transform.position.x - transform.position.x) + Mathf.Abs(tile.transform.position.z - transform.position.z) + 0.5f;

                            if (mapData.tileInfo[tile.transform.position] <= moves)
                            {
                                reachableTiles.Add(tile);
                                tile.GetComponent<Renderer>().material.color = Color.black;
                            }
                        characterPhase = Phase.Moving;
                    }
                }
            }
        }
	}

    void Move()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                foreach(GameObject tile in reachableTiles)
                {
                    if(tile.transform.position == hit.transform.position)
                    {
                        characterPhase = Phase.Attacking;
                        transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                        break;
                    }
                }
            }
        }
    }

    void Attack()
    {
        foreach(GameObject enemy in mapData.enemies)
        {
            if (enemy.transform.position.y == 1)
                mapData.enenmyInfo[enemy.transform.position] = Mathf.Abs(enemy.transform.position.x - transform.position.x) + Mathf.Abs(enemy.transform.position.z - transform.position.z);
            else if (enemy.transform.position.y == 1.25f)
                mapData.enenmyInfo[enemy.transform.position] = Mathf.Abs(enemy.transform.position.x - transform.position.x) + Mathf.Abs(enemy.transform.position.z - transform.position.z) + 0.25f;
            else if (enemy.transform.position.y == 1.5f)
                mapData.enenmyInfo[enemy.transform.position] = Mathf.Abs(enemy.transform.position.x - transform.position.x) + Mathf.Abs(enemy.transform.position.z - transform.position.z) + 0.5f;

            if (mapData.enenmyInfo[enemy.transform.position] <= range)
            {
                Vector3 Pos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1, enemy.transform.position.z);
                Instantiate(indicator, Pos, Quaternion.identity);
                Debug.Log(mapData.enenmyInfo[enemy.transform.position]);
            }
            characterPhase = Phase.Idle;
        }
    }
}
