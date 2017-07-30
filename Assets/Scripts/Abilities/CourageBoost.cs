using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourageBoost : AbilitiesBase {

	public static void CourageBoostBasic(CharacterData character, int amount)
    {
        character.courage += amount;
    }

    public static void CourageBoostBasic(List<GameObject> characters, int amount)
    {
        foreach(GameObject character in characters)
        {
            character.GetComponent<CharacterData>().courage += amount;
        }
    }

    public static void CourageForScaredEnemies(CharacterTeam team, int amount)
    {
        if(team == CharacterTeam.Friend)
        {
            foreach(GameObject enemy in MapData.enemies)
            {
                if (enemy.GetComponent<CharacterData>().courage <= 5)
                    CourageBoostBasic(MapData.friends, amount);
            }
        } else
        {
            foreach(GameObject friend in MapData.friends)
            {
                if (friend.GetComponent<CharacterData>().courage <= 5)
                    CourageBoostBasic(MapData.enemies, amount);
            }
        }
    }
}
