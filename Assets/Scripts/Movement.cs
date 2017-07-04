using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public bool isMoving = false;
    MapData mapData;

	void Start () {
        mapData = GameObject.FindGameObjectWithTag("Map").GetComponent<MapData>();
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0) && isMoving && GameManager.selectedCharacter == this.gameObject)
        {   
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject tile in Paths.reachableTiles)
                {
                    if (tile.transform.childCount <= 0)
                    {
                        if (tile.transform.position == hit.transform.position && !(hit.transform.position.x == transform.position.x && hit.transform.position.z == transform.position.z))
                        {
                            transform.parent = hit.transform;
                            GameManager.selectedCharacterInfo.moves -= (int)MapData.tileInfo[hit.transform.position];
                            transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                            // Reset tile colors
                            Paths.ResetTiles();
                        }
                    } else if(hit.transform.position != GameManager.selectedCharacter.transform.position)
                    {
                        Paths.ResetTiles();
                    }
                }
            }
        }
    }
}
