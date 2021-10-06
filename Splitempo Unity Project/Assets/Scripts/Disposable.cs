using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disposable : MonoBehaviour
{
    public float length;

    public void Dispose()
    {
        StartCoroutine(DisposeAudioSource());
    }

    IEnumerator DisposeAudioSource(){
        yield return new WaitForSeconds(length);
        Destroy(gameObject);

    }

    public void Dispose(Vector3 dir)
    {
        StartCoroutine(DisposeAtom(dir));
    }

    IEnumerator DisposeAtom(Vector3 dir){
        float t = 0;
        while (t < length)
        {
             transform.position += dir * (1f - t/length) * Time.deltaTime;
             transform.localScale = Vector3.Lerp(Vector3.one*0.5f, Vector3.zero, t/length);
             t+= Time.deltaTime;
             yield return 0;
        }
        Destroy(gameObject);
    }
}
