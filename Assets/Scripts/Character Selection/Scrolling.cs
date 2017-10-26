using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrolling : MonoBehaviour {

    public Scrollbar bar;

	void Start () {

	}
	
	void Update () {
        transform.position = new Vector3(-bar.value * 18, transform.position.y, 0);
	}
}
