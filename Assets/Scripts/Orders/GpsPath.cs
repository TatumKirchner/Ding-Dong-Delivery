using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class GpsPath : MonoBehaviour
{
    public Transform target;
    public LineRenderer lineRenderer;
    private NavMeshPath path;

    //Create a new Path.
    private void Awake()
    {
        path = new NavMeshPath();
    }

    //Get Line renderer, set size and position count.
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 10f;
        lineRenderer.endWidth = 10f;
        lineRenderer.positionCount = 0;
    }

    private void FixedUpdate()
    {
        //If there is a target update the path. Otherwise set the line render count to 0
        if (target != null)
        {
            UpdatePath();
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    private void LateUpdate()
    {
        DrawPath();
    }

    void DrawPath()
    {
        //If there is less than 2 corners return out (No line to be drawn).
        if (path.corners.Length < 2)
        {
            return;
        }

        //Set the line renderer count to the amount of corners from the nav path and set the position of the first to the players position.
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPosition(0, transform.position);

        //Loop through the paths corners and set the line renders position count to the paths corners positions.
        for (int i = 0; i < path.corners.Length; i++)
        {
            Vector3 pointPos = new Vector3(path.corners[i].x, path.corners[i].y + 5, path.corners[i].z);
            lineRenderer.SetPosition(i, pointPos);
        }
    }

    //Calculate a new path from the players new position.
    void UpdatePath()
    {
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
    }
}
