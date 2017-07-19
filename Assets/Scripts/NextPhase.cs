using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPhase : MonoBehaviour {

	public void Next()
    {
        if(GameManager.selectedBaseCharacter.currentState == State.Moving)
        {
            GameManager.selectedBaseCharacter.ExitState(State.Moving);
        } else if (GameManager.selectedBaseCharacter.currentState == State.Attacking)
        {
            GameManager.selectedBaseCharacter.ExitState(State.Attacking);
        }
    }
}
