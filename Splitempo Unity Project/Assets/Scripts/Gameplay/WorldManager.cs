using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private List<World> worlds;
    private List<LevelManager> levels;
    public World currentWorld;
    public LevelManager currentLevel;

    public int CurrentWorldIndex => worlds.IndexOf(currentWorld);
    public int WorldsCount => worlds.Count;

    public void SpawnLevel(int id)
    {
        currentLevel = Instantiate(currentWorld.levels[id]).GetComponent<LevelManager>();
        currentLevel.Initialize(id);
    }

    public World SetCurrentWorld(int i)
    {
        currentWorld = worlds[i];
        return currentWorld;
    }
}