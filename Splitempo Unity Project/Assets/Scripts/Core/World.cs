using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World", menuName = "Data/World", order = 1)]
public class World : ScriptableObject
{
    public string displayName;
    public Sprite icon;
    public Color color;
    public int minimumTrophies;
    public List<GameObject> levels;
    public AudioClip sfxRise, sfxCrash, sfxWinLevel, sfxWinWorld;
    public float bpm;
    public bool IsLocked => GM.I.trophies < minimumTrophies;

    public int TrophiesInWorld{
        get{
            int i = 0;
            foreach (string trophyName in GM.I.trophyLevels)
            {
                foreach (GameObject level in levels)
                {
                    if(level.name == trophyName){i++;}
                }
            }

            return i;
        }
    }

}
