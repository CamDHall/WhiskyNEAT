using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public MapData mapData;
    UIManager overlay;
    int meleeRange, rangedRange;
    public bool targetsListed; // Only call determine once
    public bool characterSelected;
    public string typeOfAttack; // Ranged, melee, or spell

    public bool attackAdded = false;

    public Movement movement;

    // Set with character stats
    float meleeStrength, rangedStrength;
    public int numOfAttacks = 1;
    public int startingNumAttacks;

    public List<GameObject> _enemiesInRange, _friendsInRange, _enemiesInMeleeRange, _friendsInMeleeRange;

    GameObject currentEnemy, currentCharacter;

    // Inspector
    public GameObject indicator;

    // Info
    public BaseCharacter baseCharacter;

    void Start()
    {
        meleeRange = baseCharacter.meleeRange;
        rangedRange = baseCharacter.rangedRange;

        meleeStrength = baseCharacter.meleeStrength;
        rangedStrength = baseCharacter.rangedStrength;

        startingNumAttacks = numOfAttacks;
        movement = GetComponent<Movement>();

        targetsListed = false;
        characterSelected = false;

        _enemiesInRange = new List<GameObject>();
        _friendsInRange = new List<GameObject>();
        _enemiesInMeleeRange = new List<GameObject>();
        _friendsInMeleeRange = new List<GameObject>();

        // Overlay 
        overlay = GameObject.FindGameObjectWithTag("Overlay").GetComponent<UIManager>();
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
            // Selecting target to be attacked
            if (characterSelected && Input.GetMouseButtonDown(0))
            {
                // Debug.Log(typeOfAttack);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
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

        if (gameObject.tag == "Friend")
        {
            if (typeOfAttack == "Melee")
            {
                currentEnemy.GetComponent<BaseCharacter>().health -= meleeStrength;

                if (currentEnemy.GetComponent<Attacking>().meleeStrength % 2 == 0)
                {
                    GetComponent<BaseCharacter>().health -= (currentEnemy.GetComponent<Attacking>().meleeStrength / 2);
                } else
                {
                    GetComponent<BaseCharacter>().health -= ((currentEnemy.GetComponent<Attacking>().meleeStrength - 1)/ 2);
                }
            } else if(typeOfAttack == "Ranged")
            {
                currentEnemy.GetComponent<BaseCharacter>().health -= rangedStrength;
            }
        } else if(gameObject.tag == "Enemy")
        {
            if (typeOfAttack == "Melee")
            {
                currentCharacter.GetComponent<BaseCharacter>().health -= meleeStrength;
            } else if(typeOfAttack == "Ranged")
            {
                currentCharacter.GetComponent<BaseCharacter>().health -= meleeStrength;
            }
        }

        overlay.OverlayOff();
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

    void AddAttack()
    {
        attackAdded = true;
        PhaseManager.numAttacked++;
    }
}
