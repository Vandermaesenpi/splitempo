using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Text music, sfx, vfx;

    // Update is called once per frame
    void Update()
    {
        music.text = "MUSIC = "+(GM.I.MusicMute? "OFF" : "ON"); 
        sfx.text = "SFX = "+(GM.I.SFXMute? "OFF" : "ON"); 
        vfx.text = "VIDEO EFFECTS = "+(GM.I.VFXMute? "OFF" : "ON"); 
    }
}
