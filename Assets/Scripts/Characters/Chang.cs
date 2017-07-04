using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chang : BaseCharacter {

    void Update()
    {

    }

    protected override void ExitAttacking()
    {
        base.ExitAttacking();
    }

    protected override void ExitMoving()
    {
        throw new NotImplementedException();
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
