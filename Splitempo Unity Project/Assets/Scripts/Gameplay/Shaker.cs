using UnityEngine;
public class Shaker : MonoBehaviour {
    
    [SerializeField] private float _shakeStrength;
    [SerializeField] private float _shakeSpeed;
    Vector3 _startPos;
    Transform _transform;
    private void Awake() {
        _transform = transform;
    }

    private void Start() {
        _startPos = _transform.position;
    }

    public void Shake(Vector3 pos){
        Vector3 direction = _transform.position - pos;
        _transform.position = _startPos + direction * _shakeStrength;
    }   

    private void Update() {
        _transform.position = Vector3.Lerp(_transform.position, _startPos, Time.deltaTime * _shakeSpeed);
    }
}