using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour {

    public static List<GameObject> reachableTiles = new List<GameObject>();

    public static void ChangeTiles()
    {
        if (GameManager.selectedCharacter != null && GameManager.selectedCharacterData.currentNumberofMoves > 0)
        {
            reachableTiles.Clear();
            Transform currentTransform = GameManager.selectedCharacter.transform; // For transform.position

            foreach (GameObject tile in MapData.tiles)
            {
                if (tile.transform.position.y == 0)
                {
                    MapData.tileInfo[tile.transform.position] = Mathf.Abs(tile.transform.position.x - currentTransform.position.x) + Mathf.Abs(tile.transform.position.z - currentTransform.position.z);
                }
                else if (tile.transform.position.y == 0.25f)
                    MapData.tileInfo[tile.transform.position] = Mathf.Abs(tile.transform.position.x - currentTransform.position.x) + Mathf.Abs(tile.transform.position.z - currentTransform.position.z) + 0.25f;
                else if (tile.transform.position.y == 0.5f)
                    MapData.tileInfo[tile.transform.position] = Mathf.Abs(tile.transform.position.x - currentTransform.position.x) + Mathf.Abs(tile.transform.position.z - currentTransform.position.z) + 0.5f;

                if (MapData.tileInfo[tile.transform.position] <= GameManager.selectedCharacterData.currentNumberofMoves &&
                    !(tile.transform.childCount > 0))
                {
                    reachableTiles.Add(tile);
                    tile.GetComponent<Renderer>().material.color = Color.black;
                }
            }
        }
    }

    public static void ResetTiles()
    {
        foreach (GameObject oldTile in reachableTiles)
        {
            oldTile.GetComponent<Renderer>().material.color = oldTile.GetComponent<SampleColors>().oldColor;
        }
    }
}
