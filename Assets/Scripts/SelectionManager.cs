using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour {

    public PlayerManager p1Manager, p2Manager;
	
	// Update is called once per frame
	void Update () {
		if(p1Manager.playerReady && p2Manager.playerReady)
        {
            SceneManager.LoadScene("Round");
        }
	}
}
