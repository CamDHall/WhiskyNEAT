using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLocation : MonoBehaviour {

    RectTransform rt;
    public GameObject playerCard;
    public BoxCollider bc;

    float y;

	// Use this for initialization
	void Start () {
        rt = GetComponent<RectTransform>();
        bc = playerCard.GetComponent<BoxCollider>();
        y = bc.bounds.min.y - 1.5f;
    }
	
	// Update is called once per frame
	void Update () {
        
        float x = playerCard.transform.position.x + 0.25f;
        Vector3 statPos = new Vector3(x , y, 0);

        rt.position = Camera.main.WorldToScreenPoint(statPos);
    }
}
