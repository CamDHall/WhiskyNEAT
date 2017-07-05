using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {

    public GameObject menu;
    Attacking attacking;

    public Image indicator;
    List<Image> imgs;
    public Canvas worldCanvas;

    void Awake()
    {
        attacking = GetComponent<Attacking>();
        menu.SetActive(false);
        imgs = new List<Image>();
    }

    public void DisplayActionBar()
    {
        menu.SetActive(true);
    }

    public void DisplayOff()
    {
        menu.SetActive(false);
    }

    public void MeleeButton()
    {
        if(gameObject.tag == "Friend")
        {
            foreach(GameObject target in attacking._enemiesInMeleeRange)
            {
                Debug.Log("MELEE");
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                imgs.Add(img);
            }
        } else
        {
            foreach (GameObject target in attacking._friendsInMeleeRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                imgs.Add(img);
            }
        }
    }

    public void RangedButton()
    {
        if (gameObject.tag == "Friend")
        {
            foreach (GameObject target in attacking._enemiesInRangedRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                imgs.Add(img);
            }
        }
        else
        {
            foreach (GameObject target in attacking._friendsInRangedRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                imgs.Add(img);
            }
        }
    }
}
