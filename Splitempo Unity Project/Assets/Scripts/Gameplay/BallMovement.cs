using System;
using UnityEngine;

[System.Serializable]
public class BallMovement : BeatListener
{
    [SerializeField] private Vector4 _bounds;
    [SerializeField] private Vector3 _currentDirection;
    [SerializeField] private LayerMask _raycastMask;

    [SerializeField] private float _kickStrength;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _radius;

    [SerializeField] private bool _waitingForBounce;
    [SerializeField] private bool _waitingForHurt;

    private Transform _transform;
    private PlayerBall _playerBall;
    public PlayerBall MyPlayerBall { 
        get {
            return _playerBall;
        }
    }
    public Vector3 CurrentDirection => _currentDirection;

    private void Awake() {
        _playerBall = GetComponent<PlayerBall>();
        _transform = transform;        
    }

    private void FixedUpdate()
    {
        if (_waitingForBounce || _waitingForHurt) { return; }
        Move();
        ClampPositionToBounds();
    }

    private void ClampPositionToBounds()
    {
        _transform.position = new Vector3(Mathf.Clamp(_transform.position.x, _bounds.x, _bounds.y), Mathf.Clamp(_transform.position.y, _bounds.z, _bounds.w), 0);
    }

    public void Move(){

        _currentDirection = _currentDirection.normalized * Mathf.Clamp(_currentDirection.magnitude, 0, _maxVelocity);
        Vector3 movementVector = _currentDirection * Time.fixedDeltaTime;
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, movementVector, out hit, movementVector.magnitude, _raycastMask))
        {
            _transform.position = hit.point - movementVector.normalized * _radius;
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.Interact(this, hit);
        }else{
            _transform.position += movementVector;
            _currentDirection *= 1f - _deceleration * Time.fixedDeltaTime;
            _transform.position = new Vector3(_transform.position.x, _transform.position.y, 0);
        }

    }

    public void KickBall(Vector3 direction){
        if(!_waitingForBounce || _waitingForHurt){
            _currentDirection += direction * _kickStrength;
        }
    }

    public override void OnNotePlay()
    {
        if(_waitingForBounce){
            MyPlayerBall.Bounce();
        }
        _waitingForBounce = false;
        if(_waitingForHurt){
            MyPlayerBall.Hurt();
        }
        _waitingForHurt = false;
        base.OnNotePlay();
    }

    internal void Reflect(Vector3 normal)
    {
        _currentDirection = Vector3.Reflect(_currentDirection, normal);
        _waitingForBounce = true;
    }
}