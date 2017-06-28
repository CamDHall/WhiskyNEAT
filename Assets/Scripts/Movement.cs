using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {


    // Objects
    public Attacking attacking;
    public MapData mapData;
    public BaseCharacter baseCharacter;

    public List<GameObject> reachableTiles, inRange;
    public float moves;
    public float startingMoves;
    public int range = 1;

    public bool moveAdded = false;

	void Start () {
        PhaseManager.characterPhase = Phase.Moving;
        reachableTiles = new List<GameObject>();
        inRange = new List<GameObject>();

        moves = baseCharacter.moves;
        startingMoves = moves;
	}
	
	void Update () {
        if(PhaseManager.characterPhase == Phase.Moving)
        {
            attacking.numOfAttacks = attacking.startingNumAttacks;
            attacking.attackAdded = false;
            attacking.targetsListed = false;

            if (moves >= 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Move();
                }
            } else if(!moveAdded)
            {
                AddMovement();
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

        // Select character
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.position == transform.position)
            {
                foreach (GameObject tile in mapData.tiles)
                {
                    if (tile.transform.position.y == 0)
                    {
                        mapData.tileInfo[tile.transform.position] = Mathf.Abs(tile.transform.position.x - transform.position.x) + Mathf.Abs(tile.transform.position.z - transform.position.z);
                    }
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
            } else
            {
                ResetTiles();
            }

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject tile in reachableTiles)
                {
                    if (tile.transform.childCount <= 0)
                    {
                        if (tile.transform.position == hit.transform.position && !(hit.transform.position.x == transform.position.x && hit.transform.position.z == transform.position.z))
                        {
                            transform.parent = hit.transform;
                            moves -= (int)mapData.tileInfo[hit.transform.position];
                            transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                            // Reset tile colors
                            ResetTiles();
                        }
                    }
                }
            }
        }
    }

    void AddMovement()
    {
        moveAdded = true;
        PhaseManager.numMoved++;
    }

    public void ResetTiles()
    {
        foreach (GameObject oldTile in reachableTiles)
        {
            oldTile.GetComponent<Renderer>().material.color = oldTile.GetComponent<SampleColors>().oldColor;
        }
    }
}
