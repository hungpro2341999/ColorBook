using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour
{

    private static readonly float PanSpeed = 20f;
    private static readonly float ZoomSpeedTouch = 10f;
    private static readonly float ZoomSpeedMouse = 50f;

    public float[] BoundsX = new float[] {0,0};
    public float[] BoundsZ = new float[] {0,0};
    public float[] ZoomBounds = new float[] { 320, 640 };
    public float cache;
    public Vector2 posCenter = Vector2.zero;
   // public int touch = 0;
  
    
    public static bool isZoom;
    public void setBoundX()
    {

    }

    public Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId; // Touch mode only

    private bool wasZoomingLastFrame; // Touch mode only
    private Vector2[] lastZoomPositions; // Touch mode only

    void Awake()
    {
      
    }

    void Update()
    {

      

   
      

        HandleTouch();
    }
    
    public void SetUpCamera(float[] BoundView)
    {
     
        BoundsX = new float[] { 0, 0 };
        BoundsZ = new float[] { 0, 0 };
        transform.position = Vector3.zero;

    }

    public void setBoundXZCamera(float sizeCam)
    {
        BoundsX = new float[2] { -sizeCam * cache, sizeCam * cache };
        BoundsZ = new float[2] { -sizeCam * 0.09f, sizeCam * 0.09f };
    }
    void HandleTouch()
    {



        // setBoundXZCamera(Bound);

        if (Input.GetMouseButtonDown(0))
        {
            int touch = Input.touchCount;
            Debug.Log(touch);
            switch (touch)
            {



                case 1: // Zooming
                    Debug.Log("Update Camera");
                    StartPichZoom();
                    isZoom = true;
                    break;

                default:
                    isZoom = false;
                    BoundsX = new float[2] { 0, 0 };
                    BoundsZ = new float[2] { 0, 0 };
                    isZoomCamera = false;
                    // Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, fieldOfView, Time.deltaTime * 50);
                    //  transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * 50);
                    break;
            }
        }

       
    }
    

   

    public void EndPinchZoom(float distance)
    {

    }
    public void StartPichZoom()
    {
        if (!isZoomCamera)
        {
            //Vector3 Mouse = Input.GetTouch(0).position;
            //Mouse.z = 10;
            //Vector3 Pos1 = Camera.main.ScreenToWorldPoint(Mouse);

            //Vector3 Mouse1 = Input.GetTouch(1).position;
            //Mouse1.z = 10;
            //Vector3 Pos2 = Camera.main.ScreenToWorldPoint(Mouse1);

            //posZoom = new Vector3((Pos1.x + Pos2.x) / 2, (Pos1.y + Pos2.y) / 2, (Pos1.z + Pos2.z) / 2);



            Touch1 = cam.ScreenToViewportPoint(Input.GetTouch(0).position);
            Touch2 = cam.ScreenToViewportPoint(Input.GetTouch(0).position)*1.5f;
            this.posCenter = (Vector2)((Touch1 + Touch2) / 2);
            newdistance = Vector3.Distance(Touch1, Touch2);
            lastdistance = newdistance;
            isZoomCamera = true;
           
        }
        else
        {
            UpdatePinchZoom();
        }
     
    }


    public Vector3 posZoom;
    public Vector3 Touch1;
    public Vector3 Touch2;
    
    public float lastdistance;
    public float newdistance;
    public bool isZoomCamera;


    public void UpdatePinchZoom()
    {
        MoveCamera(posCenter);
        Touch1 = cam.ScreenToViewportPoint(Input.GetTouch(0).position);
        Touch2 = cam.ScreenToViewportPoint(Input.GetTouch(1).position);

        //      posZoom = new Vector3((Touch1.x + Touch2.x) / 2, (Touch1.y + Touch2.y) / 2, (Touch1.z + Touch2.z) / 2);
        newdistance = Vector3.Distance(Touch1, Touch2);
     
         
                float offset = newdistance - lastdistance;

              



                ZoomCamera(offset, 35);

         
            lastdistance = newdistance;

       
      

    }

    public void ResetPinchZoom()
    {
       
    }
    public void MoveCamera(Vector3 pos)
    {

        Vector3 position = pos;
       
       position.x = Mathf.Clamp(position.x, BoundsX[0], BoundsX[1]);
    
        position.z = Mathf.Clamp(position.z, BoundsZ[0], BoundsZ[1]);

        transform.position =  Vector3.MoveTowards(transform.position,new Vector3(position.x, transform.position.y, position.z),Time.deltaTime*5);
    }

    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
        {
            return;
        }
      
        
        
        cam.orthographicSize = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }

    

  
}