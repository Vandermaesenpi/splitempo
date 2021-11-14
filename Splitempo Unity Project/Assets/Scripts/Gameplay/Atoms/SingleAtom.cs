using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

internal class SingleAtom : Atom
{
    public override void PreSplit(Vector3 d, PlayerBall ball)
    {
        base.PreSplit(d, ball);
        GM.I.gp.PreSplit(this, 0);
        GetComponent<Collider>().enabled = false;
    }
    public override IEnumerator SplitRoutine()
    {
        float startAngle = 180f / (float)splitParts.Count;
        for (int i = 0; i < splitParts.Count; i++)
        {
            Vector3 dir = Quaternion.Euler(0, 0, startAngle + (360f / (float)splitParts.Count) * (float)i) * direction;
            Disposable newAtom = Instantiate(splitParts[i], transform.position, Random.rotation).GetComponent<Disposable>();
            newAtom.Dispose(dir);
        }
        yield return 0;
        GM.I.gp.CurrentLevel.SplitAtom(this);
        Destroy(gameObject);
    }

}