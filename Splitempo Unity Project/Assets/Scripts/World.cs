using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World", menuName = "Data/World", order = 1)]
public class World : ScriptableObject
{
    public List<GameObject> levelPrefabs;
    public AudioClip rise, crash, winLevel, winWorld;

    public float bpm;
}
