using System;
using UnityEngine;

public class TrophiesManager : MonoBehaviour {
    private static TrophiesManager _instance;
    public static TrophiesManager I 
    {
        get {
            if (!_instance) {
                _instance = FindObjectOfType<TrophiesManager>();
            }
            return _instance;
        }
    }



    public bool IsLevelLocked(World world, int id)
    {
        return true;
    }
}