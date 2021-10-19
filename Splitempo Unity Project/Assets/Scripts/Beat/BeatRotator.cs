using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRotator : BeatAnimator
{
    Vector3 _startEuler;
    Vector3 _endEuler;
    Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    public override void SetupAnimation()
    {
        
        _startEuler = new Vector3(0,0,transform.eulerAngles.z);
        _endEuler = new Vector3(0,0,_startEuler.z + amount);
        base.SetupAnimation();
    }

    public override void ExecuteAnimation(float _time)
    {
        _transform.rotation = Quaternion.Lerp(Quaternion.Euler(_startEuler),Quaternion.Euler(_endEuler), _time);
        base.ExecuteAnimation(_time);
    }

    public override void EndAnimation()
    {
        _transform.rotation = Quaternion.Euler(_endEuler);
        base.EndAnimation();
    }

}
