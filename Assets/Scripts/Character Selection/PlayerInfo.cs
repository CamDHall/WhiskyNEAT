using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public int numberOfFollowers = 3;

    public static string heroPlayer1, heroPlayer2;
    public List<GameObject> deck1, deck2;
    public List<GameObject> followersPlayer1, followersPlayer2;

    public GameObject p1_Container, p2_Container;

    // Round info
    public static List<string> p1_FollowersName = new List<string>(), p2_FollowersName = new List<string>();

    private void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(p1_Container);
        DontDestroyOnLoad(p2_Container);
    }
}
