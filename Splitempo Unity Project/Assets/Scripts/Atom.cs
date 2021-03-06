using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : BeatListener
{
    public bool isCollectable;
    public List<GameObject> splitParts;
    public float rotSpeed;
    public float splitPitch;
    
    bool waitForBeat = false;
    public bool virus = false;
    public bool colored = false;
    public bool blue = true;
    public GameObject virusOffspring;

    Vector3 rot;
    Vector3 direction;
    Vector3 spawnDirection = Vector3.zero;

    private void Start() {
        rot = Random.onUnitSphere;
    }

    public void Spawn(Vector3 d){
        spawnDirection = d * 10f;
    }

    public void Split(Vector3 d, PlayerBall ball){
        if(colored && ball.blue != blue){return;}
        PreSplit(splitPitch);
        direction = d.normalized;
        waitForBeat = true;
        GetComponent<Collider>().enabled = false;
    }

    private void Update() {
        if(spawnDirection != Vector3.zero){
            transform.position += spawnDirection * Time.deltaTime;
            spawnDirection *= 0.95f;
            if(spawnDirection.magnitude < 0.01f){
                spawnDirection = Vector3.zero;
            }
        }
        if(waitForBeat){
            transform.localScale *= 0.99f;
            rotSpeed *= 1.05f;
            transform.Rotate(rot * rotSpeed * Time.deltaTime);
        }else{
            transform.Rotate(rot * rotSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Collider" && spawnDirection != Vector3.zero){
            spawnDirection = Vector3.Reflect(spawnDirection, other.GetContact(0).normal);
        }
    }

    public override void OnBeat()
    {
        if(waitForBeat){
            sfxSource.transform.parent = transform.parent;
            sfxSource.GetComponent<Disposable>().Dispose();
            if(isCollectable){
                Collect();
                GM.I.gameplay.Split(this, null);
                Destroy(gameObject);
                float startAngle = 180f/(float)splitParts.Count;
                for (int i = 0; i < splitParts.Count; i++)
                {
                    Vector3 dir = Quaternion.Euler(0,0,startAngle + (360f/(float)splitParts.Count)*(float)i) * direction; 
                    Disposable newAtom = Instantiate(splitParts[i], transform.position, Random.rotation).GetComponent<Disposable>();
                    newAtom.Dispose(dir);
                }
            }else{
                Split();
                // Spawn offsprings
                float startAngle = 180f/(float)splitParts.Count;
                List<Atom> children = new List<Atom>();
                for (int i = 0; i < splitParts.Count; i++)
                {
                    
                    Vector3 dir = Quaternion.Euler(0,0,startAngle + (360f/(float)splitParts.Count)*(float)i) * direction; 
                    Atom newAtom = Instantiate(splitParts[i], transform.position, Random.rotation).GetComponent<Atom>();
                    newAtom.transform.parent = transform.parent;
                    newAtom.Spawn(dir);
                    children.Add(newAtom);
                }
                GM.I.gameplay.Split(this, children);
                Destroy(gameObject);
            }
        }else if (virus && GM.I.gameplay.currentLevel.player.playerBalls[0].interactable){
                Split();
            float startAngle = Random.Range(0f, 180f);
                List<Atom> children = new List<Atom>();
                Vector3 dir = Quaternion.Euler(0,0,startAngle) * Vector3.right; 
                Atom newAtom = Instantiate(virusOffspring, transform.position, Random.rotation).GetComponent<Atom>();
                newAtom.transform.parent = transform.parent;
                newAtom.Spawn(dir);
                children.Add(newAtom);
                GM.I.gameplay.Split(null, children);
        }
        base.OnBeat();
    }

    public AudioSource sfxSource;
    public List<AudioClip> sfx;
    public void PreSplit(float pitch){
        sfxSource.Stop();
        sfxSource.pitch = 0.5f;
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
