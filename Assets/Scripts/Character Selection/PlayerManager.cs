using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public GameObject[] heroList = new GameObject[4];
    GameObject[] _heros = new GameObject[4];
    GameObject[] deck1, deck2;
    public int selectedHero = 0, player = 0;
    Vector3 heroPos;

    public PlayerInfo playerInfo;

    public GameObject heroSelector, followerSelector;
    public Text followerStatPrefab;

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

        deck1 = new GameObject[playerInfo.deckPlayer1.Count];
        deck2 = new GameObject[playerInfo.deckPlayer2.Count];

        // Spawn deck inactive
        for(int i = 0; i < deck1.Length; i++)
        {
            Vector3 Pos = new Vector3(-6 + (i * 3), 4, 0);
            var card = Instantiate(playerInfo.deckPlayer1[i], Pos, playerInfo.deckPlayer1[i].transform.rotation);
            deck1[i] = card;
            deck1[i].SetActive(false);
        }

        for(int i = 0; i < deck2.Length; i++)
        {
            Vector3 Pos = new Vector3(-6 + (i * 3), -4, 0);
            var card = Instantiate(playerInfo.deckPlayer2[i], Pos, playerInfo.deckPlayer2[i].transform.rotation);
            deck2[i] = card;
            deck2[i].SetActive(false);
        }

        selectedHero = 0;
        DisplayStats(_heros[selectedHero].GetComponent<CharacterData>());
    }

    // For selecting followers
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (player == 1)
                {
                    foreach (GameObject follower in deck1)
                    {
                        if (hit.transform.position == follower.transform.position)
                        {
                            if (playerInfo.followerPlayer1.Count < 3)
                            {
                                if (playerInfo.followerPlayer1.Contains(follower))
                                {
                                    playerInfo.followerPlayer1.Remove(follower);
                                }
                                else
                                {
                                    playerInfo.followerPlayer1.Add(follower);
                                }
                            }
                            break;
                        }
                    }
                }
                else
                {
                    foreach (GameObject follower in deck2)
                    {
                        if (transform.position == follower.transform.position)
                        {
                            if (playerInfo.followerPlayer2.Count < 3)
                            {
                                if(playerInfo.followerPlayer2.Contains(follower))
                                {
                                    playerInfo.followerPlayer2.Remove(follower);
                                } else
                                {
                                    playerInfo.followerPlayer2.Add(follower);
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
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

    public void ConfirmHero()
    {
        // Set heros and turn them off
        if(player == 1)
        {
            playerInfo.heroPlayer1 = selectedHero;
            // Display deck
            for(int i = 0; i < deck1.Length; i++)
            {
                deck1[i].SetActive(true);
                Vector3 statPos = new Vector3(-515 + (i * 275), 0, 0);
                Text stat = Instantiate(followerStatPrefab, Vector3.zero, Quaternion.identity, followerSelector.transform);
                stat.GetComponent<RectTransform>().anchoredPosition = statPos;
                CharacterData data = deck1[i].GetComponent<CharacterData>();
                stat.text = data.characterName + "\nHealth: \t" + data.health + "\nCourage: \t" + data.courage + "\nMoves: \t" + data.moves + "\nMelee Strength: \t" + data.meleeStrength
            + "\nRanged Strength: \t" + data.rangedStrength;
            }
        } else
        {
            playerInfo.heroPlayer2 = selectedHero;
            foreach (GameObject card in deck1)
            {
                card.SetActive(true);
            }
        }

        heroSelector.SetActive(false);
        _heros[selectedHero].SetActive(false);

    }
}
