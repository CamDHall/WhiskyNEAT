using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapBuilder : ScriptableWizard {

    public string _mapName;

    public int width, height, noise;
    public Material grass, stone, dirt;
    public GameObject tilePrefab;

    [MenuItem ("My Tools/Map Builder")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<MapBuilder>("Create Map", "Create","Save Map");
    }

    void OnWizardCreate()
    {
        GameObject map = new GameObject();
        MapData mapData = map.AddComponent<MapData>();
        map.gameObject.name = _mapName;
        map.gameObject.tag = "Map";

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

    void OnWizardOtherButton()
    {
        if(Selection.activeTransform == null)
        {
            Debug.Log("Please Select a Map from the Scene to Save");
        } else
        {
            GameObject newMap = Selection.activeTransform.gameObject;

            foreach(Transform child in newMap.transform)
            {
                if (child.childCount != 0)
                {
                    foreach(Transform nestedChild in child.transform)
                    {
                        if(nestedChild.gameObject.tag == "Friend" || nestedChild.gameObject.tag == "Enemy")
                        {
                            child.parent = null;
                        }
                    }
                }
            }
            PrefabUtility.CreatePrefab("Assets/Assets/Prefabs/" + Selection.activeTransform.gameObject.name + ".prefab", newMap, ReplacePrefabOptions.Default);
        }
    }
}
