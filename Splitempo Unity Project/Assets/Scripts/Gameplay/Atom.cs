using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Atom : BeatListener,IInteractable
{
    public bool isCollectable;
    public List<GameObject> splitParts;
    public float rotSpeed;
    public float splitPitch;    
    bool waitForBeat = false;
    private Spawner _spawner;
    private AtomAudio _atomAudio;
    public bool colored = false;
    public bool blue = true;
    Vector3 _randomRotation;
    Vector3 _direction;
    Vector3 _spawnDirection = Vector3.zero;

    private void Awake() {
        _spawner = GetComponent<Spawner>();
        _atomAudio = GetComponent<AtomAudio>();
    }

    private void Start() {
        _randomRotation = Random.onUnitSphere;
    }

    public void Spawn(Vector3 d){
        _spawnDirection = d * 10f;
    }

    public void Split(Vector3 d, PlayerBall ball){
        if(colored && ball.blue != blue){return;}
        _atomAudio.PlaySFXPreSplit();
        _direction = d.normalized;
        waitForBeat = true;
        GetComponent<Collider>().enabled = false;
    }

    private void FixedUpdate() {
        if(_spawnDirection != Vector3.zero){
            transform.position += _spawnDirection * Time.fixedDeltaTime;
            _spawnDirection *= 0.95f;
            if(_spawnDirection.magnitude < 0.01f){
                _spawnDirection = Vector3.zero;
            }
        }
        if(waitForBeat){
            transform.localScale *= 0.99f;
            rotSpeed *= 1.05f;
        }
        transform.Rotate(_randomRotation * rotSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Collider" && _spawnDirection != Vector3.zero){
            _spawnDirection = Vector3.Reflect(_spawnDirection, other.GetContact(0).normal);
        }
    }

    public override void OnNotePlay()
    {
        if(waitForBeat){
            if(isCollectable)
            {
                SplitCollectable();
            }
            else
            {
                SplitNormal();
            }
        }
        _spawner?.Spawn();
        base.OnNotePlay();
    }

    private void SplitNormal()
    {
        _atomAudio.PlaySFXSplit();
        // Spawn offsprings
        float startAngle = 180f / (float)splitParts.Count;
        List<Atom> children = new List<Atom>();
        for (int i = 0; i < splitParts.Count; i++)
        {

            Vector3 dir = Quaternion.Euler(0, 0, startAngle + (360f / (float)splitParts.Count) * (float)i) * _direction;
            Atom newAtom = Instantiate(splitParts[i], transform.position, Random.rotation).GetComponent<Atom>();
            newAtom.transform.parent = transform.parent;
            newAtom.Spawn(dir);
            children.Add(newAtom);
        }
        GM.I.gp.Split(this, children);
        Destroy(gameObject);
    }

    private void SplitCollectable()
    {
        _atomAudio.PlaySFXDestroy();
        GM.I.gp.Split(this, null);
        Destroy(gameObject);
        float startAngle = 180f / (float)splitParts.Count;
        for (int i = 0; i < splitParts.Count; i++)
        {
            Vector3 dir = Quaternion.Euler(0, 0, startAngle + (360f / (float)splitParts.Count) * (float)i) * _direction;
            Disposable newAtom = Instantiate(splitParts[i], transform.position, Random.rotation).GetComponent<Disposable>();
            newAtom.Dispose(dir);
        }
    }


    public void Interact(BallMovement interactor, RaycastHit hit)
    {
        interactor.transform.position = hit.transform.position;
        Split(interactor.CurrentDirection, interactor.MyPlayerBall);
    }
}