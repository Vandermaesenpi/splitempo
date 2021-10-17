using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMaterialColorBop : BeatColorAnimator
{
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    
    private void Awake() {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    public override void SetColor(Color _color){
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetColor("_OutlineColor", _color);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
