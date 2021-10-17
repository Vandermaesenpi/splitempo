using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRotator : BeatAnimator
{
    Vector3 _startForward;
    Vector3 _endForward;
    Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    public override void SetupAnimation()
    {
        _startForward = transform.right;
        _endForward = Quaternion.AngleAxis(amount, Vector3.forward) * transform.right;
        base.SetupAnimation();
    }

    public override void ExecuteAnimation(float _time)
    {
        _transform.right = Vector3.Lerp(_startForward, _endForward, _time);
        base.ExecuteAnimation(_time);
    }

    public override void EndAnimation()
    {
        _transform.right = _endForward;
        base.EndAnimation();
    }

}
