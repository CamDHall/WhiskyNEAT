using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleFollower : MonoBehaviour {

    public int followerIndex;
    PlayerManager playerManager;

    private void Start()
    {
        playerManager = transform.parent.GetComponentInParent<PlayerManager>();
    }

    private void Update()
    {
        if(GetComponent<Toggle>().isOn)
        {
            if(playerManager.player == 1)
            {

            } else
            {

            }
            // Debug.Log(followerIndex);
        }
    }
}
