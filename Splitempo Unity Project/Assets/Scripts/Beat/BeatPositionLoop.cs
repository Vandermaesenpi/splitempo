using UnityEngine;

public class BeatPositionLoop : BeatAnimator
{
    [SerializeField] private PositionsLoop positionsLoop;
    
    [SerializeField]private int currentStep = 0;
    private Transform _transform;
    private Vector3 _targetPosition, _startPosition, _endPosition;

    private void Awake() {
        _transform = transform;
    }

    private void FixedUpdate() {
        _transform.localPosition = _targetPosition;
    }

    public override void SetupAnimation()
    {
        _startPosition = positionsLoop.GetWorldPositionAt(currentStep);
        currentStep = positionsLoop.GetNextPosition(currentStep);
        _endPosition = positionsLoop.GetWorldPositionAt(currentStep);
        _targetPosition = _startPosition;
        base.SetupAnimation();
    }

    public override void ExecuteAnimation(float _time)
    {
        _targetPosition = GetPositionAtTime(_time);
        base.ExecuteAnimation(_time);
    }

    private Vector3 GetPositionAtTime(float _time)
    {
        return Vector3.Lerp(_startPosition, _endPosition, _time);
    }

    public override void EndAnimation()
    {
        _targetPosition = GetPositionAtTime(1);
        base.EndAnimation();
    }

}
