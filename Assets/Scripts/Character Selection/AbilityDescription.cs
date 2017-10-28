using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDescription : MonoBehaviour {

    CharacterData data;
    public CharacterTeam team;

    Text stats, abilityTxt, title;

    private void Start()
    {
        Text[] items = GetComponentsInChildren<Text>();

        stats = items[0];
        title = items[1];
        abilityTxt = items[2];

        abilityTxt.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == gameObject.tag)
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
        // Switch out text and turn off title
        title.enabled = false;
        stats.enabled = false;
        abilityTxt.enabled = true;

        // Set text
        CharacterData data = target.GetComponent<CharacterData>();
        abilityTxt.text = "<size=16><color=#3784C5FF>" + data.nameAbility1 + ":</color></size> " + data._description1 + "\n\n" +
            "<size=16><color=#3784C5FF>" + data.nameAbility2 + ":</color></size> " + data._description2 + "\n\n" +
            "<size=16><color=#3784C5FF>" + data.nameAbility3 + ":</color></size> " + data._description3;
    }

    void DisplayStatOff()
    {
        abilityTxt.enabled = false;
        title.enabled = true;
        stats.enabled = true;
    }
}
