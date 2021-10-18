using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : MenuUI
{
    [SerializeField] private List<LevelButton> levelButtons;
    [SerializeField] private GridLayoutGroup grid;
    
    CanvasGroup _canvasGroup;
    private SnapshotCamera snapshotCamera;

    public override void Awake()
    {
        snapshotCamera = SnapshotCamera.MakeSnapshotCamera("SnapshotLayer");
        _canvasGroup = GetComponent<CanvasGroup>();
        base.Awake();
    }
    public override void Initialize()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            
            GameObject levelPrefab = GM.I.gp.CurrentWorld.levels[i].gameObject;
            Texture2D levelTexture = snapshotCamera.TakePrefabSnapshot(levelPrefab, Color.gray, new Vector3(0,0,10), Quaternion.identity, Vector3.one * 0.2f, (int)grid.cellSize.x, (int)grid.cellSize.y);
            levelButtons[i].Initialize(GM.I.gp.CurrentWorld, i, levelTexture);
        }

        base.Initialize();
    }

}
