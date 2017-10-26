using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourageBoost : AbilitiesBase {

    // Basic boost
	public static void CourageBoostBasic(CharacterData character, int amount)
    {
        character.courage += amount;
    }

    // Boost group
    public static void CourageBoostBasic(List<GameObject> characters, int amount)
    {
        foreach(GameObject character in characters)
        {
            character.GetComponent<CharacterData>().courage += amount;
        }
    }

    // Morale boost
    public static void MoraleBoost(List<GameObject> characters, CharacterData targetData, int amount)
    {
        foreach(GameObject character in characters)
        {
            CharacterData data = character.GetComponent<CharacterData>();
            if(data.courage <= 5)
            {
                targetData.courage += amount;
            }
        }
    }

    // Scare tactics
    public static void ScareTactics(List<GameObject> targetedCharacters, CharacterData userData, int amount)
    {
        foreach(GameObject character in targetedCharacters)
        {
            CharacterData targetData = character.GetComponent<CharacterData>();
            if(targetData.courage < userData.courage)
            {
                targetData.courage -= amount;
            }
        }
    }

    // Warcry
    public static void WarCry(List<GameObject> targetedCharacters, CharacterData userData, int amount)
    {
        foreach(GameObject character in targetedCharacters)
        {
            CharacterData data = character.GetComponent<CharacterData>();
            if(data.courage > userData.courage)
            {
                data.courage -= amount;
            }
        }
    }

    // Bloodcry
    public static void BloodCry(List<GameObject> targetedCharacters, CharacterData userData)
    {
        foreach(GameObject character in targetedCharacters)
        {
            character.GetComponent<CharacterData>().courage = 5;
        }

        userData.health -= 15; 
    }

    // Sacarfic
    public static void Sacrifice(List<GameObject> allies, CharacterData userData, int amount)
    {
        foreach(GameObject allie in allies)
        {
            allie.GetComponent<CharacterData>().health += amount;
        }

        userData.courage = 1;
    }
}
