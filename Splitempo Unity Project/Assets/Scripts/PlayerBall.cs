using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : BeatListener
{
    [Header("References")]
    public Transform mousePointer;
    public LineRenderer pole;
    public LineRenderer directionLine;
    public TrailRenderer trail;
    public bool blue;
    Vector3 mousePos;

    [Header("Movement")]
    public Vector4 bounds;
    public Vector3 currentDirection;
    public LayerMask raycastMask;

    public float kickStrength;


    public float deceleration;
    public float maxVelocity;
    public float radius;

    public bool waitingForBounce;
    public bool waitingForHurt;

    [Header("Audio")]
    public AudioSource bounceSource;
    public List<AudioClip> bounce;
    public List<AudioClip> hurt;

    public bool interactable =false;

    public void Initialize(){
        interactable = true;
        mousePointer.gameObject.SetActive(true);
        pole.gameObject.SetActive(true);
    }
    public void StopInput()
    {
        interactable = false;
        deceleration *= 0.5f;
        mousePointer.gameObject.SetActive(false);
        pole.gameObject.SetActive(false);
    }
    private void Update() {
        if (interactable){
            HandleInputs();
        }

    }

    private void FixedUpdate() {
        if(waitingForBounce || waitingForHurt){return;}
        Move();
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bounds.x, bounds.y), Mathf.Clamp(transform.position.y, bounds.z, bounds.w),0);
    }

    public void HandleInputs(){
        if(Input.GetMouseButtonDown(0)){
            mousePointer.position = GM.I.cam.gameCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePointer.position = mousePointer.position - new Vector3(0,0,mousePointer.position.z);
            mousePos = mousePointer.position;
        }else if(Input.GetMouseButton(0)){
            mousePointer.position = GM.I.cam.gameCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePointer.position = mousePointer.position - new Vector3(0,0,mousePointer.position.z);
            if(Vector3.Distance(mousePos, mousePointer.position) > 0.1f){
                pole.SetPosition(0,  mousePos - Vector3.forward * mousePos.z - Vector3.forward* 9f);
                pole.SetPosition(1, mousePointer.position - Vector3.forward * mousePos.z- Vector3.forward * 9f);
                directionLine.SetPosition(0,  Vector3.zero);
                directionLine.SetPosition(1, -(mousePointer.position - mousePos).normalized * Mathf.Min((mousePointer.position - mousePos).magnitude, 2f));
            }
        }else if (Input.GetMouseButtonUp(0)){
            KickBall(-(mousePointer.position - mousePos), (mousePointer.position - mousePos).magnitude);
        
        }else{
            pole.SetPosition(0,  Vector3.one * 1000);
            pole.SetPosition(1, Vector3.one * 1000);
                directionLine.SetPosition(0,  Vector3.zero);
                directionLine.SetPosition(1,  Vector3.zero);

            mousePointer.position = GM.I.cam.gameCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePointer.position = mousePointer.position - new Vector3(0,0,mousePointer.position.z);
        }
    }

    public void Move(){
        
        currentDirection = currentDirection.normalized * Mathf.Clamp(currentDirection.magnitude, 0, maxVelocity);
        Vector3 movementVector = currentDirection * Time.fixedDeltaTime;
        Debug.DrawRay(transform.position, movementVector, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movementVector, out hit, movementVector.magnitude, raycastMask))
        {
            
            transform.position = hit.point - movementVector.normalized * radius;
            if(hit.collider.tag == "Collider"){
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                waitingForBounce = true;
                trail.Clear();
            
            }if(hit.collider.tag == "Lava"){
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                if(interactable){
                    waitingForHurt = true;
                }else{
                    waitingForBounce = true;
                }
                trail.Clear();
            
            }else if (hit.collider.tag == "Atom" && interactable){
                transform.position = hit.transform.position;
                hit.transform.gameObject.GetComponent<Atom>().Split(currentDirection, this);
            }else if (hit.collider.tag == "Boss" && interactable){
                transform.position = hit.transform.position;
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                waitingForBounce = true;
                hit.transform.gameObject.GetComponent<Boss>().Split(currentDirection, this);
            }
        }else{
            transform.position += movementVector;
            currentDirection *= 1f - deceleration * Time.fixedDeltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

    }

    public void KickBall(Vector3 direction, float force){
        if(!waitingForBounce || waitingForHurt){
            currentDirection += direction * force * kickStrength;
            GM.I.gp.shot = true;
        }

    }

    public override void OnBeat()
    {
        if(waitingForBounce){
            GM.I.cam.CamShake(-transform.position);
            Bounce();
            GM.I.gp.Bump();
        }
        waitingForBounce = false;
        if(waitingForHurt){
            GM.I.cam.CamShake(-transform.position * 3f);
            Hurt();
            GM.I.gp.Hurt();
        }
        waitingForHurt = false;
        base.OnBeat();
    }

    public void Bounce(){
        bounceSource.pitch = 1f;
        bounceSource.Stop();
        bounceSource.clip = bounce[GM.I.am.beat];
        bounceSource.Play();
    }

    public void Hurt(){
        bounceSource.pitch = 1f;
        bounceSource.Stop();
        bounceSource.clip = hurt[0];
        bounceSource.Play();
    }
}
