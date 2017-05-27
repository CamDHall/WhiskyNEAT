using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{

    MapData mapData;
    int range = 1;
    bool targetsListed; // Only call determine once
    bool characterSelected; // Must select characte before attacking

    List<GameObject> _enemiesInRange, _charactersInRange;

    GameObject currentEnemy, currentCharacter;

    // Inspector
    public GameObject indicator;
    public GameObject map;

    void Start()
    {
        targetsListed = false;
        characterSelected = false;
        mapData = map.GetComponent<MapData>();

        _enemiesInRange = new List<GameObject>();
        _charactersInRange = new List<GameObject>();
    }

    void Update()
    {
        // Selecting character to perform attack
        if (Movement.characterPhase == Phase.Attacking && !characterSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.position == transform.position)
                    {
                        DetermineTargets();
                        characterSelected = true;
                    }
                }
            }
        }

        // Selecting target to be attacked
        if (characterSelected && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Enemy
                foreach (GameObject enemy in _enemiesInRange)
                {
                    if (hit.transform.position == enemy.transform.position)
                    {
                        currentEnemy = hit.transform.gameObject;
                        Damage();
                    }
                }

                // Character
                foreach(GameObject character in _charactersInRange)
                {
                    if(hit.transform.position == character.transform.position)
                    {
                        currentCharacter = hit.transform.gameObject;
                        Damage();
                    }
                }
            }
        }
    }

    void Damage()
    {
        if (gameObject.tag == "Character")
        {
            currentEnemy.GetComponent<BaseCharacter>().health--;
        } else
        {
            currentCharacter.GetComponent<BaseCharacter>().health--;
        }
    }

    void DetermineTargets()
    {
        targetsListed = true;

        // Check if this is a character or enemy
        if (gameObject.tag == "Character")
        {
            foreach (GameObject enemy in mapData.enemies)
            {
                if (enemy.transform.position.y == 1)
                    mapData.enenmyInfo[enemy.transform.position] = Mathf.Abs(enemy.transform.position.x - transform.position.x) + Mathf.Abs(enemy.transform.position.z - transform.position.z);
                else if (enemy.transform.position.y == 1.25f)
                    mapData.enenmyInfo[enemy.transform.position] = Mathf.Abs(enemy.transform.position.x - transform.position.x) + Mathf.Abs(enemy.transform.position.z - transform.position.z) + 0.25f;
                else if (enemy.transform.position.y == 1.5f)
                    mapData.enenmyInfo[enemy.transform.position] = Mathf.Abs(enemy.transform.position.x - transform.position.x) + Mathf.Abs(enemy.transform.position.z - transform.position.z) + 0.5f;

                if (mapData.enenmyInfo[enemy.transform.position] <= range)
                {
                    _enemiesInRange.Add(enemy);
                    Vector3 Pos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1, enemy.transform.position.z);
                    Instantiate(indicator, Pos, Quaternion.identity, enemy.transform);
                }
            }
        } else
        {
            foreach(GameObject character in mapData.characters)
            {
                if (character.transform.position.y == 1.0f)
                    mapData.characterInfo[character.transform.position] = Mathf.Abs(character.transform.position.x - transform.position.x) + Mathf.Abs(character.transform.position.z - transform.position.z);
                else if (character.transform.position.y == 1.25f)
                    mapData.characterInfo[character.transform.position] = Mathf.Abs(character.transform.position.x - transform.position.x) + Mathf.Abs(character.transform.position.z - transform.position.z) + 0.25f;
                else if (character.transform.position.y == 1.5f)
                    mapData.characterInfo[character.transform.position] = Mathf.Abs(character.transform.position.x - transform.position.x) + Mathf.Abs(character.transform.position.z - transform.position.z) + 0.5f;

                if(mapData.characterInfo[character.transform.position] <= range)
                {
                    _charactersInRange.Add(character);
                    Vector3 Pos = new Vector3(character.transform.position.x, character.transform.position.y + 1, character.transform.position.z);
                    Instantiate(indicator, Pos, Quaternion.identity, character.transform);
                }
            }
        }
    }
}
