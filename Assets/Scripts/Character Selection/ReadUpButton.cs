using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadUpButton : MonoBehaviour {

    public void ReadyUp()
    {
        PlayerManager playerManager = GetComponent<PlayerManager>();
        playerManager.playerReady = true;
        if(playerManager.player == 1)
        {
            foreach(GameObject character in playerManager.selectedFollowers)
            {
                //Debug.Log(character.name);
                PlayerInfo.p1_followersName.Add(character.name);
            }
        } else
        {
            foreach(GameObject character in playerManager.selectedFollowers)
            {
                PlayerInfo.p2_FollowersName.Add(character.name);
            }
        }
    }
}
