using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {

		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.tag == "Enemy" || hit.transform.gameObject.tag == "Friend")
                {
                    CharacterSelected(hit.transform.gameObject.tag.ToString(), hit.transform.gameObject.GetComponent<CharacterData>(), hit.transform.gameObject);
                } else if(!Targeting.reachableTiles.Contains(hit.transform.gameObject))
                {
                    NothingSelected();
                    Targeting.ResetTiles();
                }
            }
        }

        if(GameManager.selectedCharacterInfo != null)
        {
            // Debug.Log(GameManager.selectedCharacterInfo.health);
        }
	}

    void NothingSelected()
    {
        GameManager.selectedCharacter = null;
        GameManager.selectedCharacterInfo = null;
    }

    void CharacterSelected(string team, CharacterData info, GameObject hit)
    {
        if(team == GameManager.characterTeam.ToString())
        {
            GameManager.selectedCharacter = hit;
            GameManager.selectedCharacterInfo = hit.transform.gameObject.GetComponent<CharacterData>();
            GameManager.selectedBaseCharacter = GameManager.selectedCharacter.GetComponent<BaseCharacter>();
            BaseCharacter baseCharacter = GameManager.selectedBaseCharacter;

            baseCharacter.EnterState(baseCharacter.currentState);
        }

        GameManager.selectedCharacterInfo = info;
    }
}
