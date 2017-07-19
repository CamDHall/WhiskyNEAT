using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState { Start, Handle}
public abstract class AbilitiesBase : MonoBehaviour {

    public CharacterData characterData;

    AbilityState currentAbilityState = AbilityState.Start;
    int timesCalled = 0;
    string abilityInProgress;

    // Amount variables
    int healingAmount;

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

    // Override Enter state take take ability specefic variables
    public void EnterState(AbilityState state, string nameOfAbility, int HealingAmount)
    {
        currentAbilityState = state;
        abilityInProgress = nameOfAbility;
        healingAmount = HealingAmount;
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
        switch (ability)
        {
            case "SlowHeal":
                if(timesCalled < 3) { 
                Healing.SlowHealLV1(characterData);
                timesCalled++;
                    }
                break;
            case "BasicHeal":
                Healing.BasicHeal(characterData, healingAmount);
                ExitAbility();
                break;
            case "AOEHealLV1":
                Healing.AOEHealLV1(gameObject.GetComponent<BaseCharacter>().singleCharacterTeam, characterData.gameObject);
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
                    Healing.SlowHealLV1(characterData);
                    timesCalled++;
                }
                break;
            case "BasicHeal":
                Healing.BasicHeal(characterData, healingAmount);
                ExitAbility();
                break;
        }
    }

    // Define functions for abilities
    public virtual void AbilityOne()
    {
        characterData.currentNumberofAttacks--;
    }
    public virtual void AbilityTwo()
    {
        characterData.currentNumberofAttacks--;
    }
    public virtual void AbilityThree()
    {
        characterData.currentNumberofAttacks--;
    }
}
