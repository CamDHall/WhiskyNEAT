using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfo {

    public static int numberOfFollowers = 3;

    public static int rounds = 0;

    public static string heroPlayer1, heroPlayer2;

    public static List<string> s_deck1 = new List<string>() { "Characters/Archer", "Characters/Tank", "Characters/Archer", "Characters/Tank", "Characters/Archer" };
    public static List<string> s_deck2 = new List<string>() { "Characters/Tank", "Characters/Archer", "Characters/Tank", "Characters/Archer", "Characters/Tank" };

    // Save for each round
    public static List<GameObject> deck1 = new List<GameObject>();
    public static List<GameObject> deck2 = new List<GameObject>();

    // Reset each round
    public static List<GameObject> p1_captured = new List<GameObject>();
    public static List<GameObject> p2_captured = new List<GameObject>();

    static List<GameObject> _displayCaptured = new List<GameObject>();

    public static GameObject p1_Container, p2_Container;

    // Round info
    public static List<string> p1_FollowersName = new List<string>(), p2_FollowersName = new List<string>();

    // Display captured
    public static void DisplayCaptured()
    {
        /*
        if(GameManager.Instance.currentTeam == CharacterTeam.Friend)
        {
            for(int i = 0; i < p1_captured.Count; i++)
            {
                // Skip go that is already being displayed
                if(!_displayCaptured.Contains(p1_captured[i]))
                {
                    Vector3 Pos = new Vector3(2 + i, 4, 0);
                    GameObject temp = GameObject.Instantiate(p1_captured[i]);
                    temp.transform.parent = UIManager.Instance.captured.transform;
                    temp.transform.position = Pos;
                }
            }
        } else
        {
            for(int i = 0; i < p2_captured.Count; i++)
            {

            }
        }*/
    }
}
