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
    }
    public override void CancelSplit()
    {
        GetComponent<Collider>().enabled = true;
        base.CancelSplit();
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