using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesBase : MonoBehaviour {

    public string _abilityNumber;

    public void ManaManager(int cost)
    {
        GetComponent<BaseCharacter>().mana -= cost;
    }

	public void SetInfo(string number)
    {
        _abilityNumber = number;
    }

    public void Reset()
    {
        _abilityNumber = "empty";
    }

    void Update()
    {
        // Debug.Log(_abilityNumber);
    }
}
