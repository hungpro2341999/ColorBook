using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlCameraZoom : MonoBehaviour
{

    public Camera Camera { get; private set; }


    // Start is called before the first frame update
   

    // Update is called once per frame
 
  
    // orthographic sizes
    public static float MaxZoom = 6f;
    public static float StdZoom = 1.44f;
    public static float MinZoom = 0.1f;


   
    public Camera targetCamera;
    public float initialOrthoSize;


   
    public void ChangeCamera(Camera newTargetCamera)
    {
        float newInitialSize = newTargetCamera.orthographicSize;
        newTargetCamera.orthographicSize = targetCamera.orthographicSize;
        targetCamera.orthographicSize = initialOrthoSize;
        initialOrthoSize = newInitialSize;
    }

  
    public void Init(CtrlPainting canvas)
    {


        Camera = GetComponent<Camera>();


        //   Camera.backgroundColor = Color.white;
        Camera.orthographicSize = canvas.Width * (float)Screen.height / (float)Screen.width * 0.5f;
        zoomOutMax = Camera.orthographicSize;
        zoomOutMin = Camera.orthographicSize / 2;



    }

    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }


}
