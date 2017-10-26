using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour {

    public GameObject confirmWindow;

	public void ConfirmAttack()
    {   
        GameManager.confirmationState = Confirmation.Ready;
        GameManager.currentAttackingObj.ExecuteAttack();
        confirmWindow.SetActive(false);
    }

    public void CancelAttack()
    {
        GameManager.confirmationState = Confirmation.Idle;
        confirmWindow.SetActive(false);
        GameManager.currentAttackingObj.GetComponent<CharacterMenu>().DisplayOff();
        GameManager.currentAttackingObj.GetComponent<CharacterMenu>().OverlayOff();
    }
}
