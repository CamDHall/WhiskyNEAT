using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public GameObject[] heroList = new GameObject[4];
    GameObject[] _heros = new GameObject[4];
    public int selectedHero = 0, player = 0;
    Vector3 heroPos;

    // List of selected Followers
    public List<GameObject> selectedFollowers;

    public PlayerInfo playerInfo;

    public GameObject heroSelector, followerSelector;
    public Text followerStatPrefab;

    // UI Compents
    public Text stats;

    private void Start()
    {
        playerInfo = GameObject.FindGameObjectWithTag("PlayerInfo").GetComponent<PlayerInfo>();
        if(player == 1)
        {
            heroPos = new Vector3(0, 4.5f, 0);
        } else
        {
            heroPos = new Vector3(0, -1.5f, 0);
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

        // Spawn deck inactive
        if (player == 1)
        {
            for (int i = 0; i < playerInfo.deck1.Count; i++)
            {
                Vector3 Pos = new Vector3(-6 + (i * 3), 4, 0);
                var card = Instantiate(playerInfo.deck1[i], Pos, playerInfo.deck1[i].transform.rotation);
                playerInfo.deck1[i] = card;
                playerInfo.deck1[i].SetActive(false);
            }
        } else
        {
            for (int i = 0; i < playerInfo.deck2.Count; i++)
            {
                Vector3 Pos = new Vector3(-6 + (i * 3), -1f, 0);
                Debug.Log(Pos.y);
                var card = Instantiate(playerInfo.deck2[i], Pos, playerInfo.deck2[i].transform.rotation);
                playerInfo.deck2[i] = card;
                playerInfo.deck2[i].SetActive(false);
            }
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

    void DisplayStats(Text stat, CharacterData data)
    {
        stat.text = data.characterName + "\nHealth: \t" + data.health + "\nCourage: \t" + data.courage + "\nMoves: \t" + data.moves + "\nMelee Strength: \t" + data.meleeStrength
            + "\nRanged Strength: \t" + data.rangedStrength;
    }

    public void ConfirmHero()
    {
        // Set heros and turn them off
        if(player == 1)
        {
            playerInfo.heroPlayer1 = selectedHero;
            // Display deck
            for(int i = 0; i < playerInfo.deck1.Count; i++)
            {
                playerInfo.deck1[i].SetActive(true);
                Vector3 statPos = new Vector3(-515 + (i * 275), 0, 0);
                Text stat = Instantiate(followerStatPrefab, Vector3.zero, Quaternion.identity, followerSelector.transform);
                stat.GetComponent<RectTransform>().anchoredPosition = statPos;
                CharacterData data = playerInfo.deck1[i].GetComponent<CharacterData>();
                DisplayStats(stat, data);
                stat.GetComponentInChildren<ToggleFollower>().followerIndex = i;
            }
        } else
        {
            playerInfo.heroPlayer2 = selectedHero;
            // Display deck
            for (int i = 0; i < playerInfo.deck2.Count; i++)
            {
                playerInfo.deck2[i].SetActive(true);
                Vector3 statPos = new Vector3(-515 + (i * 275), 0, 0);
                Text stat = Instantiate(followerStatPrefab, Vector3.zero, Quaternion.identity, followerSelector.transform);
                stat.GetComponent<RectTransform>().anchoredPosition = statPos;
                CharacterData data = playerInfo.deck2[i].GetComponent<CharacterData>();
                DisplayStats(stat, data);
                stat.GetComponentInChildren<ToggleFollower>().followerIndex = i;
            }
        }

        heroSelector.SetActive(false);
        _heros[selectedHero].SetActive(false);

    }
}
