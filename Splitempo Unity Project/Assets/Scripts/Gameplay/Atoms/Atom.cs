using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Atom : MonoBehaviour,IInteractable
{
    public List<GameObject> splitParts;
    public GameObject splitVFX;
    public float rotSpeed;
    public float splitPitch;    
    private Spawner _spawner;
    private AtomAudio _atomAudio;
    public bool colored = false;
    public bool blue = true;

    [HideInInspector] public Vector3 direction;
    Vector3 _spawnDirection = Vector3.zero;
    Vector3 _randomRotation;

    private void Awake() {
        _atomAudio = GetComponent<AtomAudio>();
    }

    private void Start() {
        _randomRotation = Random.onUnitSphere;
    }

    public void Spawn(Vector3 d){
        _spawnDirection = d * 10f;
    }

    public virtual void PreSplit(Vector3 d, PlayerBall ball){
        direction = d.normalized;
        rotSpeed *= 3f;
        Disposable splitVFXObject = Instantiate(splitVFX, transform.position, Quaternion.identity).GetComponent<Disposable>();
        splitVFXObject.transform.up = d;
        splitVFXObject.Dispose();
    }


    public virtual void CancelSplit()
    {
        rotSpeed /= 3f;
        GetComponent<Collider>().enabled = true;
    }

    private void FixedUpdate() {
        
        if(_spawnDirection != Vector3.zero){
            transform.position += _spawnDirection * Time.fixedDeltaTime;
            _spawnDirection *= 0.95f;
            if(_spawnDirection.magnitude < 0.01f){
                _spawnDirection = Vector3.zero;
            }
        }
        transform.Rotate(_randomRotation * rotSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Collider" && _spawnDirection != Vector3.zero){
            _spawnDirection = Vector3.Reflect(_spawnDirection, other.GetContact(0).normal);
        }
    }

    public void Interact(BallMovement interactor, RaycastHit hit)
    {
        interactor.transform.position = hit.transform.position;
        rotSpeed *= 2;
        PreSplit(interactor.CurrentDirection, interactor.MyPlayerBall);

    }
    public void Split(){
        StartCoroutine(SplitRoutine());
    }

    public virtual IEnumerator SplitRoutine(){
        yield return 0;
    }

    public virtual bool EndCombo(List<Atom> currentCombo) {
        Split();
        return true;
    }
}
