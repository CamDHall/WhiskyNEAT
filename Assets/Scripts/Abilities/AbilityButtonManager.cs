using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public int count = 0;

    Text abilityInfoTxt;

    void Start()
    {
        abilityInfoTxt = UIManager.Instance.abilityInfo.GetComponentInChildren<Text>();
    }

    public void ResetButtons(CharacterData data)
    {
        if(!GetComponent<Button>().interactable && count >= 3)
        {
            GetComponent<Button>().interactable = true;
            count = 0;

            if(data.buttonsIP.Contains(GetComponent<Button>()))
            {
                data.buttonsIP.Remove(GetComponent<Button>());
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.abilityInfo.SetActive(true);

        if (gameObject.name == "Ability1")
            abilityInfoTxt.text = GameManager.selectedCharacterData._description1;
        else if (gameObject.name == "Ability2")
            abilityInfoTxt.text = GameManager.selectedCharacterData._description2;
        else
            abilityInfoTxt.text = GameManager.selectedCharacterData._description3;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.abilityInfo.SetActive(false);
    }
}
