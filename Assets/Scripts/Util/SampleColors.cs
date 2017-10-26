using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleColors : MonoBehaviour {

    public Color grass, dirt, stone;
    public Color oldColor;

	void Start () {
        int choice = Random.Range(0, 3);

        if (choice == 0)
            GetComponent<Renderer>().material.color = grass;
        else if (choice == 1)
            GetComponent<Renderer>().material.color = dirt;
        else if (choice == 2)
            GetComponent<Renderer>().material.color = stone;

        oldColor = GetComponent<Renderer>().material.color;
		
	}
	
	void Update () {
		
	}
}
