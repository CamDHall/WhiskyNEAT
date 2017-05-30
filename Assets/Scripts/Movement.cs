using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    MapData mapData;
    public GameObject Map;
    public List<GameObject> reachableTiles, inRange;
    public int moves;
    int startingMoves, range = 1;

	void Start () {
        PhaseManager.characterPhase = Phase.Moving;
        mapData = Map.GetComponent<MapData>();
        reachableTiles = new List<GameObject>();
        inRange = new List<GameObject>();
        startingMoves = moves;
	}
	
	void Update () {
        if(PhaseManager.characterPhase == Phase.Moving)
        {
            if (moves > 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Move();
                }
            } else
            {
                PhaseManager.numMoved++;
            }
        }

        if (PhaseManager.characterPhase == Phase.Attacking)
        {
            GetComponent<Attacking>().enabled = true;
        }
	}

    void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.position == transform.position)
            {
                foreach (GameObject tile in mapData.tiles)
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
                }
            }

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject tile in reachableTiles)
                {
                    if (tile.transform.position == hit.transform.position && !(hit.transform.position.x == transform.position.x && hit.transform.position.z == transform.position.z))
                    {
                        moves -= (int)mapData.tileInfo[hit.transform.position];
                        transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                        // Reset tile colors
                        foreach (GameObject oldTile in reachableTiles)
                        {
                            oldTile.GetComponent<Renderer>().material.color = Color.white;
                        }
                        break;
                    }
                }
            }
        }
    }
}
