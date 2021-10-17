using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallInputManager
{
    [SerializeField] private BallInputGraphics _ballInputGraphics;
    private Player _player;
    
    [SerializeField] private float _deceleration;
    Vector3 _anchoredMousePos;
    Vector3 _currentMousePos;
    Camera _camera;
    
    private bool _interactable = false;
    private bool IsStretchedEnough => Vector3.Distance(_anchoredMousePos, _currentMousePos) > 0.1f;
    private Vector3 GetMousePointerPosition => _camera.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, _currentMousePos.z);

    public void Initialize(Player p){
        _player = p;
        _camera = GM.I.cam.gameCamera;
        _interactable = true;
        _ballInputGraphics.Initialize();
    }
    public void StopInput()
    {
        _interactable = false;
        _deceleration *= 0.5f;
        _ballInputGraphics.Hide();
    }
    private void Update() {
        if (_interactable){
            HandleInputs();
        }
    }

    public void HandleInputs(){
        if(Input.GetMouseButtonDown(0))
        {
            SetAnchorPosition();
        }
        else if(Input.GetMouseButton(0)){
            _currentMousePos = GetMousePointerPosition;
            if(IsStretchedEnough){
                _ballInputGraphics.UpdateGraphics(_anchoredMousePos, _currentMousePos);
                _player.UpdateBallsTargetRay(_anchoredMousePos, _currentMousePos);
            }
        }else if (Input.GetMouseButtonUp(0)){
            _player.KickBalls(_currentMousePos - _anchoredMousePos);        
            _ballInputGraphics.Hide();
        }else{
            _player.HideBallsTargetRay();
            _currentMousePos = GetMousePointerPosition;
        }
    }

    private void SetAnchorPosition()
    {
        _currentMousePos = GetMousePointerPosition;
        _anchoredMousePos = _currentMousePos;
    }

    
}