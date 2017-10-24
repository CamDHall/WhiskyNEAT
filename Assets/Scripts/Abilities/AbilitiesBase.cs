using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AbilityState { Start, Handle}
public abstract class AbilitiesBase : MonoBehaviour {

    public CharacterData characterData;

    int slowCount = 0, courageBombCount = 0;
    string abilityInProgress;
    public List<string> wipAbilities = new List<string>();
    int slowHealCalled;

    // Amount variables
    int healingAmount = 10;

    public void EnterState(AbilityState state, string nameOfAbility)
    {
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
        if (UIManager.Instance.abilityInfo.activeSelf)
            UIManager.Instance.abilityInfo.SetActive(false);
        switch (abilityInProgress)
        {
            // Heal
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
            case "AOEHeal":
                Healing.AOEHeal(gameObject.GetComponent<BaseCharacter>().singleCharacterTeam, characterData.gameObject);
                break;
            case "CourageousHeal":
                Healing.CourageousHeal(gameObject.GetComponent<BaseCharacter>().singleCharacterTeam, characterData.gameObject);
                break;
            case "IntimidatingHeal":
                Healing.IntimidatingHeal(GetComponent<BaseCharacter>().singleCharacterTeam);
                break;
            case "HealthBomb":
                Healing.HealthBomb(GetComponent<BaseCharacter>().singleCharacterTeam, 5);
                break;

            case "ReassuringHeal":
                Healing.ReassuringHeal(gameObject.GetComponent<BaseCharacter>().singleCharacterTeam, characterData.gameObject);
                break;

            // Courage
            case "CourageBomb":
                // Pass courage boost either enemies or friends
                if (gameObject.tag == "Friend")
                    CourageBoost.CourageBoostBasic(MapData.friends, 5);
                else
                    CourageBoost.CourageBoostBasic(MapData.enemies, 5);
                if (courageBombCount == 0)
                {
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
            case "BasicCourageBoost":
                CourageBoost.CourageBoostBasic(characterData, 20);
                break;
            case "MoraleBoost":
                if (GetComponent<BaseCharacter>().singleCharacterTeam == CharacterTeam.Friend)
                    CourageBoost.MoraleBoost(MapData.friends, characterData, 10);
                else
                    CourageBoost.MoraleBoost(MapData.enemies, characterData, 10);
                break;
            case "ScareTactics":
                if(GetComponent<BaseCharacter>().singleCharacterTeam == CharacterTeam.Friend)
                    CourageBoost.ScareTactics(MapData.enemies, characterData, 25);
                else
                    CourageBoost.ScareTactics(MapData.friends, characterData, 25);
                break;
            case "WarCry":
                if (GetComponent<BaseCharacter>().singleCharacterTeam == CharacterTeam.Friend)
                    CourageBoost.WarCry(MapData.enemies, characterData, 20);
                else
                    CourageBoost.WarCry(MapData.friends, characterData, 20);
                break;
            case "BloodCry":
                if (GetComponent<BaseCharacter>().singleCharacterTeam == CharacterTeam.Friend)
                    CourageBoost.BloodCry(MapData.enemies, characterData);
                else
                    CourageBoost.BloodCry(MapData.friends, characterData);
                break;
            case "Sacarfice":
                if (GetComponent<BaseCharacter>().singleCharacterTeam == CharacterTeam.Friend)
                    CourageBoost.Sacrifice(MapData.friends, characterData, 30);
                break;
            // Buffs
            case "MeleeDamageBuff":
                Buff.MeleeDamageBuff(characterData, 10);
                break;
            case "MeleeBuffTeam":
                Debug.Log("HERE");
                if (GetComponent<BaseCharacter>().singleCharacterTeam == CharacterTeam.Friend)
                    Buff.MeleeDamageBuffTeam(MapData.friends, 5);
                else
                    Buff.MeleeDamageBuffTeam(MapData.enemies, 5);
                break;
            case "RangedDamageBuff":
                Buff.RangedDamageBuff(characterData, 6);
                break;
            case "RangedBuffTeam":
                if (GetComponent<BaseCharacter>().singleCharacterTeam == CharacterTeam.Friend)
                    Buff.RangedDamageBuffTeam(MapData.friends, 3);
                else
                    Buff.RangedDamageBuffTeam(MapData.enemies, 3);
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

    public void RunWIP()
    {
        for(int i = 0; i < wipAbilities.Count; i++)
        {
            abilityInProgress = wipAbilities[i];
            HandleAbility();
        }

        foreach(Button button in characterData.buttonsIP)
        {
            button.GetComponent<AbilityButtonManager>().count++;
        }
    }
}
