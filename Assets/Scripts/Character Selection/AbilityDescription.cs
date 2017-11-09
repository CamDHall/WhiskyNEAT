using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDescription : MonoBehaviour {

    CharacterData data;
    public CharacterTeam team;

    Text stats, abilityTxt, title;

    ToggleFollower toggle;

    private void Start()
    {
        Text[] items = GetComponentsInChildren<Text>();

        // Followers have nested elements for the toggle
        if (items[0].transform.childCount > 0)
        {
            stats = items[0];
            title = items[2];
            abilityTxt = items[3];

            // Toggle
            toggle = GetComponentInChildren<ToggleFollower>();
        }
        else
        {
            stats = items[0];
            title = items[1];
            abilityTxt = items[2];
        }
        abilityTxt.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // For hero hover
        if (toggle == null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == gameObject.tag)
                {
                    DisplayAbilities(hit.transform.gameObject);
                }
            }
            else
            {
                DisplayAbilitiesOff();
            }
        } else // For follower hover
        {
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.tag == gameObject.tag)
                {
                    if(hit.transform.tag == "Friend" && PlayerInfo.deck1.IndexOf(hit.transform.gameObject) == toggle.followerIndex)
                    {
                        DisplayAbilities(hit.transform.gameObject);
                    } else if(hit.transform.tag == "Enemy" && PlayerInfo.deck2.IndexOf(hit.transform.gameObject) == toggle.followerIndex)
                    {
                        DisplayAbilities(hit.transform.gameObject);
                    }
                }
            } else
            {
                DisplayAbilitiesOff();
            }
        }
	}

    public void DisplayStats(CharacterData data_in)
    {
        // Assignments for newly created follower prefab
        Text[] items = GetComponentsInChildren<Text>();

        // Followers have nested elements for the toggle
        stats = items[0];
        title = items[2];
        abilityTxt = items[3];


        abilityTxt.enabled = false;

        data = data_in;
        
        // Name
        title.text = "\t" + "<size=26>" + data.characterName + "</size>" + "\n\n";
        // Stats
        stats.text = "<size=20>  Health:\t\t\t" + "<color=#3784C5FF><b>" + data.health + "</b></color>" + "\n  Courage:\t\t" +
            "<color=#3784C5FF><b>" + data.courage + "</b></color>" + "\n  Moves:\t\t\t" + "<color=#3784C5FF><b>" + data.moves + "</b></color>"
            + "\n  Melee:\t\t\t" + "<color=#3784C5FF><b>" + data.meleeStrength + "</b></color>" + "\n  Ranged:\t\t" + "<color=#3784C5FF><b>"
            + data.rangedStrength + "</b></color></size>";
    }

    void DisplayAbilities(GameObject target)
    {
        // Switch out text and turn off title
        title.enabled = false;
        stats.enabled = false;
        abilityTxt.enabled = true;

        // Set text
        CharacterData data = target.GetComponent<CharacterData>();
        abilityTxt.text = "<size=20><color=#3784C5FF>" + data.nameAbility1 + ":</color></size> " + "<size=18>" + data._description1 + 
            "</size>\n <size=20><color=#3784C5FF>" + data.nameAbility2 + ":</color></size> " + "<size=18>" +data._description2 + 
            "</size>\n <size=20><color=#3784C5FF>" + data.nameAbility3 + ":</color></size> " + "<size=18>" + data._description3 + "</size>";
    }

    void DisplayAbilitiesOff()
    {
        abilityTxt.enabled = false;
        title.enabled = true;
        stats.enabled = true;
    }
}
