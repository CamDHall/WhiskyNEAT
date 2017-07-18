using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    private static EndGame s_Instance = null;
    public GameObject finish;
    public Text winner;

    public static EndGame instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(EndGame)) as EndGame;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("AManager");
                s_Instance = obj.AddComponent(typeof(EndGame)) as EndGame;
                Debug.Log("Could not locate an AManager object AManager was Generated Automaticly.");
            }

            return s_Instance;
        }
    }

    void OnApplicationQuit()
    {
        s_Instance = null;
    }

    // Add the rest of the code here...
    public void End(CharacterTeam winningTeam)
    {
        Debug.Log("END");

        finish.SetActive(true);

        if(winningTeam == CharacterTeam.Friend)
        {
            winner.GetComponent<Text>().text = "Friends Win the Game";
        } else
        {
            winner.GetComponent<Text>().text = "Enemies Win the Game";
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
