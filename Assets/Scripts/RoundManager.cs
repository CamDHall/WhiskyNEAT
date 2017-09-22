using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {

	public Transform p1_HeroTile, p1_FollowerTile1, p1_FollowerTile2, p1_FollowerTile3;
    public Transform p2_HeroTile, p2_FollowerTile1, p2_FollowerTile2, p2_FollowerTile3;

    private void Start()
    {
        Vector3 Pos = new Vector3(0, 0, 1);
        // Get prefab from resources
        Debug.Log(PlayerInfo.heroPlayer1);
        GameObject p1_prefabHero = Resources.Load("Characters/" + PlayerInfo.heroPlayer1, typeof(GameObject)) as GameObject;
        GameObject p2_prefabHero = Resources.Load("Characters/" + PlayerInfo.heroPlayer2, typeof(GameObject)) as GameObject;

        GameObject p1_Hero = Instantiate(p1_prefabHero, Pos, Quaternion.identity, p1_HeroTile);
        GameObject p2_Hero = Instantiate(p2_prefabHero, Pos, Quaternion.identity, p2_HeroTile);

        foreach (string character in PlayerInfo.p1_followersName)
        {
            GameObject prefabObj = Resources.Load("Characters/"+ character, typeof(GameObject)) as GameObject;
            Instantiate(prefabObj);
        }
    }
}
