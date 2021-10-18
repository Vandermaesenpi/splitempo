using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] Text _textLevelName;
    [SerializeField] RawImage _levelImage;
    [SerializeField] List<Image> stars;
    LevelManager _level;
    int _levelId;
    
    Button _button;
    public void Initialize(World world , int levelId, Texture2D levelTexture){
        _levelId = levelId;
        _level = world.levels[_levelId];
        _levelImage.texture = levelTexture;
    }

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LevelButtonClicked);
    }

    private void LevelButtonClicked()
    {
        GM.I.StartGame(_levelId);
    }
    
}
