using UnityEngine;
using UnityEditor;
// A tiny custom editor for ExampleScript component
[CustomEditor(typeof(PositionsLoop))]
public class PositionsLoopEditor : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    public void OnSceneGUI()
    {
        PositionsLoop positionLoop = target as PositionsLoop;
        Vector3[] worldPoints = new Vector3[positionLoop.points.Count+1];
        for (int i = 0; i < positionLoop.points.Count; i++)
        {
            Vector3 newWorldPosition = Handles.DoPositionHandle(positionLoop.GetWorldPositionAt(i), Quaternion.identity);
            Handles.Label(newWorldPosition, i.ToString());
            worldPoints[i] = newWorldPosition;
            positionLoop.SetWorldPositionAt(newWorldPosition, i);
        }
        worldPoints[positionLoop.points.Count] = worldPoints[0];
        Handles.DrawPolyLine(worldPoints);
    }
}