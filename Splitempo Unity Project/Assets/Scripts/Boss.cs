using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BeatListener
{
    public AnimationCurve hurtAnim;
    public Transform m;
    bool waitForBeat = false;
    bool waitForDeath = false;

    public GameObject nextBoss;
    public int health;
    public int id;
    float rotSpeed = 0;

    Vector3 direction;

    public void Split(Vector3 d, PlayerBall ball){
        direction = d.normalized;
        waitForBeat = true;
        
    }

    private void Update() {
        if(waitForDeath){
            transform.localScale *=0.99f;
            rotSpeed *=  1.05f;
            transform.Rotate(Random.onUnitSphere * rotSpeed * Time.deltaTime);
        }
    }

    IEnumerator HurtRoutine(){
        float t = 0;
        Vector3 startPos = m.localPosition;
        while ( t < 1)
        {
            m.localPosition = Vector3.Lerp(startPos, startPos - direction, hurtAnim.Evaluate(t));
            t += Time.deltaTime;
            yield return null;
        }
        m.localPosition = startPos;

    }

    public override void OnBeat()
    {
        if(waitForDeath && GM.I.am.beat == 0){
            Collect();
            sfxSource.transform.parent = transform.parent;
                sfxSource.GetComponent<Disposable>().Dispose();
            GM.I.gameplay.currentLevel.BossPhaseEnd(id);
            if(nextBoss != null){
                nextBoss.SetActive(true);
            }
            Destroy(gameObject);
        }
        if(waitForBeat){
            waitForBeat = false;
            StartCoroutine(HurtRoutine());
            GM.I.cam.SetVolumeStatus(4);
            Split();
            health --;

            if(health <= 0){
                Die();
            }
        }else{
            
        }
        base.OnBeat();
    }

    public void Die(){
        waitForDeath = true;
        rotSpeed = 600f;
        if(id == 2)
            GM.I.gameplay.currentLevel.player.StopInput();

        GetComponent<Collider>().enabled = false;
    }

    public AudioSource sfxSource;
    public List<AudioClip> sfx;
    public void PreSplit(float pitch){
        sfxSource.Stop();
        sfxSource.pitch = 1f;
        sfxSource.clip = sfx[0];
        sfxSource.Play();
    }
    public void Split(){
        sfxSource.Stop();
        sfxSource.pitch = 1f;
        sfxSource.clip = sfx[1];
        sfxSource.Play();
    }
    public void Collect(){
        sfxSource.Stop();
        sfxSource.pitch = 1f;
        sfxSource.clip = sfx[2];
        sfxSource.Play();
    }
}
