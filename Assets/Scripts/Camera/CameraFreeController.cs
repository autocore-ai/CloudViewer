using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFreeController : MonoBehaviour
{
    public Vector3 defaultRot = new Vector3(0, 75, 0);
    private Camera camera;
    float cameraDistance = 2;
    float elevation = 20;
    public Transform focused;
    private Vector3 oldMousePos;
    private Vector3 newMosuePos;

    [SerializeField]
    private float minimumY = 0.2f;

    [SerializeField]
    private float zoomSpeed = 30.0f;
    [SerializeField]
    private float keyBoardMoveSpeed = 1f;
    [SerializeField]
    private float rotSpeed = 0.05f;
    [SerializeField]
    private float mouseMoveSpeed = 0.05f;//鼠标中键控制相机的速度

    private float distance = 5;
    private Vector3 initPos;
    private Vector3 initRot;
    //private Vector3 centerOffset = Vector3.zero;
    private void Awake()
    {
        camera = GetComponent<Camera>();
        // initPos = transform.position;
        // initRot = transform.eulerAngles;
        // m_camera = GetComponent<Camera>();
    }

    private void Start()
    {
        // Focus();
    }

    private void OnEnable()
    {
        // transform.position = initPos;
        // transform.eulerAngles = initRot;
    }

    void Update()
    {
        // MoveCameraKeyBoard();
        // ZoomCamera();
        // SuperViewMouse();

        // oldMousePos = Input.mousePosition;
    }

    void Focus()
    {
        Vector3 objectSizes = focused.GetComponent<Collider>().bounds.max - focused.GetComponent<Collider>().bounds.min;
        float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * camera.fieldOfView); // Visible height 1 meter in front
        float distance = cameraDistance * objectSize / cameraView; // Combined wanted distance from the object
        distance += 0.5f * objectSize; // Estimated offset from the center to the outside of the object
        camera.transform.position = focused.GetComponent<Collider>().bounds.center - distance * (Quaternion.Euler(defaultRot) * camera.transform.forward);
        camera.transform.LookAt(focused.GetComponent<Collider>().bounds.center);
        // camera.transform.rotation = Quaternion.Euler(new Vector3(elevation, 0, 0));
        // camera.transform.LookAt(focused);
    }

    private void MoveCameraKeyBoard()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))//(Input.GetAxis("Horizontal")<0)
        {
            transform.Translate(new Vector3(-keyBoardMoveSpeed, 0, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(keyBoardMoveSpeed, 0, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 0, keyBoardMoveSpeed), Space.Self);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, 0, -keyBoardMoveSpeed), Space.Self);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(new Vector3(0, keyBoardMoveSpeed, 0), Space.World);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (transform.transform.position.y - keyBoardMoveSpeed >= this.minimumY)
                transform.Translate(new Vector3(0, -keyBoardMoveSpeed, 0), Space.World);
        }
    }

    private void ZoomCamera()
    {
        float offset = Input.GetAxis("Mouse ScrollWheel");
        if (offset != 0)
        {
            offset *= zoomSpeed;
            transform.position = transform.position + transform.forward * offset;//localPosition
            //this.distance -= offset;
            transform.Translate(Vector3.forward * offset, Space.Self); //
        }
    }

    private void SuperViewMouse()
    {
        if (Input.GetMouseButton(1))
        {
            newMosuePos = Input.mousePosition;
            Vector3 dis = newMosuePos - oldMousePos;
            float angleX = dis.x * rotSpeed;//* Time.deltaTime
            float angleY = dis.y * rotSpeed;//* Time.deltaTime
            transform.Rotate(new Vector3(-angleY, 0, 0), Space.Self);
            transform.Rotate(new Vector3(0, angleX, 0), Space.World);
        }
    }
    private Camera m_camera;
}
