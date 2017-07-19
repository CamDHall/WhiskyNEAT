﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {

    public GameObject menu, meleeButton, rangedButton;
    public Button ability1, ability2, ability3;
    BaseCharacter baseCharacter;

    public Image indicator;
    List<Image> imgs;
    public Canvas worldCanvas;

    void Start()
    {
        baseCharacter = GetComponent<BaseCharacter>();

        menu.SetActive(false);
        imgs = new List<Image>();

        if (baseCharacter.characterData.rangedDistance == 0)
            rangedButton = null;
    }

    void Update()
    {
        if(menu.activeSelf == true && GameManager.selectedCharacter != gameObject)
        {
            DisplayOff();
        }
    }

    public void DisplayActionBar()
    {
        menu.SetActive(true);
        if(gameObject.tag == "Friend")
        {
            //Debug.Log(baseCharacter.attacking._enemiesInMeleeRange.Count);
            // Turn melee on and off
            if (baseCharacter.attacking._enemiesInMeleeRange.Count == 0)
                meleeButton.SetActive(false);
            else
                meleeButton.SetActive(true);

            // Turn ranged on and off
            if (baseCharacter.attacking._enemiesInRangedRange.Count == 0 && baseCharacter.characterData.rangedDistance != 0)
                rangedButton.SetActive(false);
            else if(baseCharacter.characterData.rangedDistance != 0)
                rangedButton.SetActive(true);
        } else
        {
            if (baseCharacter.attacking._friendsInMeleeRange.Count == 0)
                meleeButton.SetActive(false);
            else
                meleeButton.SetActive(true);

            if (baseCharacter.attacking._friendsInRangedRange.Count == 0 && baseCharacter.characterData.rangedDistance != 0)
                rangedButton.SetActive(false);
            else if(baseCharacter.characterData.rangedDistance != 0)
                rangedButton.SetActive(true);
        }

        ability1.interactable = true;
        ability2.interactable = true;
        ability3.interactable = true;
    }

    public void DisplayOff()
    {
        menu.SetActive(false);
    }

    public void MeleeButton()
    {
        if(gameObject.tag == "Friend")
        {
            foreach(GameObject target in baseCharacter.attacking._enemiesInMeleeRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                imgs.Add(img);
            }
        } else
        {
            foreach (GameObject target in baseCharacter.attacking._friendsInMeleeRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                imgs.Add(img);
            }
        }

        baseCharacter.HandleState(State.Attacking);
        baseCharacter.attacking.type = "Melee";
    }

    public void RangedButton()
    {
        if (gameObject.tag == "Friend")
        {
            foreach (GameObject target in baseCharacter.attacking._enemiesInRangedRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                imgs.Add(img);
            }
        }
        else
        {
            foreach (GameObject target in baseCharacter.attacking._friendsInRangedRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                imgs.Add(img);
            }
        }

        baseCharacter.HandleState(State.Attacking);
        baseCharacter.attacking.type = "Ranged";
    }

    public void OverlayOff()
    {
        foreach(Image img in imgs)
        {
            Destroy(img);
        }

        imgs.Clear();
    }

    public void FirstAbility()
    {
        baseCharacter.AbilityOne();
        ability1.interactable = false;
        GameManager.selectedCharacterData.currentNumberofAttacks--;
    }

    public void SecondAbility()
    {
        baseCharacter.AbilityTwo();
        ability2.interactable = false;
        GetComponent<CharacterData>().currentNumberofAttacks--;
    }

    public void ThirdAbility()
    {
        baseCharacter.AbilityThree();
        ability3.interactable = false;
        GameManager.selectedCharacterData.currentNumberofAttacks--;
    }
}
