using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallInputGraphics : MonoBehaviour
{
    [SerializeField] private LineRenderer _pole;
    private GameObject _poleGameObject;

    private void Awake() {
        _poleGameObject = _pole.gameObject;
    }
    internal void Initialize()
    {
        _poleGameObject.SetActive(true);
    }

    internal void Hide()
    {
        _poleGameObject.SetActive(false);
    }

    internal void UpdateGraphics(Vector3 _mousePos, Vector3 _currentMousePos)
    {
        if(!_poleGameObject.activeSelf){_poleGameObject.SetActive(true);}
        _pole.SetPosition(0,  _mousePos - Vector3.forward * _mousePos.z - Vector3.forward* 9f);
        _pole.SetPosition(1, _currentMousePos - Vector3.forward * _mousePos.z- Vector3.forward * 9f);
    }
}