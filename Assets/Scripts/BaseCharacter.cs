﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Moving, Attacking, Idle }
[RequireComponent(typeof(Attacking), typeof(Movement))]
public abstract class BaseCharacter : MonoBehaviour {

    MapData mapData;
    Movement movement;
    CharacterData characterData;
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
        // Only exit moving and attacking when their aren't any moves or attacks left
        if (!(currentState == State.Moving && characterData.moves != 0) || (currentState == State.Attacking && characterData.numberOfAttacks != 0))
            ExitState(currentState);

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

    protected virtual void ExitState(State state)
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
        Targeting.DetermineTargets(gameObject.tag.ToString(), characterData.rangedDistance, characterData.meleeDistance, this.gameObject);
        GameManager.selectedCharacter.GetComponent<CharacterMenu>().DisplayActionBar();
    }
    protected virtual void HandleAttacking()
    {
        attacking.isAttacking = true;
    }
    protected virtual void ExitAttacking()
    {
        attacking.isAttacking = false;
    }

    // Moving
    protected virtual void EnterMoving()
    {
        Paths.ChangeTiles();
        HandleState(State.Moving);
    }
    protected virtual void HandleMoving()
    {
        if(characterData.moves == 0)
        {
            movement.isMoving = false;
            EnterState(State.Attacking);
        }
        movement.isMoving = true;
    }
    protected virtual void ExitMoving()
    {

    }

    // Idle
    protected virtual void EnterIdle()
    {
        // Debug.Log("ENETERED IDLE");
        // if the amount of characters that have moved equals the size of a team, change states
        if (GameManager.currentPhase == Phase.Moving)
        {
            ExitState(State.Idle);
            if (MapData.friends.Count == GameManager.haveGone)
            {
                Debug.Log("HERE");
                EnterState(State.Attacking);
            }
            else
            {
                EnterState(State.Moving);
            }
        }

        if (GameManager.currentPhase == Phase.Attacking)
        {
            if (MapData.friends.Count == GameManager.haveGone)
            {
                EnterState(State.Moving);
                GameManager.currentPhase = Phase.Moving;
                GameManager.ChangeTeams();
            }
            else
            {
                EnterAttacking();
            }
        }
    }
    protected virtual void HandleIdle() {}
    protected virtual void ExitIdle() {}

    //
    // General Functions 
    //

    void Melee(int meleeStrength, CharacterData targetData)
    {
        targetData.health -= meleeStrength;
    }

    void Ranged(int rangedStrength, CharacterData targetData)
    {
        targetData.health -= rangedStrength;
    }
}