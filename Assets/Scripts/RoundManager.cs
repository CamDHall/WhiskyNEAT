using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour {

    public MapData map;
    public Material mat_p1, mat_p2;

    List<GameObject> obj_follower1 = new List<GameObject>();
    List<GameObject> obj_follower2 = new List<GameObject>();

    private void Awake()
    {
        Vector3 Pos = new Vector3(0, 1, 0);
        // Get prefab from resources
        GameObject p1_prefabHero = Resources.Load("Characters/" + PlayerInfo.heroPlayer1, typeof(GameObject)) as GameObject;
        GameObject p2_prefabHero = Resources.Load("Characters/" + PlayerInfo.heroPlayer2, typeof(GameObject)) as GameObject;

        GameObject p1_Hero = Instantiate(p1_prefabHero, Pos, Quaternion.identity, map.p1_startingTiles[0]);
        GameObject p2_Hero = Instantiate(p2_prefabHero, Pos, Quaternion.identity, map.p2_startingTiles[0]);

        // Highlighting
        GenerateMaterial(p1_Hero.transform.GetChild(0).gameObject, 1);
        GenerateMaterial(p2_Hero.transform.GetChild(0).gameObject, 2);

        p1_Hero.transform.localPosition = Pos;
        p2_Hero.transform.localPosition = Pos;

        // Set Data
        p1_Hero.GetComponent<BaseCharacter>().characterData = p1_Hero.GetComponent<CharacterData>();
        p1_Hero.gameObject.tag = "Friend"; // Set team tag
        p2_Hero.GetComponent<BaseCharacter>().characterData = p2_Hero.GetComponent<CharacterData>();
        p2_Hero.gameObject.tag = "Enemy";

        // Set Menu
        p1_Hero.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.heroPlayer1 + " Menu", 
            typeof(RectTransform)) as RectTransform;

        p2_Hero.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.heroPlayer2 + " Menu", 
            typeof(RectTransform)) as RectTransform;

        // p1 Followers
        for (int i = 0; i < PlayerInfo.p1_FollowersName.Count; i++)
        {
            Pos = new Vector3(0, 0.5f, 0);
            GameObject tempPrefab = Resources.Load("Characters/" + PlayerInfo.p1_FollowersName[i], typeof(GameObject)) as GameObject;
            GameObject follower = Instantiate(tempPrefab, Pos, Quaternion.identity, map.p1_startingTiles[i + 1]);
            follower.transform.localPosition = Pos;
            follower.tag = "Friend"; // Set team tag

            obj_follower1.Add(follower);

            // Set Data
            follower.GetComponent<BaseCharacter>().characterData = follower.GetComponent<CharacterData>();
            // Set Menu
            follower.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.p1_FollowersName[i] + " Menu", typeof(RectTransform)) as RectTransform;
        }

        // p2 Followers
        for (int i = 0; i < PlayerInfo.p2_FollowersName.Count; i++)
        {
            Pos = new Vector3(0, 0.5f, 0);
            GameObject tempPrefab = Resources.Load("Characters/" + PlayerInfo.p2_FollowersName[i], typeof(GameObject)) as GameObject;
            GameObject follower = Instantiate(tempPrefab, Pos, Quaternion.identity, map.p2_startingTiles[i + 1]);
            follower.transform.localPosition = Pos;
            follower.tag = "Enemy"; // Set team tag

            obj_follower2.Add(follower);

            // Set Data
            follower.GetComponent<BaseCharacter>().characterData = follower.GetComponent<CharacterData>();
            // Set Menu
            follower.GetComponent<CharacterMenu>().menuPrefab = Resources.Load("Characters/Menus/" + PlayerInfo.p2_FollowersName[i] + " Menu", typeof(RectTransform)) as RectTransform;
        }

        // Set highlighters for follower
        GenerateMaterial(obj_follower1, 1);
        GenerateMaterial(obj_follower2, 2);

        // Reset selected followers names
        PlayerInfo.p1_FollowersName.Clear();
        PlayerInfo.p2_FollowersName.Clear();
    }

    public void NextRound()
    {
        if (PlayerInfo.rounds < 3)
        {
            SceneManager.LoadScene("CharacterSelection");
        } else
        {
            if(GameManager.Instance.currentTeam == CharacterTeam.Friend)
            {
                PlayerInfo.score_p1++;
            } else
            {
                PlayerInfo.score_p2++;
            }
        }
    }

    void GenerateMaterial(GameObject model, int pn)
    {
        // Assign color and mat, for current team
        Color h_color;
        Material h_mat;

        if(pn == 1)
        {
            h_color = mat_p1.GetColor("_TintColor");
            h_mat = mat_p1;
        } else
        {
            h_color = mat_p2.GetColor("_TintColor");
            h_mat = mat_p2;
        }

        if (model.transform.parent.name.Contains("George"))
        {
            SkinnedMeshRenderer skin = model.GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] mats = skin.materials;
            foreach(Material mat in mats)
            {
                mat.SetColor("_Color", h_color);
            }
        }
        else
        {
            SkinnedMeshRenderer[] skins = model.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer skin in skins)
            {
                Material[] mats = new Material[skin.materials.Length];

                for (int i = 0; i < skin.materials.Length; i++)
                {
                    if (i < skin.materials.Length - 1)
                    {
                        mats[i] = skin.materials[i];
                    }
                    else
                    {
                        mats[i] = h_mat;
                    }
                }

                skin.materials = mats;
            }
        }
    }

    void GenerateMaterial(List<GameObject> models, int pn)
    {
        // Assign color and mat, for current team
        Color h_color;
        Material h_mat;

        if (pn == 1)
        {
            h_color = mat_p1.GetColor("_TintColor");
            h_mat = mat_p1;
        }
        else
        {
            h_color = mat_p2.GetColor("_TintColor");
            h_mat = mat_p2;
        }

        foreach (GameObject follower in models)
        {
            SkinnedMeshRenderer[] skins = follower.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer skin in skins)
            {
                Material[] mats = new Material[skin.materials.Length];

                for (int i = 0; i < skin.materials.Length; i++)
                {
                    if (i < skin.materials.Length - 1)
                    {
                        mats[i] = skin.materials[i];
                    }
                    else
                    {
                        mats[i] = h_mat;
                    }
                }

                skin.materials = mats;
            }
        }
    }
}
