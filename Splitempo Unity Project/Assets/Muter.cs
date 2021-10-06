using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muter : MonoBehaviour
{
    public bool music;
    public AudioSource source;
    private void OnEnable() {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        source.mute = music? GM.I.MusicMute : GM.I.SFXMute;
    }
}
