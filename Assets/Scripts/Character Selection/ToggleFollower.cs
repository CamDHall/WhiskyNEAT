using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleFollower : MonoBehaviour {

    public int followerIndex;

    private void Update()
    {
        if(GetComponent<Toggle>().isOn)
        {
            Debug.Log(followerIndex);
        }
    }
}
