using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {

    public MapData map;

    private void Awake()
    {
        Vector3 Pos = new Vector3(0, 1, 0);
        // Get prefab from resources
        GameObject p1_prefabHero = Resources.Load("Characters/" + PlayerInfo.heroPlayer1, typeof(GameObject)) as GameObject;
        GameObject p2_prefabHero = Resources.Load("Characters/" + PlayerInfo.heroPlayer2, typeof(GameObject)) as GameObject;

        GameObject p1_Hero = Instantiate(p1_prefabHero, Pos, Quaternion.identity, map.p1_startingTiles[0]);
        GameObject p2_Hero = Instantiate(p2_prefabHero, Pos, Quaternion.identity, map.p2_startingTiles[0]);

        p1_Hero.transform.localPosition = Pos;
        p2_Hero.transform.localPosition = Pos;

        // Set Menu
        p1_Hero.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.heroPlayer1 + " Menu", typeof(RectTransform)) as RectTransform;
        p2_Hero.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.heroPlayer2 + " Menu", typeof(RectTransform)) as RectTransform;

        for (int i = 0; i < PlayerInfo.p1_followersName.Count; i++)
        {
            Pos = new Vector3(0, 0, 0);
            Debug.Log(Resources.Load("Characters/Menus/" + PlayerInfo.p1_followersName[i] + " Menu"));
            GameObject tempPrefab = Resources.Load("Characters/" + PlayerInfo.p1_followersName[i], typeof(GameObject)) as GameObject;
            GameObject follower = Instantiate(tempPrefab, Pos, Quaternion.identity, map.p1_startingTiles[i + 1]);
            follower.transform.localPosition = Pos;

            follower.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.p1_followersName[i] + " Menu", typeof(RectTransform)) as RectTransform;
        }
    }
}
