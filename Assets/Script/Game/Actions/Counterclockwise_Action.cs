using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counterclockwise_Action : Action
{
    public override string Name => "counterclockwiseAction";

    public override ActionType Type => ActionType.RotateAction;

    public override void Execute(Transform t)
    {
        Vector3 rot = t.eulerAngles;
        t.DORotate(rot + (Vector3.down * 90), 0.5f);

        Debug.Log("Execute " + Name);
    }
}
