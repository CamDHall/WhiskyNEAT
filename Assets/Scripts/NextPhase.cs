using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPhase : MonoBehaviour {

	public void Next()
    {
        if(GameManager.Instance.selectedBaseCharacter.currentState == State.Moving)
        {
            GameManager.Instance.selectedBaseCharacter.ExitState(State.Moving);
        } else if (GameManager.Instance.selectedBaseCharacter.currentState == State.Attacking)
        {
            GameManager.Instance.selectedBaseCharacter.ExitState(State.Attacking);
        }
    }
}
