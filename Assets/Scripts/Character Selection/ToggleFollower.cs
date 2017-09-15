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
            // Check if selected characters is less than the maximum size, insure no duplicates are added
            if(playerManager.player == 1)
            {
                if(playerManager.selectedFollowers.Count == PlayerInfo.Instance.numberOfFollowers)
                {
                    GetComponent<Toggle>().isOn = false;
                } else if (!playerManager.selectedFollowers.Contains(PlayerInfo.Instance.deck1[followerIndex]) && playerManager.selectedFollowers.Count <= PlayerInfo.Instance.numberOfFollowers)
                {
                    playerManager.selectedFollowers.Add(PlayerInfo.Instance.deck1[followerIndex]);
                }
            } else
            {
                if (!playerManager.selectedFollowers.Contains(PlayerInfo.Instance.deck2[followerIndex]) && playerManager.selectedFollowers.Count <= PlayerInfo.Instance.numberOfFollowers)
                {
                    playerManager.selectedFollowers.Add(PlayerInfo.Instance.deck2[followerIndex]);
                } else
                {
                    GetComponent<Toggle>().isOn = false;
                }
            }
            // Debug.Log(followerIndex);
        }
    }
}
