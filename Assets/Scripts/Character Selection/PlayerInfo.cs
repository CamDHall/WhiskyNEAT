using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public static PlayerInfo Instance;

    public int numberOfFollowers = 3;

    public static string heroPlayer1, heroPlayer2;

    // Save for each round
    public static List<GameObject> deck1 = new List<GameObject>();
    public static List<GameObject> deck2 = new List<GameObject>();

    // For scene saving
    public List<GameObject> p_deck1, p_deck2;

    public List<GameObject> followersPlayer1, followersPlayer2;

    public GameObject p1_Container, p2_Container;

    // Round info
    public static List<string> p1_FollowersName = new List<string>(), p2_FollowersName = new List<string>();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(Instance);

        if(deck1.Count == 0 & deck2.Count == 0)
        {
            deck1 = p_deck1;
            deck2 = p_deck2;
        } else
        {
            foreach(GameObject card in deck1)
            {
                if (!p_deck1.Contains(card))
                    p_deck1.Add(card);
            }

            foreach(GameObject card in deck2)
            {
                if (!p_deck2.Contains(card))
                    p_deck2.Add(card);
            }
        }
    }
}
