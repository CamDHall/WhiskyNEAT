using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterCreatorWizard : ScriptableWizard {
    
    // Replace with models later
    public Mesh _mesh;
    public Material _mat;

    public float health, _meleeDefense, _rangedDefense, _spellDefense;
    public float moves;
    public int numAttacks, meleeStrength, rangedStrength;
    public int meleeRange, rangedRange;
    public float courage;
    public int mana;

    public string characterName;
    public string tag;
    public MapData mapData;

    [MenuItem ("Character Tools/Create Character...")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<CharacterCreatorWizard>("Create Character", "Create", "Update/Save");
    }

    void OnWizardCreate()
    {
        GameObject characterGO = new GameObject();
        characterGO.AddComponent<MeshFilter>();
        characterGO.AddComponent<MeshRenderer>();
        characterGO.GetComponent<Renderer>().material = _mat;
        characterGO.AddComponent<SphereCollider>();

        characterGO.GetComponent<MeshFilter>().mesh = _mesh;

        BaseCharacter baseCharacterComponent = characterGO.AddComponent<BaseCharacter>();

        // Set info
        characterGO.gameObject.name = characterName;
        baseCharacterComponent.health = health;
        baseCharacterComponent._meleeDefense = _meleeDefense;
        baseCharacterComponent._rangedDefense = _rangedDefense;
        baseCharacterComponent._spellDefense = _spellDefense;
        baseCharacterComponent.moves = moves;
        baseCharacterComponent.numAttacks = numAttacks;
        baseCharacterComponent.meleeStrength = meleeStrength;
        baseCharacterComponent.rangedStrength = rangedStrength;
        baseCharacterComponent.meleeRange = meleeRange;
        baseCharacterComponent.rangedRange = rangedRange;
        baseCharacterComponent.courage = courage;
        baseCharacterComponent.mana = mana;
        baseCharacterComponent.mapData = mapData;
        baseCharacterComponent.tag = tag;

        // Scripts
        Attacking attacking = characterGO.AddComponent<Attacking>();
        Movement movement = characterGO.AddComponent<Movement>();

        attacking.baseCharacter = baseCharacterComponent;
        movement.baseCharacter = baseCharacterComponent;
        movement.attacking = attacking;
        attacking.movement = movement;

        movement.mapData = mapData;
        attacking.mapData = mapData;

        if(Selection.activeTransform != null && Selection.activeTransform.gameObject.tag == "Tile")
        {
            characterGO.transform.parent = Selection.activeTransform;
        }

        characterGO.transform.localPosition = new Vector3(0, 1, 0);
    }

    void OnWizardOtherButton()
    {
        foreach (GameObject current in Selection.gameObjects)
        {
            if (mapData != null)
            {
                PrefabUtility.CreatePrefab("Assets/Assets/Prefabs/Characters/" + Selection.activeTransform.gameObject.name + ".prefab", current, ReplacePrefabOptions.Default);
            }
        }
    }
}
