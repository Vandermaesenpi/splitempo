using System;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : BeatListener
{
    private List<Atom> currentCombo = new List<Atom>();
    [SerializeField] private List<AudioClip> comboEndSounds;
    public void AddToCombo(Atom atom){
        currentCombo.Add(atom);
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
        AudioManager.PlaySFX(comboEndSounds[Mathf.Min(currentCombo.Count, comboEndSounds.Count-1)]);
        for (int i = currentCombo.Count - 1; i >= 0; i--)
        {
            Atom atom = currentCombo[i];
            atom.Split();
            currentCombo.RemoveAt(i);
        }
        currentCombo.Clear();
    }
}