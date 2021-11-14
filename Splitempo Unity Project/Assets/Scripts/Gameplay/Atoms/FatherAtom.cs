using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FatherAtom : Atom
{
    public override void PreSplit(Vector3 d, PlayerBall ball)
    {
        if(colored && ball.blue != blue){return;}
        base.PreSplit(d, ball);
        GM.I.gp.PreSplit(this, splitParts.Count);
    }

    public override IEnumerator SplitRoutine()
    {
        float startAngle = 180f / (float)splitParts.Count;
        List<Atom> children = new List<Atom>();
        for (int i = 0; i < splitParts.Count; i++)
        {

            Vector3 dir = GetDirection(startAngle, i);
            Atom newAtom = Instantiate(splitParts[i], transform.position, Random.rotation).GetComponent<Atom>();
            newAtom.transform.parent = transform.parent;
            newAtom.Spawn(dir);
            children.Add(newAtom);
        }
        GM.I.gp.CurrentLevel.AddNewWaitingAtoms(children);
        yield return 0;
        GM.I.gp.CurrentLevel.SplitAtom(this,children.Count);
        Destroy(gameObject);
    }
}