using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState { Start, Handle}
public abstract class AbilitiesBase : MonoBehaviour {

    public CharacterData characterData;

    AbilityState currentAbilityState = AbilityState.Start;
    int timesCalled = 0;
    string abilityInProgress;

    void Start()
    {
        characterData = GetComponent<CharacterData>();
    }

    public void EnterState(AbilityState state, string nameOfAbility)
    {
        currentAbilityState = state;
        abilityInProgress = nameOfAbility;

        switch (state)
        {
            case AbilityState.Start:
                StartAbility();
                break;
            case AbilityState.Handle:
                HandleAbility(nameOfAbility);
                break;
        }

    }



    public void ExitAbility()
    {
        currentAbilityState = AbilityState.Start;
        timesCalled = 0;
    }

    void StartAbility()
    {
        timesCalled = 0;
        EnterState(AbilityState.Handle, abilityInProgress);

    }

    // Run sinle turn abilities
    public virtual void HandleAbility(string ability)
    {
        Debug.Log("Not Overloaded");
        switch (ability)
        {
            case "SlowHeal":
                if(timesCalled < 3) { 
                Healing.SlowHeal(characterData);
                timesCalled++;
                    }
                break;
            case "BasicHeal":
                Healing.BasicHeal(characterData);
                ExitAbility();
                break;
        }
    }

    // Override to handle in progress abilities
    public virtual void HandleAbility()
    {
        switch (abilityInProgress)
        {
            case "SlowHeal":
                if (timesCalled < 3)
                {
                    Healing.SlowHeal(characterData);
                    timesCalled++;
                }
                break;
            case "BasicHeal":
                Healing.BasicHeal(characterData);
                ExitAbility();
                break;
        }
    }
}
