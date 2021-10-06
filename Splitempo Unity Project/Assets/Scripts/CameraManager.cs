using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraManager : BeatListener
{
    public PostProcessVolume rythmVolume;
    public List<PostProcessVolume> statusVolumes;
    public AnimationCurve hurtCurve;
    public Camera gameCamera;
    public float shakeStrength;
    public float shakeSpeed;
    public Vector2 ppWeightRange;
    Vector3 startPos;
    private void Start() {
        startPos = transform.position;
        rythmVolume.weight = ppWeightRange.x;
    }

    public void CamShake(Vector3 pos){
        Vector3 direction = transform.position - pos;
        transform.position = startPos + direction * shakeStrength;
    }

    public Coroutine volumeStatusRout;

    public void SetVolumeStatus(int i){
        if(volumeStatusRout != null){
            StopCoroutine(volumeStatusRout);
        }
        volumeStatusRout = StartCoroutine(StatusVolumeRoutine(i));
    }

    IEnumerator StatusVolumeRoutine(int i){
        if((i == 1 || i == 4) && GM.I.VFXMute){}else{
        foreach (PostProcessVolume v in statusVolumes)
        {
            switch (i){
                // Hurt
                case 1:
                    if(statusVolumes.IndexOf(v) == 1){
                        v.weight = 0;
                    }else{
                        v.weight = 0;
                    }
                break;
                // Win
                case 2:
                    if(statusVolumes.IndexOf(v) == 2){
                        v.weight = 0.1f;
                    }else{
                        v.weight = 0;
                    }
                break;

                // Loose
                case 3:
                    if(statusVolumes.IndexOf(v) == 3){
                        v.weight = 0.1f;
                    }else{
                        v.weight = 0;
                    }
                break;

                // Boss hurt
                case 4:
                    if(statusVolumes.IndexOf(v) == 4){
                        v.weight = 0f;
                    }else{
                        v.weight = 0;
                    }
                break;

                // Win Game
                case 5:
                    if(statusVolumes.IndexOf(v) == 5){
                        v.weight = 1f;
                    }else{
                        v.weight = 0;
                    }
                break;
            }
        }float t = 0;
        while (t < 1f)
        {
            foreach (PostProcessVolume v in statusVolumes)
            {
                
                switch (i){
                    // No effect
                    case 0:
                        v.weight = Mathf.Lerp(v.weight, 0, Time.deltaTime * 5f);

                    break;
                    // Hurt
                    case 1:
                        if(statusVolumes.IndexOf(v) == 1){
                            v.weight = Mathf.Lerp(0, 1, hurtCurve.Evaluate(t));
                        }else{
                            v.weight = 0;
                        }
                    break;
                    // Win
                    case 2:
                        if(statusVolumes.IndexOf(v) == 2){
                            v.weight = Mathf.Lerp(0, 1, t);
                        }else{
                            v.weight = 0;
                        }
                    break;

                    // Loose
                    case 3:
                        if(statusVolumes.IndexOf(v) == 3){
                            v.weight = Mathf.Lerp(0, 1, t);
                        }else{
                            v.weight = 0;
                        }
                    break;
                    case 4:
                        if(statusVolumes.IndexOf(v) == 4){
                            v.weight = Mathf.Lerp(0, 1, hurtCurve.Evaluate(t));
                        }else{
                            v.weight = 0;
                        }
                    break;
                    case 5:
                        if(statusVolumes.IndexOf(v) == 5){
                            v.weight = Mathf.Lerp(0, 1, t);
                        }else{
                            v.weight = 0;
                        }
                    break;
                }
                t += Time.deltaTime * GM.I.am.bpmInSeconds * 2f;
                yield return 0;
            }
        }


        foreach (PostProcessVolume v in statusVolumes)
        {
            switch (i){
                // No effect
                case 0:
                    v.weight = 0;

                break;
                // Hurt
                case 1:
                    if(statusVolumes.IndexOf(v) == 1){
                        v.weight = 0;
                    }else{
                        v.weight = 0;
                    }
                break;
                // Win
                case 2:
                    if(statusVolumes.IndexOf(v) == 2){
                        v.weight = 1;
                    }else{
                        v.weight = 0;
                    }
                break;

                // Loose
                case 3:
                    if(statusVolumes.IndexOf(v) == 3){
                        v.weight = 1;
                    }else{
                        v.weight = 0;
                    }
                break;

                case 4:
                    if(statusVolumes.IndexOf(v) == 4){
                        v.weight = 0f;
                    }else{
                        v.weight = 0;
                    }
                break;

                case 5:
                    if(statusVolumes.IndexOf(v) == 5){
                        v.weight = 0;
                    }else{
                        v.weight = 0;
                    }
                break;
            }
        }
        }
    }

    public override void OnBeat()
    {
        rythmVolume.weight = ppWeightRange.y;
        base.OnBeat();
    }
    private void Update() {
        if(!GM.I.VFXMute){
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * shakeSpeed);
            rythmVolume.weight = Mathf.Lerp(rythmVolume.weight, ppWeightRange.x, Time.deltaTime * 5f);
        }
        
    }

    
}
