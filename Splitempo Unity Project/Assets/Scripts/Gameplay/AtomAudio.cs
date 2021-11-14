using UnityEngine;

internal class AtomAudio : MonoBehaviour
{
    [SerializeField] private AudioClip sfxSplit;
    [SerializeField] private AudioClip sfxDestroy;

    Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    public void PlaySFXSplit(){
        AudioManager.PlaySFX(sfxSplit, _transform.position);
    }

    public void PlaySFXDestroy(){
        AudioManager.PlaySFX(sfxDestroy, _transform.position);
    }
}