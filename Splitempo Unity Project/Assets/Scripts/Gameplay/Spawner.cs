using System.Collections.Generic;
using UnityEngine;


public class Spawner : BeatListener
{
    [SerializeField] private GameObject _offspring;
    [SerializeField] private AudioClip _sfxSpawn;

    private Transform _transform;

    private void Start() {
        _transform = transform;
    }

    public override void OnNotePlay()
    {
        if(!GM.I.gp.CurrentLevel.IsPlaying){return;}

        float startAngle = Random.Range(0f, 180f);
        List<Atom> children = new List<Atom>();
        Vector3 dir = Quaternion.Euler(0, 0, startAngle) * Vector3.right;
        Atom newAtom = Instantiate(_offspring, _transform.position, Random.rotation).GetComponent<Atom>();
        newAtom.transform.parent = _transform.parent;
        AudioManager.PlaySFX(_sfxSpawn, _transform.position);
        newAtom.Spawn(dir);
        children.Add(newAtom);
        GM.I.gp.CurrentLevel.AddNewAtoms(children);
        base.OnNotePlay();
    }
}