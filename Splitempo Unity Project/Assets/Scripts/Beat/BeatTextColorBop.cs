using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatTextColorBop : BeatColorAnimator
{
    Text _myText;
    private void Awake() {
        _myText = GetComponent<Text>();
    }

    public override void SetColor(Color _color)
    {
        _myText.color = _color;
        base.SetColor(_color);
    }

}
