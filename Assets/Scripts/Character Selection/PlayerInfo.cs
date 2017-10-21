using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfo {

    public static int numberOfFollowers = 3;

    public static int rounds = 0;

    public static string heroPlayer1, heroPlayer2;

    public static List<string> s_deck1 = new List<string>() { "Characters/GoodGuy", "Characters/BadGuy", "Characters/GoodGuy", "Characters/BadGuy", "Characters/GoodGuy" };
    public static List<string> s_deck2 = new List<string>() { "Characters/BadGuy", "Characters/GoodGuy", "Characters/BadGuy", "Characters/GoodGuy", "Characters/BadGuy" };

    // Save for each round
    public static List<GameObject> deck1 = new List<GameObject>();

    public static List<GameObject> deck2 = new List<GameObject>();

    public static GameObject p1_Container, p2_Container;

    // Round info
    public static List<string> p1_FollowersName = new List<string>(), p2_FollowersName = new List<string>();
}
