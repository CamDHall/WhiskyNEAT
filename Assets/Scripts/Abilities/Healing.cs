using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : AbilitiesBase {

    // Always use this to heal, even for multi-turn healing
	public static void BasicHeal(CharacterData data, int amount)
    {
        data.health += amount;
    }

    public static void BasicHeal(List<GameObject> characters, int amount)
    {
        foreach(GameObject character in characters)
        {
            character.GetComponent<CharacterData>().health += amount;
        }
    }

    public static void SlowHealLV1(CharacterData data)
    {
        BasicHeal(data, 4);
    }

    public static void AOEHeal(CharacterTeam team, GameObject user)
    {
        if (team == CharacterTeam.Enemy)
        {
            foreach (GameObject character in MapData.enemies)
            {
                if(Vector3.Distance(user.transform.position, character.transform.position) <= 3)
                {
                    BasicHeal(character.GetComponent<CharacterData>(), 3);
                }
            }
        } else
        {
            foreach(GameObject character in MapData.friends)
            {
                if(Vector3.Distance(user.transform.position, character.transform.position) <= 3)
                {
                    BasicHeal(character.GetComponent<CharacterData>(), 3);
                }
            }
        }
    }

    // Heal for every enemy that has less courage than the user
    public static void CourageousHeal(CharacterTeam team, GameObject user)
    {
        if(team == CharacterTeam.Friend)
        {
            foreach(GameObject character in MapData.enemies)
            {
                if(character.GetComponent<CharacterData>().courage < user.GetComponent<CharacterData>().courage)
                {
                    BasicHeal(MapData.friends, 2);
                }
            }
        }

        if(team == CharacterTeam.Enemy)
        {
            foreach(GameObject character in MapData.friends)
            {
                if(character.GetComponent<CharacterData>().courage < user.GetComponent<CharacterData>().courage)
                {
                    foreach(GameObject enemy in MapData.enemies)
                    {
                        BasicHeal(enemy.GetComponent<CharacterData>(), 2);
                    }
                }
            }
        }
    }

    // For every scarred enemy heal 2
    public static void IntimidatingHeal(CharacterTeam team)
    {
        if(team == CharacterTeam.Friend)
        {
            foreach(GameObject enemy in MapData.enemies)
            {
                if(enemy.GetComponent<CharacterData>().courage <= 5)
                {
                    BasicHeal(MapData.friends, 2);
                }
            }
        } else
        {
            foreach(GameObject friend in MapData.friends)
            {
                if(friend.GetComponent<CharacterData>().courage <= 1)
                {
                    BasicHeal(MapData.enemies, 2);
                }
            }
        }
    }

    public static void ReassuringHeal(CharacterTeam team, GameObject user)
    {
        if(team == CharacterTeam.Friend)
        {
            foreach(GameObject friend in MapData.friends)
            {
                if(friend.GetComponent<CharacterData>().courage <= 1)
                {
                    BasicHeal(MapData.friends, 2);
                }
            }
        } else
        {
            foreach(GameObject enemy in MapData.enemies)
            {
                if(enemy.GetComponent<CharacterData>().courage <= 1)
                {
                    BasicHeal(MapData.enemies, 2);
                }
            }
        }
    }

    // Heal everyteam mate, reduce courage of enemy to 5 for 3 turns
    public static void HealthBomb(CharacterTeam team, int amount)
    {
        if(team == CharacterTeam.Friend)
        {
            BasicHeal(MapData.friends, amount);
            CourageBoost.CourageBoostBasic(MapData.friends, amount);
        } else
        {
            BasicHeal(MapData.enemies, amount);
            CourageBoost.CourageBoostBasic(MapData.enemies, amount);
        }
    }

}
