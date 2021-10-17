using System.Collections;
using UnityEngine;

public class BeatAnimator : BeatListener
{
    public AnimationCurve curve;
    public float amount;

    [Tooltip("If set to 0, the animation time will be the duration in beetween the notes")]
    [SerializeField] private int _fixedAnimationTime;

    public int AnimationTime => _fixedAnimationTime != 0 ? _fixedAnimationTime : _sequencerPattern.BeatsBeetweenCurrentNotes;
    

    Coroutine currentAnimationRoutine;
    public override void OnNotePlay()
    {
        StartAnimation();
        base.OnNotePlay();
    }

    private void StartAnimation()
    {
        if (currentAnimationRoutine != null) { StopCoroutine(currentAnimationRoutine); }
        currentAnimationRoutine = StartCoroutine(AnimationRoutine());
    }

    IEnumerator AnimationRoutine(){
        float animationTime = BeatManager.BeatToSeconds(AnimationTime);
        float t = 0;
        SetupAnimation();
        while (t < animationTime)
        {
            ExecuteAnimation(GetAmountAt(animationTime / t));
            t += Time.deltaTime;
            yield return 0;
        }
        EndAnimation();
    }

    public float GetAmountAt(float t)
    {
        return curve.Evaluate(t);
    }

    public virtual void SetupAnimation(){} 

    public virtual void ExecuteAnimation(float _time){} 

    public virtual void EndAnimation(){} 

}