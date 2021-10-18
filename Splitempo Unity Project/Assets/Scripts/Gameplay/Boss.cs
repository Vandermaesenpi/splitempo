using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BeatListener, IInteractable
{
    private AtomAudio _atomAudio;
    private Transform _transform;
    [SerializeField] private AnimationCurve hurtAnim;
    [SerializeField] private Transform _hurtShakeTransform;
    [SerializeField] private GameObject nextBoss;
    [SerializeField] private int health;
    [SerializeField] private int id;
    
    bool waitForBeat = false;
    bool waitForDeath = false;
    float rotSpeed = 0;
    Vector3 direction;

    public bool IsLastBoss => id == 2;

    private void Awake() {
        _atomAudio = GetComponent<AtomAudio>();
        _transform = transform;
    }

    public void Split(Vector3 d){
        direction = d.normalized;
        waitForBeat = true;
    }

    private void Update() {
        if(waitForDeath){
            _transform.localScale *=0.99f;
            rotSpeed *=  1.05f;
            _transform.Rotate(Random.onUnitSphere * rotSpeed * Time.deltaTime);
        }
    }

    IEnumerator HurtRoutine(){
        float t = 0;
        Vector3 startPos = _hurtShakeTransform.localPosition;
        while ( t < 1)
        {
            _hurtShakeTransform.localPosition = Vector3.Lerp(startPos, startPos - direction, hurtAnim.Evaluate(t));
            t += Time.deltaTime;
            yield return null;
        }
        _hurtShakeTransform.localPosition = startPos;

    }

    public override void OnNotePlay()
    {
        if(waitForBeat){
            waitForBeat = false;
            StartCoroutine(HurtRoutine());
            CameraManager.I.StartPostProcessingEffect(PostProcessEffectType.BossHurt);
            _atomAudio.PlaySFXSplit();
            health --;
            if(health <= 0){
                Die();
            }
        }
        base.OnNotePlay();
    }

    public void Die(){
    }

    IEnumerator DeathRoutine(){
        rotSpeed = 600f;
        if(IsLastBoss)
            GM.I.gp.CurrentLevel.StopLevel();

        GetComponent<Collider>().enabled = false;

        yield return BeatManager.WaitUntilBeatInBar(0);

        _atomAudio.PlaySFXDestroy();
        GM.I.gp.CurrentLevel.BossPhaseEnd(id);
        if(nextBoss != null){
            nextBoss.SetActive(true);
        }
        Destroy(gameObject);
    }

    public void Interact(BallMovement interactor, RaycastHit hit)
    {
        interactor.transform.position = hit.transform.position;
        interactor.Reflect(hit.normal);
        hit.transform.gameObject.GetComponent<Boss>().Split(interactor.CurrentDirection);
    }
}
