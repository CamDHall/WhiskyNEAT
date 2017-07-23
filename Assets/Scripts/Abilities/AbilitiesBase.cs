using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState { Start, Handle}
public abstract class AbilitiesBase : MonoBehaviour {

    public CharacterData characterData;

    AbilityState currentAbilityState = AbilityState.Start;
    int timesCalled = 0;
    string abilityInProgress;
    List<string> wipAbilities = new List<string>();
    int slowHealCalled;

    // Amount variables
    int healingAmount = 10;

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
                HandleAbility();
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
                HandleAbility();
                break;
        }
    }

    void StartAbility()
    {
        timesCalled = 0;
        EnterState(AbilityState.Handle, abilityInProgress);

    }

    // Run sinle turn abilities
    public virtual void HandleAbility()
    {
        switch (abilityInProgress)
        {
            case "SlowHealLV1":
                Debug.Log("HERE");
                if(timesCalled < 3)
                {
                    timesCalled++;
                    Healing.SlowHealLV1(characterData);
                    if(!wipAbilities.Contains("SlowHealLV1"))
                        wipAbilities.Add("SlowHealLV1");
                }
                break;
            case "BasicHeal":
                Healing.BasicHeal(characterData, healingAmount);
                break;
            case "AOEHealLV1":
                Healing.AOEHealLV1(gameObject.GetComponent<BaseCharacter>().singleCharacterTeam, characterData.gameObject);
                break;
            case "EnemyWeakHEAL":
                Healing.EnemyWeakerHEAL(gameObject.GetComponent<BaseCharacter>().singleCharacterTeam, characterData.gameObject);
                break;
        }
        currentAbilityState = AbilityState.Start;
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

    public void RunWIP()
    {
        //Debug.Log("WIP");
        foreach(string ability in wipAbilities)
        {
            abilityInProgress = ability;
            HandleAbility();
        }
    }
}
