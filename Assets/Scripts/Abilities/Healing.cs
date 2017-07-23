﻿using System.Collections;
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

    public static void AOEHealLV1(CharacterTeam team, GameObject user)
    {
        if (team == CharacterTeam.Enemy)
        {
            foreach (GameObject character in MapData.enemies)
            {
                if(Vector3.Distance(user.transform.position, character.transform.position) <= 3)
                {
                    BasicHeal(character.GetComponent<CharacterData>(), 2);
                }
            }
        } else
        {
            foreach(GameObject character in MapData.friends)
            {
                if(Vector3.Distance(user.transform.position, character.transform.position) <= 3)
                {
                    BasicHeal(character.GetComponent<CharacterData>(), 2);
                }
            }
        }
    }

    // Heal for every enemy that has less courage than the user
    public static void EnemyWeakerHEAL(CharacterTeam team, GameObject user)
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
    public static void FearedHealLV1(CharacterTeam team, GameObject user)
    {
        if(team == CharacterTeam.Friend)
        {
            foreach(GameObject enemy in MapData.enemies)
            {
                if(enemy.GetComponent<CharacterData>().courage <= 1)
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

}
