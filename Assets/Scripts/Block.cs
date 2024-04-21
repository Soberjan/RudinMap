using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject Content;

    public List<GameObject> connections;
    public List<string> from;
    public List<string> to;

    public Material lineMaterial;

    private List<GameObject> lineRenderers;

    public Dictionary<string, Vector2> positionsDict;

    void Awake()
    {
        positionsDict = new Dictionary<string, Vector2>()
        {
            {"top", new Vector2(transform.position.x, transform.position.y+0.5f)},
            {"bottom", new Vector2(transform.position.x, transform.position.y-0.5f)},
            {"right", new Vector2(transform.position.x+2.0f, transform.position.y)},
            {"left", new Vector2(transform.position.x-2.0f, transform.position.y)}
        };
    }

    void Start()
    {
        lineRenderers = new List<GameObject>(connections.Count);

        for (int i = 0; i < connections.Count; i++)
        {
            lineRenderers.Add(new GameObject());
            LineRenderer line = lineRenderers[i].AddComponent<LineRenderer>();
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
            line.material = lineMaterial;
            // line.material = new Material(Shader.Find("Standart"));
            line.startColor = Color.black;
            line.endColor = Color.black;

            Dictionary<string, Vector2> destPositionsDict = connections[i].GetComponent<Block>().positionsDict;

            List<Vector2> path = CreatePath(positionsDict[from[i]], destPositionsDict[to[i]], from[i]);

            line.positionCount = path.Count;
            for (int j = 0; j < path.Count; j++)
                line.SetPosition(j, path[j]);
        }
    }

    List<Vector2> CreatePath(Vector2 startPoint, Vector2 endPoint, string startDir)
    {
        List<Vector2> path = new List<Vector2>();

        path.Add(startPoint);
        if (startDir == "right")
            path.Add(new Vector2(startPoint.x+0.5f, startPoint.y));
        else if (startDir == "left")
            path.Add(new Vector2(startPoint.x-0.5f, startPoint.y));
        else if (startDir == "top")
            path.Add(new Vector2(startPoint.x, startPoint.y+0.5f));
        else if (startDir == "bottom")
            path.Add(new Vector2(startPoint.x, startPoint.y-0.5f));
        Vector2 currentPoint = path[1];
        int c = 0;
        while(Vector2.Distance(currentPoint, endPoint) >= 0.6f && c <= 20)
        {
            Vector2 rightPoint = new Vector2(currentPoint.x+1.25f, currentPoint.y);
            Vector2 leftPoint = new Vector2(currentPoint.x-1.25f, currentPoint.y);
            Vector2 upPoint = new Vector2(currentPoint.x, currentPoint.y+1.0f);
            Vector2 downPoint = new Vector2(currentPoint.x, currentPoint.y-1.0f);
            Vector2 newPoint = currentPoint;
            if (!PointAtBlock(rightPoint) && (Vector2.Distance(rightPoint, endPoint) < Vector2.Distance(currentPoint, endPoint) || newPoint == currentPoint))
                currentPoint = rightPoint;
            if (!PointAtBlock(leftPoint) && (Vector2.Distance(leftPoint, endPoint) < Vector2.Distance(currentPoint, endPoint) || newPoint == currentPoint))
                currentPoint = leftPoint;
            if (!PointAtBlock(upPoint) && (Vector2.Distance(upPoint, endPoint) < Vector2.Distance(currentPoint, endPoint) || newPoint == currentPoint))
                currentPoint = upPoint;
            if (!PointAtBlock(downPoint) && (Vector2.Distance(downPoint, endPoint) < Vector2.Distance(currentPoint, endPoint) || newPoint == currentPoint))
                currentPoint = downPoint;
            path.Add(currentPoint);
            c++;
        }
        path.Add(endPoint);
        return path;
    }

    bool PointAtBlock(Vector2 point)
    {
        return Physics2D.OverlapPoint(point) != null;
    }
}
