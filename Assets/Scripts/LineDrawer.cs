using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousPosition;

    // public float minDistance = 0.1f;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, new Vector2(0, 0));
        line.SetPosition(1, new Vector2(1, 1));
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetMouseButton(0))
    //     {
    //         Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         currentPosition.z = 0;
    //         if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
    //         {
    //             if (previousPosition == transform.position)
    //             {
    //                 line.SetPosition(0, currentPosition);
    //             }
    //             else
    //             {
    //                 line.positionCount++;
    //                 line.SetPosition(line.positionCount - 1, currentPosition);
    //             }
    //             previousPosition = currentPosition;
    //         }
    //     }
    // }
}
