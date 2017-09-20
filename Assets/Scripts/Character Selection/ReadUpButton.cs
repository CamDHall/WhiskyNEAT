using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadUpButton : MonoBehaviour {

    public void ReadyUp()
    {
        GetComponent<PlayerManager>().playerReady = true;
    }
}
