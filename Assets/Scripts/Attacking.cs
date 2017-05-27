using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{

    MapData mapData;
    int range = 1;
    bool targetsListed; // Only call determine once
    bool characterSelected; // Must select characte before attacking

    List<GameObject> _enemiesInRange;

    // Inspector
    public GameObject indicator;
    public GameObject map;

    void Start()
    {
        targetsListed = false;
        characterSelected = false;
        mapData = map.GetComponent<MapData>();
        _enemiesInRange = new List<GameObject>();
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
                foreach (GameObject enemy in _enemiesInRange)
                {
                    if (hit.transform.position == enemy.transform.position)
                    {
                        Damage();
                    }
                }
            }
        }
    }

    void Damage()
    {

    }

    void DetermineTargets()
    {
        targetsListed = true;
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
                Instantiate(indicator, Pos, Quaternion.identity);
            }
        }
    }
}
