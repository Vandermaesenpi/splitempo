using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Objective
{
    public ObjectiveType objectiveType;
    public int amount;

    public GameObject targetType;
}

public enum ObjectiveType{
    TimeLimit,
    ShotLimit,
    HurtLimit,
    Combo,
    OneShotSplit
}
