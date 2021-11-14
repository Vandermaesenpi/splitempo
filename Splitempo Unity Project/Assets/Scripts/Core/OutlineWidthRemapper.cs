using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineWidthRemapper : MonoBehaviour
{    
    private Renderer _renderer;
    private Transform _transform;
    private MaterialPropertyBlock _materialPropertyBlock;
    
    private void Awake() {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _transform = transform;
    }

    private void Start() {
        UpdateOutline();
    }
    public void UpdateOutline(){
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat("_Outline", 0.1f/Mathf.Max(_transform.localScale.x, _transform.localScale.y));
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
