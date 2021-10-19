using System.Collections.Generic;
using UnityEngine;

public class PositionsLoop : MonoBehaviour
{
    public List<Vector2> points;
    public int GetNextPosition(int i){
        if(i == points.Count -1){
            return 0;
        }
        return i+1;
    }

    public Vector3 GetWorldPositionAt(int i){
        return transform.TransformPoint(points[i]);
    }

    public void SetWorldPositionAt(Vector3 position, int i){
        points[i] = transform.InverseTransformPoint(position);
    }
}