using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public bool isMoving = false;
    CharacterData characterData;

	void Start () {
        characterData = GetComponent<CharacterData>();
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
                foreach (GameObject tile in Paths.reachableTiles)
                {
                    if (tile.transform.childCount <= 0)
                    {
                        if (tile.transform.position == hit.transform.position && !(hit.transform.position.x == transform.position.x && hit.transform.position.z == transform.position.z))
                        {
                            transform.parent = hit.transform;
                            GameManager.Instance.selectedCharacterData.currentNumberofMoves -= (int)MapData.tileInfo[hit.transform.position];
                            // Reset highlighter before moving
                            if (GameManager.Instance.selectedCharacterData.currentNumberofMoves == 0)
                            {
                                UIManager.Instance.highlighterEnemy.SetActive(false);
                                UIManager.Instance.highlighterFriend.SetActive(false);
                            }
                            transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                            UIManager.Instance.NothingSelected();
                            // Reset tile colors
                            Paths.ResetTiles();
                        }
                    } else if(hit.transform.position != GameManager.Instance.selectedCharacter.transform.position)
                    {
                        Paths.ResetTiles();
                    }
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
