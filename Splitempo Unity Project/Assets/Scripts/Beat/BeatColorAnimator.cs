using System.Collections;
using UnityEngine;
public class BeatColorAnimator : BeatAnimator
{
    [SerializeField] private Color _startColor, _targetColor;

    
    public override void OnNotePlay()
    {
        base.OnNotePlay();
    }

    public override void SetupAnimation()
    {
        SetColor(GetColorAt(0));
        base.SetupAnimation();
    }

    public override void ExecuteAnimation(float _time)
    {
        SetColor(GetColorAt(_time));
        base.ExecuteAnimation(_time);
    }

    public override void EndAnimation()
    {
        SetColor(GetColorAt(1));
        base.EndAnimation();
    }

    Color GetColorAt(float _time){
        return Color.Lerp(_startColor, _targetColor,GetAmountAt(_time));
    }

    public virtual void SetColor(Color _color){

    }
}