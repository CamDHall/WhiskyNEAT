using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Moving, Attacking, Idle, Done }
[RequireComponent(typeof(Attacking), typeof(Movement))]
public abstract class BaseCharacter : MonoBehaviour {

    MapData mapData;
    Movement movement;
    public CharacterData characterData;
    public Attacking attacking;

    void Start()
    {
        mapData = GameObject.FindGameObjectWithTag("Map").gameObject.GetComponent<MapData>();
        movement = GetComponent<Movement>();
        characterData = GetComponent<CharacterData>();
        attacking = GetComponent<Attacking>();
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

    protected virtual void HandleState(State state)
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
        Paths.ChangeTiles();
        HandleState(State.Moving);
    }
    protected virtual void HandleMoving()
    {
        movement.isMoving = true;
    }
    protected virtual void ExitMoving()
    {
        // Reset number of current attacks
        characterData.currentNumberofAttacks = characterData.numberofAttacks;
        EnterState(State.Attacking);
    }

    // Idle
    protected virtual void EnterIdle()
    {
        // Debug.Log("ENETERED IDLE");
        // if the amount of characters that have moved equals the size of a team, change states
        ExitState(State.Idle);
        if (MapData.friends.Count == GameManager.haveGone)
        {
            EnterState(State.Attacking);
        }
        else
        {
            EnterState(State.Moving);
        }
    }
    protected virtual void HandleIdle() {}
    protected virtual void ExitIdle() {}

    // Done
    void EnterDone()
    {
        characterData.currentNumberofMoves = characterData.moves;
    }
}