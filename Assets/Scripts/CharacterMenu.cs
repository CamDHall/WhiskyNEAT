﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterMenu : MonoBehaviour {

    public GameObject meleeButton, rangedButton;
    public Button ability1, ability2, ability3;
    BaseCharacter baseCharacter;

    Image indicator;
    List<Image> imgs;
    Canvas worldCanvas, ScreenCanvas;

    RectTransform menu;
    GameObject abilityBar;
    Animator bar;
    void Start()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        worldCanvas = GameObject.FindGameObjectWithTag("worldCanvas").GetComponent<Canvas>();
        ScreenCanvas = GameObject.FindGameObjectWithTag("ScreenCanvas").GetComponent<Canvas>();

        indicator = Resources.Load("UI/IndicatorImage", typeof(Image)) as Image;

        RectTransform temp = Resources.Load("Characters/Menus/Menu", typeof(RectTransform)) as RectTransform;

        menu = Instantiate(temp);
        menu.SetParent(ScreenCanvas.transform);
        imgs = new List<Image>();

        // Set actions
        Button[] btns = menu.GetComponentsInChildren<Button>();

        btns[0].onClick.AddListener(FirstAbility);
        btns[1].onClick.AddListener(SecondAbility);
        btns[2].onClick.AddListener(ThirdAbility);
        //

        if (baseCharacter.characterData.rangedDistance == 0)
            rangedButton = null;

        if (rangedButton != null)
            rangedButton.GetComponent<Button>().onClick.AddListener(RangedButton);


        if(menu.transform.childCount == 3)
        {
            // Assign buttons from menu
            abilityBar = menu.transform.GetChild(2).gameObject;
            meleeButton = menu.transform.GetChild(0).gameObject;
            rangedButton = menu.transform.GetChild(1).gameObject;
        } else
        {
            // Assign buttons from menu
            abilityBar = menu.transform.GetChild(1).gameObject;
            meleeButton = menu.transform.GetChild(0).gameObject;
        }

        ability1 = abilityBar.transform.GetChild(0).gameObject.GetComponent<Button>();
        ability2 = abilityBar.transform.GetChild(1).gameObject.GetComponent<Button>();
        ability3 = abilityBar.transform.GetChild(2).gameObject.GetComponent<Button>();

        // Assign actions to Melee and Ranged
        meleeButton.GetComponent<Button>().onClick.AddListener(MeleeButton);
        if(rangedButton != null)
            rangedButton.GetComponent<Button>().onClick.AddListener(RangedButton);

        // Ability names
        ability1.GetComponentInChildren<Text>().text = baseCharacter.characterData.nameAbility1;
        ability2.GetComponentInChildren<Text>().text = baseCharacter.characterData.nameAbility2;
        ability3.GetComponentInChildren<Text>().text = baseCharacter.characterData.nameAbility3;


        switch (gameObject.name)
        {
            case "George Hammerschmidt":
                ability1.onClick.AddListener(GeorgeHammerschmidt.action1);
                ability2.onClick.AddListener(GeorgeHammerschmidt.action2);
                ability3.onClick.AddListener(GeorgeHammerschmidt.action3);
                break;
            case "Demetri Wolfgang":
                ability1.onClick.AddListener(DemetriWolfgang.action1);
                ability2.onClick.AddListener(DemetriWolfgang.action2);
                ability3.onClick.AddListener(DemetriWolfgang.action3);
                break;
            case "Garry Nation":
                ability1.onClick.AddListener(GarryNation.action1);
                ability2.onClick.AddListener(GarryNation.action2);
                ability3.onClick.AddListener(GarryNation.action3);
                break;
            case "Chang":
                ability1.onClick.AddListener(Chang.action1);
                ability2.onClick.AddListener(Chang.action2);
                ability3.onClick.AddListener(Chang.action3);
                break;
        }

        bar = menu.GetComponent<Animator>();
    }

    public void DisplayActionBar()
    {
        bar.SetBool("On", true);
        if (gameObject.tag == "Friend")
        {
            // Turn melee on and off
            if (baseCharacter.attacking._enemiesInMeleeRange.Count == 0)
            {
                bar.SetBool("MeleeOn", false);
            }
            else
            {
                bar.SetBool("MeleeOn", true);
            }
            // Turn ranged on and off
            if (baseCharacter.attacking._enemiesInRangedRange.Count == 0 && baseCharacter.characterData.rangedDistance != 0)
                bar.SetBool("RangedOn", false);
            else if (baseCharacter.characterData.rangedDistance != 0)
                bar.SetBool("RangedOn", true);
        } else
        {
            if (baseCharacter.attacking._friendsInMeleeRange.Count == 0)
                bar.SetBool("MeleeOn", false);
            else
                bar.SetBool("MeleeOn", true);

            if (baseCharacter.attacking._friendsInRangedRange.Count == 0 && baseCharacter.characterData.rangedDistance != 0)
                bar.SetBool("RangedOn", false);
            else if(baseCharacter.characterData.rangedDistance != 0)
                bar.SetBool("RangedOn", true);
        }
    }

    public void DisplayOff()
    {
        bar.SetBool("On", false);
        bar.SetBool("RangedOn", false);
        bar.SetBool("MeleeOn", false);
    }

    public void MeleeButton()
    {
        GameManager.Instance.selectedBaseCharacter.attacking.damageAmount = GameManager.Instance.selectedCharacterData.meleeStrength;
        if(gameObject.tag == "Friend")
        {
            foreach(GameObject target in baseCharacter.attacking._enemiesInMeleeRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z);
                Image img = Instantiate(indicator, Pos, indicator.transform.rotation);
                img.transform.SetParent(worldCanvas.transform);
                imgs.Add(img);
            }
        } else
        {
            foreach (GameObject target in baseCharacter.attacking._friendsInMeleeRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z);
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
        GameManager.Instance.selectedBaseCharacter.attacking.damageAmount = GameManager.Instance.selectedCharacterData.rangedStrength;
        if (gameObject.tag == "Friend")
        {
            foreach (GameObject target in baseCharacter.attacking._enemiesInRangedRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z);
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
        if (GameManager.confirmationState == Confirmation.Idle)
        {
            baseCharacter.AbilityOne();
            ability1.interactable = false;
        }
    }

    public void SecondAbility()
    {
        if (GameManager.confirmationState == Confirmation.Idle)
        {
            baseCharacter.AbilityTwo();
            ability2.interactable = false;
        }
    }

    public void ThirdAbility()
    {
        if (GameManager.confirmationState == Confirmation.Idle)
        {
            baseCharacter.AbilityThree();
            ability3.interactable = false;
        }
    }
}
