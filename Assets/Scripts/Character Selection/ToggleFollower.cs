using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleFollower : MonoBehaviour {

    public int followerIndex;
    PlayerManager playerManager;
    Toggle toggle;

    private void Start()
    {
        playerManager = transform.parent.GetComponentInParent<PlayerManager>();
        toggle = GetComponent<Toggle>();
    }

    private void Update()
    {
        if(toggle.isOn)
        {
            // Check if selected characters is less than the maximum size, insure no duplicates are added
            if (playerManager.selectedFollowers.Count >= playerManager.playerInfo.numberOfFollowers)
            {

                // if current isn't in list, toggle off
                if(playerManager.player == 1)
                {
                    if (!playerManager.selectedFollowers.Contains(playerManager.playerInfo.deck1[followerIndex]))
                    {
                        toggle.isOn = false;
                    }
                } else
                {
                    if (!playerManager.selectedFollowers.Contains(playerManager.playerInfo.deck2[followerIndex]))
                    {
                        toggle.isOn = false;
                    }
                }
            }
            else
            {
                if (playerManager.player == 1)
                {
                    if (!playerManager.selectedFollowers.Contains(playerManager.playerInfo.deck1[followerIndex]))
                    {
                        playerManager.selectedFollowers.Add(playerManager.playerInfo.deck1[followerIndex]);
                    }
                }
                else
                {
                    if (!playerManager.selectedFollowers.Contains(playerManager.playerInfo.deck2[followerIndex]))
                    {
                        playerManager.selectedFollowers.Add(playerManager.playerInfo.deck2[followerIndex]);
                    }
                }
            }
        } else // Remove from selected if stil contained 
        {
            if(playerManager.player == 1 && playerManager.selectedFollowers.Contains(playerManager.playerInfo.deck1[followerIndex]))
            {
                playerManager.selectedFollowers.Remove(playerManager.playerInfo.deck1[followerIndex]);
            } else if (playerManager.player == 2 && playerManager.selectedFollowers.Contains(playerManager.playerInfo.deck2[followerIndex]))
            {
                playerManager.selectedFollowers.Remove(playerManager.playerInfo.deck2[followerIndex]);
            }
        }
        }
}
