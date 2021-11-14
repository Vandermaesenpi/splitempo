using System;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : BeatListener
{
    private List<Atom> currentCombo = new List<Atom>();
    [SerializeField] private List<AudioClip> comboEndSounds;
    [SerializeField] private List<AudioClip> comboBeatSounds;
    public void AddToCombo(Atom atom, int childrenCount){
        currentCombo.Add(atom);
        AudioManager.PlaySFX(comboBeatSounds[BeatManager.I.CurrentBeatInBar]);
    }

    public override void OnNotePlay()
    {
        if(currentCombo.Count > 0){
            EndCombo();
        }
        base.OnNotePlay();
    }

    private void EndCombo()
    {
        bool AtLeastOneAtomSplit = false;
        for (int i = currentCombo.Count - 1; i >= 0; i--)
        {
            Atom atom = currentCombo[i];
            if(atom.EndCombo(currentCombo)){
                AtLeastOneAtomSplit = true;
            }
        }

        if(AtLeastOneAtomSplit){
            AudioManager.PlaySFX(comboEndSounds[Mathf.Min(currentCombo.Count/2, comboEndSounds.Count-1)]);
        }

        GM.I.gp.CheckWinState();
        currentCombo.Clear();
    }
}