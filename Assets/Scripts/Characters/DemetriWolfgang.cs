using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemetriWolfgang : BaseCharacter {

    void Update()
    {
    }

    protected override void EnterAttacking()
    {
        throw new NotImplementedException();
    }

    protected override void ExitAttacking()
    {
        base.ExitAttacking();
    }

    protected override void HandleAttacking()
    {
        throw new NotImplementedException();
    }

    protected override void HandleIdle()
    {
        throw new NotImplementedException();
    }
}
