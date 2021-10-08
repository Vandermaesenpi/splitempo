using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World", menuName = "Data/World", order = 1)]
public class World : ScriptableObject
{
    public string displayName;
    public Sprite icon;
    public Color color;
    public List<GameObject> levelPrefabs;
    public AudioClip rise, crash, winLevel, winWorld;
    public float bpm;

    public int TrophiesInWorld{
        get{
            int i = 0;
            foreach (string trophyName in GM.I.trophyLevels)
            {
                foreach (GameObject level in levelPrefabs)
                {
                    if(level.name == trophyName){i++;}
                }
            }

            return i;
        }
    }
}
