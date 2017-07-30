using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState { Start, Handle}
public abstract class AbilitiesBase : MonoBehaviour {

    public CharacterData characterData;

    AbilityState currentAbilityState = AbilityState.Start;
    int slowCount = 0, courageBombCount = 0;
    string abilityInProgress;
    public List<string> wipAbilities = new List<string>();
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
        EnterState(AbilityState.Handle, abilityInProgress);

    }

    // Run sinle turn abilities
    public virtual void HandleAbility()
    {
        switch (abilityInProgress)
        {
            case "SlowHealLV1":
                if(slowCount < 3)
                {
                    slowCount++;
                    Healing.SlowHealLV1(characterData);
                    if (!wipAbilities.Contains("SlowHealLV1"))
                    {
                        wipAbilities.Add("SlowHealLV1");
                    }
                } else
                {
                    slowCount = 0;
                    if (wipAbilities.Contains("SlowHealLV1"))
                        wipAbilities.Remove("SlowHealLV1");
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

            case "ScaredAllyHeal":
                Healing.ScaredAllyHeal(gameObject.GetComponent<BaseCharacter>().singleCharacterTeam, characterData.gameObject);
                break;
            case "CourageBomb":
                // Pass courage boost either enemies or friends
                if (gameObject.tag == "Friend")
                    CourageBoost.CourageBoostBasic(MapData.friends, 5);
                else
                    CourageBoost.CourageBoostBasic(MapData.enemies, 5);
                if (courageBombCount == 0)
                {
                    Healing.CourageBomb(gameObject.GetComponent<BaseCharacter>().singleCharacterTeam, characterData.gameObject);
                    wipAbilities.Add("CourageBomb");
                }
                else if(courageBombCount >= 3)
                {
                    courageBombCount = 0;
                    if (wipAbilities.Contains("CourageBomb"))
                        wipAbilities.Remove("CourageBomb");
                }
                courageBombCount++;
                break;
            case "CourageBoostBasic":
                CourageBoost.CourageBoostBasic(characterData, 50);
                break;
            case "CourageForScaredEnemies":
                CourageBoost.CourageForScaredEnemies(gameObject.GetComponent<BaseCharacter>().singleCharacterTeam, 25);
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
        for(int i = 0; i < wipAbilities.Count; i++)
        {
            abilityInProgress = wipAbilities[i];
            HandleAbility();
        }
    }
}
