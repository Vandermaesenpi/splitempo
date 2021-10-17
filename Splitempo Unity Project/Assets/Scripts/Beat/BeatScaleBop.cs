using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScaleBop : BeatAnimator
{
    Vector3 _startScale;
    Vector3 _endScale;
    Transform _transform;
    private void Awake() {
        _transform = transform;
        _startScale = _transform.localScale;
    }

    public override void SetupAnimation()
    {
        _endScale = _startScale * amount;
        _transform.localScale = GetScaleAtTime(0);
        base.SetupAnimation();
    }

    public override void ExecuteAnimation(float _time)
    {
        _transform.localScale = GetScaleAtTime(_time);
        base.ExecuteAnimation(_time);
    }

    private Vector3 GetScaleAtTime(float _time)
    {
        return Vector3.Lerp(_startScale, _endScale, _time);
    }

    public override void EndAnimation()
    {
        _transform.localScale = GetScaleAtTime(1);
        base.EndAnimation();
    }

}
