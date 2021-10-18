using System;
using UnityEngine.Events;

[Serializable]
public class UnityEventInt : UnityEvent<int>{}
[Serializable]
public class UnityEventIntFloat : UnityEvent<int,float>{}