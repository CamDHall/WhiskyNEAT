using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : MonoBehaviour {

    public GameObject confirmWindow;

	public void ConfirmAttack()
    {
        
        GameManager.confirmationState = Confirmation.Ready;
        confirmWindow.SetActive(false);
        GameManager.currentAttackingObj.ExecuteAttack();
    }
}
