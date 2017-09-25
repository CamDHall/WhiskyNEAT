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

        // Set Data
        p1_Hero.GetComponent<BaseCharacter>().characterData = p1_Hero.GetComponent<CharacterData>();
        p1_Hero.gameObject.tag = "Friend"; // Set team tag
        p2_Hero.GetComponent<BaseCharacter>().characterData = p2_Hero.GetComponent<CharacterData>();
        p2_Hero.gameObject.tag = "Enemy";

        // Set Menu
        p1_Hero.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.heroPlayer1 + " Menu", 
            typeof(RectTransform)) as RectTransform;

        p2_Hero.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.heroPlayer2 + " Menu", 
            typeof(RectTransform)) as RectTransform;

        // p1 Followers
        for (int i = 0; i < PlayerInfo.p1_FollowersName.Count; i++)
        {
            Pos = new Vector3(0, 0.5f, 0);
            GameObject tempPrefab = Resources.Load("Characters/" + PlayerInfo.p1_FollowersName[i], typeof(GameObject)) as GameObject;
            GameObject follower = Instantiate(tempPrefab, Pos, Quaternion.identity, map.p1_startingTiles[i + 1]);
            follower.transform.localPosition = Pos;
            follower.tag = "Friend"; // Set team tag

            // Set Data
            follower.GetComponent<BaseCharacter>().characterData = follower.GetComponent<CharacterData>();
            // Set Menu
            //Debug.Log(PlayerInfo.p1_FollowersName[i]);
            follower.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.p1_FollowersName[i] + " Menu", typeof(RectTransform)) as RectTransform;
        }

        // p2 Followers
        for (int i = 0; i < PlayerInfo.p2_FollowersName.Count; i++)
        {
            Pos = new Vector3(0, 0.5f, 0);
            GameObject tempPrefab = Resources.Load("Characters/" + PlayerInfo.p2_FollowersName[i], typeof(GameObject)) as GameObject;
            GameObject follower = Instantiate(tempPrefab, Pos, Quaternion.identity, map.p2_startingTiles[i + 1]);
            follower.transform.localPosition = Pos;
            follower.tag = "Enemy"; // Set team tag

            // Set Data
            follower.GetComponent<BaseCharacter>().characterData = follower.GetComponent<CharacterData>();
            // Set Menu
            follower.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.p2_FollowersName[i] + " Menu", typeof(RectTransform)) as RectTransform;
        }
    }
}
