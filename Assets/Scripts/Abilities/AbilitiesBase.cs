using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesBase : MonoBehaviour {

    public void Mana(int mana)
    {
        GetComponent<BaseCharacter>().mana -= mana;
    }
}
