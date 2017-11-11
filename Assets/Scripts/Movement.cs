using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public bool isMoving = false;
    CharacterData characterData;

    CharacterMenu c_menu;

	void Start () {
        characterData = GetComponent<CharacterData>();
        c_menu = GetComponent<CharacterMenu>();
	}
	
	void Update () {
        if (characterData.currentNumberofMoves == 0 && GetComponent<BaseCharacter>().currentState == State.Moving)
        {
            isMoving = false;
            GetComponent<BaseCharacter>().ExitState(State.Moving);
        }

        if (Input.GetMouseButtonDown(0) && isMoving && GameManager.Instance.selectedCharacter == this.gameObject)
        {   
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(Paths.reachableTiles.Contains(hit.transform.gameObject))
                {
                    transform.parent = hit.transform;
                    // Overlaoaded change also returns move amount
                    GameManager.Instance.selectedCharacterData.currentNumberofMoves = Paths.ChangeTiles(hit.transform);
                    if (GameManager.Instance.selectedCharacterData.currentNumberofMoves == 0)
                    {
                        UIManager.Instance.highlighterEnemy.SetActive(false);
                        UIManager.Instance.highlighterFriend.SetActive(false);
                        Paths.ResetTiles();
                    }

                    transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                    // Change for true position
                    Paths.ChangeTiles();
                }
            }
        }
    }

    public void CheckMovement()
    {
        // Check if state is moving but Handle state hasn't been called yet, then check for raycast to set path and handle movement
        if (GetComponent<BaseCharacter>().currentState == State.Moving && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.position == transform.position)
                {
                    Paths.ChangeTiles();
                    GetComponent<BaseCharacter>().HandleState(State.Moving);
                }
            }
        }
    }
}
