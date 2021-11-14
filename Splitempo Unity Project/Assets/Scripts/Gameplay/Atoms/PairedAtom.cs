using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PairedAtom : Atom
{
    public PairedAtom pair;
    
    [HideInInspector]
    public PairedAtomLine pairLine;

    public override void PreSplit(Vector3 d, PlayerBall ball)
    {
        base.PreSplit(d, ball);
        GM.I.gp.PreSplit(this, 0);
        GetComponent<Collider>().enabled = false;
    }
    public override void CancelSplit()
    {
        GetComponent<Collider>().enabled = true;
        base.CancelSplit();
    }

    public override IEnumerator SplitRoutine()
    {

        float startAngle = 180f / (float)splitParts.Count;
        for (int i = 0; i < splitParts.Count; i++)
        {
            Vector3 dir = GetDirection(startAngle, i);
            Disposable newAtom = Instantiate(splitParts[i], transform.position, Random.rotation).GetComponent<Disposable>();
            newAtom.Dispose(dir);
        }
        yield return 0;
        GM.I.gp.CurrentLevel.SplitAtom(this);
        Destroy(gameObject);
    }

    public override bool EndCombo(List<Atom> currentCombo)
    {
        if(currentCombo.Contains(((Atom)pair)))
        {
            pairLine?.DestroyLine();
            Split();
            return true;
        }else{
            CancelSplit();
            return false;
        }
    }
}