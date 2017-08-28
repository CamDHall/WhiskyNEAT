using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public GameObject[] heroList = new GameObject[4];
    GameObject[] _heros = new GameObject[4];
    public int selectedHero = 0, player = 0;
    Vector3 heroPos;

    // UI Compents
    public Text stats;

    private void Start()
    {
        if(player == 1)
        {
            heroPos = new Vector3(0, 4.5f, 0);
        } else
        {
            heroPos = new Vector3(0, -2, 0);
        }
        for(int i = 0; i < heroList.Length; i++)
        {
            var hero = Instantiate(heroList[i], heroPos, heroList[i].transform.rotation);
            _heros[i] = hero;
            if (i != 0)
                _heros[i].SetActive(false);
            else
                _heros[i].SetActive(true);
        } 

        selectedHero = 0;
        DisplayStats(_heros[selectedHero].GetComponent<CharacterData>());
    }

    public void NextCharacter()
    {
        // Set index for current character
        if(selectedHero < heroList.Length -1)
        {
            selectedHero++;

        } else
        {
            selectedHero = 0;
        }

        // Change the displayed character
        for(int i = 0; i < heroList.Length; i++)
        {
            if(i != selectedHero)
            {
                _heros[i].SetActive(false);
            } else
            {
                _heros[i].SetActive(true);
            }
        }

        DisplayStats(_heros[selectedHero].GetComponent<CharacterData>());
    }

    public void BackCharacter()
    {
        // Set index for current character
        if (selectedHero > 0)
        {
            selectedHero--;
        }
        else
        {
            selectedHero = _heros.Length - 1;
        }

        // Change the displayed character
        for (int i = 0; i < heroList.Length; i++)
        {
            if (i != selectedHero)
            {
                _heros[i].SetActive(false);
            }
            else
            {
                _heros[i].SetActive(true);
            }
        }
        DisplayStats(_heros[selectedHero].GetComponent<CharacterData>());
    }

    void DisplayStats(CharacterData data)
    {
        stats.text = data.characterName + "\nHealth: \t" + data.health + "\nCourage: \t" + data.courage + "\nMoves: \t" + data.moves + "\nMelee Strength: \t" + data.meleeStrength
            + "\nRanged Strength: \t" + data.rangedStrength;
    }
}
