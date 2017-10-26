using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (GameManager.selectedCharacter != this.transform.parent.gameObject)
        {
            GetComponent<Renderer>().enabled = false;
        } else
        {
            GetComponent<Renderer>().enabled = true;
        }
	}
}
