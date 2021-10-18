using System;

class StabilityManager : BeatListener
{

    private int stability;
    private float stabilityRatio => (float)stability / (float)_maxStability;
    private float _maxStability;
    private GameplayManager _gameplayManager;

    public void SetManager(GameplayManager gameplayManager)
    {
        _gameplayManager = gameplayManager;
    }

    public override void OnNotePlay()
    {
        if(_gameplayManager.CurrentLevel == null){return;}
        if(!_gameplayManager.CurrentLevel.IsPlaying){return;}
        DecreaseStability(1);
    }

    public void Initialize(int maxStability){
        stability = maxStability;
        _maxStability = maxStability;
    }

    public void DecreaseStability(int amount){
        stability-= amount;
        _gameplayManager.onStabilityChanged.Invoke(stability, stabilityRatio);
        if(stability <= 0){
            _gameplayManager.LooseLevel();
        }
    }

}