using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject selection;
    public GameObject BlockContent;
    public GameObject Cam;

    private GameObject Content;

    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;

    private bool drag = false;
    private bool contentOpened = false;

    void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.OverlapPoint(mousePos);
        if (col != null && col.tag == "Block" && !contentOpened)
        {
            selection.SetActive(true);
            selection.transform.position = col.transform.position;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                BlockContent.SetActive(true);
                BlockContent.transform.position = new Vector2(Cam.transform.position.x, Cam.transform.position.y);
                Content = col.gameObject.GetComponent<Block>().Content;
                Content.SetActive(true);
                Content.transform.position = BlockContent.transform.position; // не забудь убрать потом
                contentOpened = true;
            }
        }
        else if (col != null && col.tag == "Close")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Content.SetActive(false);
                BlockContent.SetActive(false);
                contentOpened = false;
            }
        }
        else if (selection.activeSelf)
            selection.SetActive(false);
    }

    private void LateUpdate()
    {
        if (!contentOpened)
        {
            if (Input.GetMouseButton(1))
            {
                Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if(drag == false)
                {
                    drag = true;
                    Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }

            }
            else
            {
                drag = false;
            }

            if (drag)
            {
                Camera.main.transform.position = Origin - Difference * 0.5f;
            }

            if (Input.GetMouseButton(2) || Input.GetKeyDown(KeyCode.R))
                Camera.main.transform.position = ResetCamera;
        }
    }

}
