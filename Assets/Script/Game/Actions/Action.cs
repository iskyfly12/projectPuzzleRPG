using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType { MoveAction, RotateAction, AttackAction };

public abstract class Action
{
    public abstract string Name { get; }

    public abstract ActionType Type { get; }

    public abstract void Execute(Transform t);
}
