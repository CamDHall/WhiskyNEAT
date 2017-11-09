using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public GameObject[] heroList = new GameObject[4];
    GameObject[] _heros = new GameObject[4];
    public int selectedHero = 0;
    public int player;
    Vector3 heroPos;

    // List of selected Followers
    public List<GameObject> selectedFollowers;

    public GameObject heroSelector, followerSelector;

    public GameObject p_FollowerStats;
    Text followerStatTxT;

    // UI Compents
    public Text stats, title;
    public GameObject readyButton;

    public bool playerReady;

    public void Start()
    {
        followerStatTxT = p_FollowerStats.GetComponentInChildren<Text>();

        playerReady = false;

        // Only select hero on first round
        if (PlayerInfo.rounds == 0)
        {
            if (player == 1)
            {
                heroPos = new Vector3(0, 5.5f, 0);
            }
            else
            {
                heroPos = new Vector3(0, -0.3f, 0);
            }
            for (int i = 0; i < heroList.Length; i++)
            {
                var hero = Instantiate(heroList[i], heroPos, heroList[i].transform.rotation);

                // Disable scripts on prefab
                hero.GetComponent<Attacking>().enabled = false;
                hero.GetComponent<Movement>().enabled = false;
                hero.GetComponent<CharacterMenu>().enabled = false;
                hero.GetComponent<BaseCharacter>().enabled = false;
                hero.name = heroList[i].name;

                // Parent to container and set tags
                if (player == 1)
                {
                    hero.transform.parent = PlayerInfo.p1_Container.transform;
                    hero.tag = "Friend";
                }
                else
                {
                    hero.transform.parent = PlayerInfo.p2_Container.transform;
                    hero.tag = "Enemy";
                }

                // Add to heros list
                _heros[i] = hero;
                if (i != 0)
                    _heros[i].SetActive(false);
                else
                    _heros[i].SetActive(true);
            }
        }

        // Spawn deck inactive
        if (player == 1)
        {
            for (int i = 0; i < PlayerInfo.deck1.Count; i++)
            {
                Vector3 Pos = new Vector3(-6 + (i * 3), 5, 0);
                var card = Instantiate(PlayerInfo.deck1[i], Pos, PlayerInfo.deck1[i].transform.rotation);
                // Disable scripts on prefab
                card.GetComponent<Attacking>().enabled = false;
                card.GetComponent<Movement>().enabled = false;
                card.GetComponent<CharacterMenu>().enabled = false;
                card.GetComponent<BaseCharacter>().enabled = false;
                card.name = PlayerInfo.deck1[i].name; // Set Name to match prefab
                card.tag = "Friend";
                card.transform.parent = PlayerInfo.p1_Container.transform;

                PlayerInfo.deck1[i] = card;
                if (PlayerInfo.rounds == 0)
                {
                    PlayerInfo.deck1[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < PlayerInfo.deck2.Count; i++)
            {
                Vector3 Pos = new Vector3(-6 + (i * 3), -0.75f, 0);
                var card = Instantiate(PlayerInfo.deck2[i], Pos, PlayerInfo.deck2[i].transform.rotation);
                // Disable scripts on prefab
                card.GetComponent<Attacking>().enabled = false;
                card.GetComponent<Movement>().enabled = false;
                card.GetComponent<CharacterMenu>().enabled = false;
                card.GetComponent<BaseCharacter>().enabled = false;
                card.name = PlayerInfo.deck2[i].name; // Set Name to match prefab
                card.tag = "Enemy";
                card.transform.parent = PlayerInfo.p2_Container.transform;

                PlayerInfo.deck2[i] = card;
                if (PlayerInfo.rounds == 0)
                {
                    PlayerInfo.deck2[i].SetActive(false);
                }
            }
        }

        if(PlayerInfo.rounds != 0)
        {
            DisplayDeck();
            heroSelector.SetActive(false);
        }
    }

    private void Update()
    {
        // Show ready button when all followers are selected
        if(selectedFollowers.Count == 3 && !playerReady)
        {
            readyButton.SetActive(true);
        } else
        {
            readyButton.SetActive(false);
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
        title.text = "<size=26>" + data.characterName + "</size>" + "\n\n";
        // Stats
        stats.text = "<size=20>  Health:\t\t\t" + "<color=#3784C5FF><b>" + data.health + "</b></color>" + "\n  Courage:\t\t" + 
            "<color=#3784C5FF><b>" + data.courage + "</b></color>" + "\n  Moves:\t\t\t" + "<color=#3784C5FF><b>" + data.moves + "</b></color>" 
            + "\n  Melee:\t\t\t" + "<color=#3784C5FF><b>" + data.meleeStrength + "</b></color>" + "\n  Ranged:\t\t" + "<color=#3784C5FF><b>"
            + data.rangedStrength + "</b></color></size>";
    }

    public void ConfirmHero()
    {
        // Set heros and turn them off
        if (player == 1)
        {
            PlayerInfo.heroPlayer1 = _heros[selectedHero].name;
        } else
        {
            PlayerInfo.heroPlayer2 = _heros[selectedHero].name;
        }

        DisplayDeck();

        heroSelector.SetActive(false);
        _heros[selectedHero].SetActive(false);

    }

    void DisplayDeck()
    {
        // Set heros and turn them off
        if (player == 1)
        {
            PlayerInfo.p1_Container.GetComponent<Scrolling>().bar.gameObject.SetActive(true); // Scrollbar
            // Display deck
            for (int i = 0; i < PlayerInfo.deck1.Count; i++)
            {
                PlayerInfo.deck1[i].SetActive(true);
                p_FollowerStats.GetComponent<StatLocation>().playerCard = PlayerInfo.deck1[i];
                p_FollowerStats.GetComponentInChildren<ToggleFollower>().followerIndex = i;

                GameObject _followerStat = Instantiate(p_FollowerStats, Vector3.zero, Quaternion.identity, followerSelector.transform);
                _followerStat.tag = "Friend";
                CharacterData data = PlayerInfo.deck1[i].GetComponent<CharacterData>();

                _followerStat.GetComponent<AbilityDescription>().DisplayStats(data);
            }
        }
        else
        {
            PlayerInfo.p2_Container.GetComponent<Scrolling>().bar.gameObject.SetActive(true); // Scrollbar

            // Display deck
            for (int i = 0; i < PlayerInfo.deck2.Count; i++)
            {
                PlayerInfo.deck2[i].SetActive(true);

                p_FollowerStats.GetComponentInChildren<ToggleFollower>().followerIndex = i;
                p_FollowerStats.GetComponent<StatLocation>().playerCard = PlayerInfo.deck2[i];

                GameObject _followerStat = Instantiate(p_FollowerStats, Vector3.zero, Quaternion.identity, followerSelector.transform);
                _followerStat.tag = "Enemy";
                CharacterData data = PlayerInfo.deck2[i].GetComponent<CharacterData>();

                p_FollowerStats.GetComponent<AbilityDescription>().DisplayStats(data);
            }
        }
    }
}
