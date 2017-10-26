using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDescription : MonoBehaviour {

    CharacterData data;
    public CharacterTeam team;

    public GameObject hoverUI;

    // Update is called once per frame
    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "Friend" || hit.transform.tag == "Enemy")
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
        if (target.transform.tag == team.ToString())
        {
            hoverUI.SetActive(true);
            Vector2 Pos = new Vector2((target.transform.position.x) * 100, target.transform.position.y + 400);
            hoverUI.GetComponent<RectTransform>().anchoredPosition = Pos;

            // Set text
            CharacterData data = target.GetComponent<CharacterData>();
            hoverUI.GetComponentInChildren<Text>().text = data.nameAbility1 + ": " + data._description1 + "\n" +
                data.nameAbility2 + ": " + data._description2 + "\n" +
                data.nameAbility3 + ": " + data._description3 + "\n";
        }
    }

    void DisplayStatOff()
    {
        hoverUI.SetActive(false);
    }
}
