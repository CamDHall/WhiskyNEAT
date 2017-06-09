﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    MapData mapData;
    int meleeRange, rangedRange;
    public bool targetsListed; // Only call determine once
    public bool characterSelected;
    public string typeOfAttack; // Ranged, melee, or spell

    public bool attackAdded = false;

    Movement movement;

    // Set with character stats
    float meleeStrength, rangedStrength;
    public int numOfAttacks = 1;
    public int startingNumAttacks;

    public List<GameObject> _enemiesInRange, _friendsInRange, _enemiesInMeleeRange, _friendsInMeleeRange;

    GameObject currentEnemy, currentCharacter;

    // Inspector
    public GameObject indicator;
    public GameObject map;

    // Info
    CharacterData characterData;

    private void Awake()
    {
        characterData = GetComponent<CharacterData>();
    }

    void Start()
    {
        meleeRange = characterData.meleeRange;
        rangedRange = characterData.rangedRange;

        meleeStrength = characterData.meleeStrength;
        rangedStrength = characterData.rangedStrength;

        startingNumAttacks = numOfAttacks;
        movement = GetComponent<Movement>();

        targetsListed = false;
        characterSelected = false;
        mapData = map.GetComponent<MapData>();

        _enemiesInRange = new List<GameObject>();
        _friendsInRange = new List<GameObject>();
        _enemiesInMeleeRange = new List<GameObject>();
        _friendsInMeleeRange = new List<GameObject>();
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
            if (gameObject.tag == "Friend")
            {
                if (_enemiesInRange.Count == 0 && _enemiesInMeleeRange.Count == 0)
                {
                    numOfAttacks = 0;
                }
            }

            if (gameObject.tag == "Enemy")
            {
                if (_friendsInRange.Count == 0 && _friendsInMeleeRange.Count == 0)
                    numOfAttacks = 0;
            }
        }
    
        if (PhaseManager.characterPhase == Phase.Attacking && numOfAttacks > 0 && (typeOfAttack == "Melee" || typeOfAttack == "Ranged"))
        {
            // Debug.Log(characterSelected);
            // Selecting target to be attacked
            if (characterSelected && Input.GetMouseButtonDown(0))
            {
                // Debug.Log(typeOfAttack);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(typeOfAttack);
                    // Enemy
                    if (typeOfAttack == "Ranged")
                    {
                        if (gameObject.tag == "Friend")
                        {
                            Debug.Log(gameObject.name);
                            foreach (GameObject enemy in _enemiesInRange)
                            {
                                if (hit.transform.position == enemy.transform.position)
                                {
                                    currentEnemy = hit.transform.gameObject;
                                    Damage();
                                }
                            }
                        }

                        if (gameObject.tag == "Enemy")
                        {
                            foreach (GameObject character in _friendsInRange)
                            {
                                if (hit.transform.position == character.transform.position)
                                {
                                    currentCharacter = hit.transform.gameObject;
                                    Damage();
                                }
                            }
                        }
                    } else if(typeOfAttack == "Melee")
                    {
                        if (gameObject.tag == "Friend")
                        {
                            foreach (GameObject enemy in _enemiesInMeleeRange)
                            {
                                if (hit.transform.position == enemy.transform.position)
                                {
                                    currentEnemy = hit.transform.gameObject;
                                    Damage();
                                }
                            }
                        }

                        if(gameObject.tag == "Enemy")
                        {
                            foreach (GameObject enemy in _friendsInMeleeRange)
                            {
                                if (hit.transform.position == enemy.transform.position)
                                {
                                    currentEnemy = hit.transform.gameObject;
                                    Damage();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void Damage()
    {
        characterSelected = false;
        Debug.Log(gameObject.name);

        if (gameObject.tag == "Friend")
        {
            if (typeOfAttack == "Melee")
            {
                //Debug.Log(gameObject.name);
                currentEnemy.GetComponent<CharacterData>().health -= meleeStrength;
                foreach (GameObject enemy in _enemiesInMeleeRange)
                {
                    Destroy(enemy.transform.GetChild(0).gameObject);
                }
            } else if(typeOfAttack == "Ranged")
            {
                currentEnemy.GetComponent<CharacterData>().health -= rangedStrength;
                foreach (GameObject enemy in _enemiesInRange)
                {
                    Destroy(enemy.transform.GetChild(0).gameObject);
                }
            }
        } else if(gameObject.tag == "Enemy")
        {
            if (typeOfAttack == "Melee")
            {
                currentCharacter.GetComponent<CharacterData>().health -= meleeStrength;
                foreach (GameObject character in _friendsInMeleeRange)
                {
                    Destroy(character.transform.GetChild(0).gameObject);
                }
            } else if(typeOfAttack == "Ranged")
            {
                currentCharacter.GetComponent<CharacterData>().health -= meleeStrength;
                foreach (GameObject character in _friendsInRange)
                {
                    Destroy(character.transform.GetChild(0).gameObject);
                }
            }
        }

        numOfAttacks--;
    }

    void DetermineTargets()
    {
        targetsListed = true;

        _friendsInRange.Clear();
        _enemiesInRange.Clear();
        _friendsInMeleeRange.Clear();
        _enemiesInMeleeRange.Clear();

        // Check if this is a character or enemy
        if (gameObject.tag == "Friend")
        {
            foreach (GameObject enemy in mapData.enemies)
            {
                if (enemy.transform.position.y == 1)
                    mapData.enenmyInfo[enemy.transform.position] = Mathf.Abs(enemy.transform.position.x - transform.position.x) + Mathf.Abs(enemy.transform.position.z - transform.position.z);
                else if (enemy.transform.position.y == 1.25f)
                    mapData.enenmyInfo[enemy.transform.position] = Mathf.Abs(enemy.transform.position.x - transform.position.x) + Mathf.Abs(enemy.transform.position.z - transform.position.z) + 0.25f;
                else if (enemy.transform.position.y == 1.5f)
                    mapData.enenmyInfo[enemy.transform.position] = Mathf.Abs(enemy.transform.position.x - transform.position.x) + Mathf.Abs(enemy.transform.position.z - transform.position.z) + 0.5f;

                if (mapData.enenmyInfo[enemy.transform.position] <= meleeRange)
                {
                    _enemiesInMeleeRange.Add(enemy);
                }

                if (mapData.enenmyInfo[enemy.transform.position] <= rangedRange)
                {
                    _enemiesInRange.Add(enemy);
                }
            }
        } else
        {
            foreach(GameObject friend in mapData.characters)
            {
                if (friend.transform.position.y == 1.0f)
                    mapData.friendsInfo[friend.transform.position] = Mathf.Abs(friend.transform.position.x - transform.position.x) + Mathf.Abs(friend.transform.position.z - transform.position.z);
                else if (friend.transform.position.y == 1.25f)
                    mapData.friendsInfo[friend.transform.position] = Mathf.Abs(friend.transform.position.x - transform.position.x) + Mathf.Abs(friend.transform.position.z - transform.position.z) + 0.25f;
                else if (friend.transform.position.y == 1.5f)
                    mapData.friendsInfo[friend.transform.position] = Mathf.Abs(friend.transform.position.x - transform.position.x) + Mathf.Abs(friend.transform.position.z - transform.position.z) + 0.5f;

                if(mapData.friendsInfo[friend.transform.position] <= meleeRange)
                {
                    _friendsInMeleeRange.Add(friend);
                }

                if(mapData.friendsInfo[friend.transform.position] <= rangedRange)
                {
                    _friendsInRange.Add(friend);
                }
            }
        }
    }

    public void SelectTarget()
    {
        if(gameObject.tag == "Friend")
        {
            foreach(GameObject target in _enemiesInRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);
                Instantiate(indicator, Pos, Quaternion.identity, target.transform);
            }

            foreach(GameObject target in _enemiesInMeleeRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);
                Instantiate(indicator, Pos, Quaternion.identity, target.transform);
            }
        }

        if (gameObject.tag == "Enemy")
        {
            foreach (GameObject target in _friendsInRange)
            {
                Vector3 Pos = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);
                Instantiate(indicator, Pos, Quaternion.identity, target.transform);
            }

            foreach (GameObject target in _friendsInMeleeRange)
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
