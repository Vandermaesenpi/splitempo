using UnityEngine;

internal class AtomAudio : MonoBehaviour
{
    [SerializeField] private AudioClip sfxPreSplit;
    [SerializeField] private AudioClip sfxSplit;
    [SerializeField] private AudioClip sfxDestroy;

    Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    public void PlaySFXPreSplit(){
        AudioManager.PlaySFX(sfxPreSplit, _transform.position);
    }

    public void PlaySFXSplit(){
        AudioManager.PlaySFX(sfxSplit, _transform.position);
    }

    public void PlaySFXDestroy(){
        AudioManager.PlaySFX(sfxDestroy, _transform.position);
    }
}