using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonManager : MonoBehaviour {

    public int count = 0;

    AbilitiesBase abilitiesBase;

    private void Start()
    {
        abilitiesBase = GetComponent<AbilitiesBase>();
    }

    public void ResetButtons(CharacterData data)
    {
        if(!GetComponent<Button>().interactable && count >= 3)
        {
            GetComponent<Button>().interactable = true;
            count = 0;

            Debug.Log(abilitiesBase);

            if(data.buttonsIP.Contains(GetComponent<Button>()))
            {
                data.buttonsIP.Remove(GetComponent<Button>());
            }
        }
    }
}
