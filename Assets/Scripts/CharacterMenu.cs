using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : BaseCharacter {

    public GameObject menu;
    BaseCharacter baseCharacter;

    public Image indicator;
    List<Image> imgs;
    public Canvas worldCanvas;

    void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
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

        HandleState(State.Attacking);
        attacking.type = "Melee";
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

        HandleState(State.Attacking);
        attacking.type = "Ranged";
    }

    public void OverlayOff()
    {
        foreach(Image img in imgs)
        {
            Destroy(img);
        }

        imgs.Clear();
    }
}
