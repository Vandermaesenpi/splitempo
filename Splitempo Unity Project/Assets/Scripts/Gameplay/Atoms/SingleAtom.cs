using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

internal class SingleAtom : Atom
{
    public override void PreSplit(Vector3 d, PlayerBall ball)
    {
        base.PreSplit(d, ball);
        GM.I.gp.PreSplit(this, 0);
    }

}