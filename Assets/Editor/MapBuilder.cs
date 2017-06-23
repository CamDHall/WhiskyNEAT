using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapBuilder : ScriptableWizard {

    public int width, height, noise;
    public Material grass, stone, dirt;
    public GameObject tilePrefab;

    [MenuItem ("My Tools/Map Builder")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<MapBuilder>("Create Map", "Create");
    }

    void OnWizardCreate()
    {
        GameObject map = new GameObject();
        MapData mapData = map.AddComponent<MapData>();
        map.gameObject.name = "Map";

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                Vector3 Pos = new Vector3((- width / 2) + x, 0, (-height / 2) + z);
                var tile = Instantiate(tilePrefab, Pos, Quaternion.identity, map.transform);
                int choice = Random.Range(0, 3);
                if (choice == 0)
                    tile.GetComponent<Renderer>().material = grass;
                else if (choice == 1)
                    tile.GetComponent<Renderer>().material = stone;
                else
                    tile.GetComponent<Renderer>().material = dirt;
            }
        }
    }
}
