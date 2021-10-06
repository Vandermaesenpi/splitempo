using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScaleBop : BeatListener
{
    Vector3 startScale;
    public float scaleModifier;
    public float bopSpeed;
    private void Start() {
        startScale = transform.localScale;
    }
    public override void OnBeat()
    {
        transform.localScale = startScale * scaleModifier;
        base.OnBeat();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startScale, Time.deltaTime * bopSpeed);
    }
}
