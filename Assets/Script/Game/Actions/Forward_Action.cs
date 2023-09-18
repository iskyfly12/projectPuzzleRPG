using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward_Action : Action
{
    public override string Name => "forwardAction";

    public override ActionType Type => ActionType.MoveAction;

    public override void Execute(Transform t)
    {
        t.DOLocalMove(t.localPosition + t.right, 0.5f);

        Debug.Log("Execute " + Name);
        Debug.Log("Local " + t.localPosition);
        Debug.Log("Forward " + t.right);
    }
}
