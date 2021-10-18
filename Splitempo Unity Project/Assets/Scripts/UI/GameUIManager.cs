using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    GameplayManager _gameplayManager;
    public Image stabilityFiller, stabilityBkg;
    public Text stabilityText, shotsText;

    public void SetManager(GameplayManager gameplayManager)
    {
        _gameplayManager = gameplayManager;
        SetEvents();
    }

    private void SetEvents()
    {
        print(_gameplayManager.onStabilityChanged);
        _gameplayManager.onStabilityChanged.AddListener(UpdateStabilityUI);
        _gameplayManager.onShotsChanged.AddListener(UpdateShotsUI);
    }

    private void OnDestroy() {
        _gameplayManager.onStabilityChanged.RemoveListener(UpdateStabilityUI);
        _gameplayManager.onShotsChanged.RemoveListener(UpdateShotsUI); 
    }

    private void UpdateShotsUI(int shots)
    {
        shotsText.text = shots.ToString();
    }

    private void UpdateStabilityUI(int stability, float stabilityRatio)
    {
        stabilityFiller.color = stabilityRatio > 0.5f ? Color.white : Color.red;
        stabilityText.color = stabilityRatio > 0.5f ? Color.white : Color.red;
        stabilityBkg.color = stabilityRatio > 0.25f ? Color.white : Color.red;
        stabilityFiller.fillAmount = stabilityRatio;
        stabilityText.text = stability.ToString();
    }



}
