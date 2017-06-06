using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour {

    MapData mapData;
    public GameObject map;
    public GameObject canvas;
    bool selected;

    CharacterData characterData;

    void Start () {
        canvas.SetActive(false);
        mapData = map.GetComponent<MapData>();
        characterData = GetComponent<CharacterData>();
	}
	
	// Update is called once per frame
	void Update () {
        if(characterData.health <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                mapData.enemies.Remove(gameObject);
                PhaseManager.numEnemies--;
                Destroy(gameObject);
            } else
            {
                mapData.characters.Remove(gameObject);
                PhaseManager.numFriendlies--;
                Destroy(gameObject);
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.position == transform.position)
                {
                    GameManager.selectedCharacterData = GetComponent<CharacterData>();
                    selected = true;
                } else
                {
                    selected = false;
                }
            }
        }

        if(selected && PhaseManager.characterPhase == Phase.Attacking)
        {
            canvas.SetActive(true);
        } else
        {
            canvas.SetActive(false);
        }
	}
}
