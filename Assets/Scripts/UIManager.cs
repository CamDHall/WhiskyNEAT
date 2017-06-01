using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text turn, whosText, phaseText;

	void Start () {
        whosText.text = RoundManager.whosTurn.ToString() + " Turn";
        phaseText.text = PhaseManager.characterPhase.ToString();
	}
	
	void Update () {
        turn.text = "Turn: " + GameManager.turns.ToString();
	}
}
