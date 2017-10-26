using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLocation : MonoBehaviour {

    RectTransform rt;
    public GameObject playerCard;

    float y;

	// Use this for initialization
	void Start () {
        rt = GetComponent<RectTransform>();

        y = playerCard.transform.position.y - 1.25f;
    }
	
	// Update is called once per frame
	void Update () {
        
        float x = playerCard.transform.position.x;
        Vector3 statPos = new Vector3(x , y, 0);

        rt.position = Camera.main.WorldToScreenPoint(statPos);
    }
}
