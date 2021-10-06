using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRotator : BeatListener
{
    public int animTime;
    public AnimationCurve curve;
    public float rotation;
    public override void OnBeat()
    {
        StartCoroutine(RotateRoutine());
        base.OnBeat();
    }

    IEnumerator RotateRoutine(){
        float rotationTime = (float)animTime * 60f/GM.I.am.bpm;
        float t = 0;
        Vector3 startForward = transform.right;
        Vector3 endForward = Quaternion.AngleAxis(rotation, Vector3.forward) * transform.right;
        while (t < rotationTime)
        {
             transform.right = Vector3.Lerp(startForward, endForward, curve.Evaluate(t/rotationTime));
             t += Time.deltaTime;
             yield return 0;
        }
        transform.right = endForward;
    }
}
