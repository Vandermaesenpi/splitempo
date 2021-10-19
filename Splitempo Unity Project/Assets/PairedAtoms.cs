using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairedAtoms : BeatAnimator
{
    private LineRenderer lineRenderer;
    [SerializeField] private List<Atom> atoms;
    [SerializeField] private float segmentLength;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        GenerateLinePoints(0);
    }

    private void GenerateLinePoints(float value)
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(atoms[atoms.Count-1].transform.position);
        for (int i = 1; i < atoms.Count; i++)
        {
            Vector3 lineSegment = atoms[i-1].transform.position - atoms[i].transform.position;
            float distance = lineSegment.magnitude;
            for (int j = 0; j < distance/segmentLength; j++)
            {
                Vector3 position = atoms[i].transform.position + lineSegment.normalized * ((distance%segmentLength)/2f + segmentLength * j);
                float rotation = j%2 == 1 ? 90f : -90f;
                Vector3 displacement = Quaternion.Euler(0,0,rotation) * lineSegment.normalized * curve.Evaluate(value) * amount;
                position += displacement;
                positions.Add(position);
            }
        }
        positions.Add(atoms[0].transform.position);
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }

    public override void SetupAnimation()
    {
        GenerateLinePoints(0);
        base.SetupAnimation();
    }

    public override void ExecuteAnimation(float _time)
    {
        GenerateLinePoints(_time);
        Debug.Log(_time);
        base.ExecuteAnimation(_time);
    }

    public override void EndAnimation()
    {
        GenerateLinePoints(1);
        base.EndAnimation();
    }
}
