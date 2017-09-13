﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDescription : MonoBehaviour {

    CharacterData data;

    public GameObject hoverUI;

    // Update is called once per frame
    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "HoverUI")
            {
                DisplayStatOn(hit.transform.gameObject);
            }
        } else
        {
            DisplayStatOff();
        }
	}

    void DisplayStatOn(GameObject target)
    {
        // Activate and Position ui element for hover info
        hoverUI.SetActive(true);
        Vector2 Pos = new Vector2((target.transform.position.x - 1.5f) * 100, target.transform.position.y * 100);
        hoverUI.GetComponent<RectTransform>().anchoredPosition = Pos;

        // Set text
        CharacterData data = target.GetComponent<CharacterData>();
        Debug.Log(data);
        hoverUI.GetComponentInChildren<Text>().text = data.nameAbility1 + ": " + data._description1 + "\n" +
            data.nameAbility2 + ": " + data._description2 + "\n" +
            data.nameAbility3 + ": " + data._description3 + "\n";
    }

    void DisplayStatOff()
    {
        hoverUI.SetActive(false);
    }
}