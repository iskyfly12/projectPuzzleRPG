using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clockwise_Action : Action
{
    public override string Name => "clockwiseAction";

    public override ActionType Type => ActionType.RotateAction;

    public override void Execute(Transform t)
    {
        Vector3 rot = t.eulerAngles;
        t.DORotate(rot + (Vector3.up * 90), 0.5f);

        Debug.Log("Execute " + Name);
    }
}
