using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour {

    private void Awake()
    {
        PlayerInfo.deck1.Clear();
        PlayerInfo.deck2.Clear();

        foreach(string name in PlayerInfo.s_deck1)
        {
            GameObject card = Resources.Load(name) as GameObject;
            PlayerInfo.deck1.Add(card);
        }

        foreach (string name in PlayerInfo.s_deck2)
        {
            GameObject card = Resources.Load(name) as GameObject;
            PlayerInfo.deck2.Add(card);
        }

        PlayerInfo.p1_Container = GameObject.FindGameObjectsWithTag("Container")[0];
        PlayerInfo.p2_Container = GameObject.FindGameObjectsWithTag("Container")[1];
    }
}
