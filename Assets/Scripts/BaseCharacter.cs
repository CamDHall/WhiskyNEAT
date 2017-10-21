using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Moving, Attacking, Idle, Done }
public enum CharacterType { Hero, Follower }
[RequireComponent(typeof(Attacking), typeof(Movement))]
public abstract class BaseCharacter : AbilitiesBase {

    MapData mapData;
    Movement movement;
    public Attacking attacking;

    public CharacterTeam singleCharacterTeam;
    public CharacterMenu menu;

    void Start()
    {
        if (PlayerInfo.deck1.Contains(this.gameObject) || PlayerInfo.deck2.Contains(this.gameObject))
            DontDestroyOnLoad(this.gameObject);

        mapData = GameObject.FindGameObjectWithTag("Map").gameObject.GetComponent<MapData>();
        movement = GetComponent<Movement>();

        attacking = GetComponent<Attacking>();
        menu = GetComponent<CharacterMenu>();

        // Set individual character team
        if (gameObject.tag == "Friend")
            singleCharacterTeam = CharacterTeam.Friend;
        else
            singleCharacterTeam = CharacterTeam.Enemy;
    }

    public State currentState = State.Idle;

    public void EnterState(State state)
    {
        currentState = state;

        switch (state)
        {
            case State.Attacking:
                EnterAttacking();
                break;
            case State.Moving:

                EnterMoving();
                break;
            case State.Idle:
                EnterIdle();
                break;
            case State.Done:
                EnterDone();
                break;
        }
    }

    public virtual void HandleState(State state)
    {
        switch (state)
        {
            case State.Attacking:
                HandleAttacking();
                break;
            case State.Moving:
                HandleMoving();
                break;
            case State.Idle:
                HandleIdle();
                break;
        }
    }

    public void ExitState(State state)
    {
        switch(state)
        {
            case State.Attacking:
                ExitAttacking();
                break;
            case State.Moving:
                ExitMoving();
                break;
            case State.Idle:
                ExitIdle();
                break;
        }
    }

    // Attacking
    protected virtual void EnterAttacking()
    {
        Targeting.DetermineTargets(GameManager.characterTeam.ToString(), characterData.rangedDistance, characterData.meleeDistance, gameObject);
    }
    protected virtual void HandleAttacking()
    {
        attacking.isAttacking = true;
    }
    protected virtual void ExitAttacking()
    {
        attacking.isAttacking = false;
        EnterState(State.Done);
        GameManager.haveGone++;
    }

    // Moving
    protected virtual void EnterMoving()
    {
        movement.isMoving = false;
    }
    protected virtual void HandleMoving()
    {
        movement.isMoving = true;
    }
    protected virtual void ExitMoving()
    {
        // Reset number of current attacks
        Paths.ResetTiles();
        characterData.currentNumberofAttacks = characterData.numberofAttacks;
        EnterState(State.Attacking);
        movement.isMoving = false;
    }

    // Idle
    protected virtual void EnterIdle()
    {
        // if the amount of characters that have moved equals the size of a team, change states

        ExitState(State.Idle);
        if (MapData.friends.Count == GameManager.haveGone)
        {
            EnterState(State.Attacking);
        }
        else
        {
            if(characterData.currentNumberofMoves != 0)
                EnterState(State.Moving);
        }
    }
    protected virtual void HandleIdle() {}
    protected virtual void ExitIdle() {}

    // Done
    void EnterDone()
    {
        characterData.currentNumberofMoves = characterData.moves;
        GetComponent<CharacterMenu>().DisplayOff();
    }

    public void Death(GameObject attacker)
    {
        RemoveDead.Remove(gameObject, attacker);
        Destroy(gameObject);

        if (attacker.tag == "Friend" && MapData.enemies.Count == 0)
            GameManager.Instance.EndGame(CharacterTeam.Enemy);
        else if (attacker.tag == "Enemy" && MapData.friends.Count == 0)
            GameManager.Instance.EndGame(CharacterTeam.Friend);
    }
}