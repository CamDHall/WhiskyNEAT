using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    MapData mapData;
    int range = 1;
    public bool targetsListed; // Only call determine once
    bool characterSelected; // Must select characte before attacking

    public bool attackAdded = false;

    Movement movement;

    // Set with character stats
    float attackStrength = 1;
    public int numOfAttacks = 1;

    public int startingNumAttacks;

    [SerializeField] List<GameObject> _enemiesInRange, _charactersInRange;

    GameObject currentEnemy, currentCharacter;

    // Inspector
    public GameObject indicator;
    public GameObject map;

    void Start()
    {
        startingNumAttacks = numOfAttacks;
        movement = GetComponent<Movement>();

        targetsListed = false;
        characterSelected = false;
        mapData = map.GetComponent<MapData>();

        _enemiesInRange = new List<GameObject>();
        _charactersInRange = new List<GameObject>();
    }

    void Update()
    {
        if(numOfAttacks == 0)
        {
            if (!attackAdded)
            {
                AddAttack();
            }
        }

        if(PhaseManager.characterPhase == Phase.Attacking)
        {
            movement.moves = movement.startingMoves;
            movement.moveAdded = false;
        }


        // Selecting character to perform attack
        if (PhaseManager.characterPhase == Phase.Attacking && !characterSelected)
        {
            if (!targetsListed)
            {
                DetermineTargets();
            }
            // Check if anything is in range
            if (gameObject.tag == "Character")
            {
                if (_enemiesInRange.Count == 0)
                {
                    numOfAttacks = 0;
                }
            }

            if (gameObject.tag == "Enemy")
            {
                if (_charactersInRange.Count == 0)
                    numOfAttacks = 0;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.position == transform.position)
                    {
                        characterSelected = true;
                        SelectTarget();
                    }
                }
            }
        }

        if (PhaseManager.characterPhase == Phase.Attacking && numOfAttacks > 0)
        {
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
                    foreach (GameObject character in _charactersInRange)
                    {
                        if (hit.transform.position == character.transform.position)
                        {
                            currentCharacter = hit.transform.gameObject;
                            Damage();
                        }
                    }
                }
            }
        }
    }

    void Damage()
    {
        characterSelected = false;

        if (gameObject.tag == "Character")
        {
            currentEnemy.GetComponent<BaseCharacter>().health -= attackStrength;
            foreach(GameObject enemy in _enemiesInRange)
            {
                Destroy(enemy.transform.GetChild(0).gameObject);
            }
        } else
        {
            currentCharacter.GetComponent<BaseCharacter>().health -= attackStrength;
            foreach(GameObject character in _charactersInRange)
            {
                Destroy(character.transform.GetChild(0).gameObject);
            }
        }

        numOfAttacks--;
    }

    void DetermineTargets()
    {
        targetsListed = true;


        _charactersInRange.Clear();
        _enemiesInRange.Clear();

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
                }
            }
        }
    }

    void SelectTarget()
    {
        if(gameObject.tag == "Character")
        {
            foreach(GameObject target in _enemiesInRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);
                Instantiate(indicator, Pos, Quaternion.identity, target.transform);
            }
        }

        if (gameObject.tag == "Enemy")
        {
            foreach (GameObject target in _charactersInRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);
                Instantiate(indicator, Pos, Quaternion.identity, target.transform);
            }
        }
    }

    void AddAttack()
    {
        attackAdded = true;
        PhaseManager.numAttacked++;
    }
}
