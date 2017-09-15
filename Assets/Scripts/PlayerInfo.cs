﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public static PlayerInfo Instance;
    public int numberOfFollowers = 3;

    public int heroPlayer1, heroPlayer2;
    public List<GameObject> deck1, deck2;
    public List<GameObject> followersPlayer1, followersPlayer2;
    private void Awake()
    {
        Instance = this;
    }
}
