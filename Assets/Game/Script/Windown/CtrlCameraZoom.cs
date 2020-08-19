using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlCameraZoom : MonoBehaviour
{
    public float speedScroll = 100;
    public Camera Camera { get; private set; }


    // Start is called before the first frame update
   

    // Update is called once per frame
 
  
    // orthographic sizes
    public static float MaxZoom = 6f;
    public static float StdZoom = 1.44f;
    public static float MinZoom = 0.1f;

    public float[] ZoomBounds = new float[] { 320, 640 };

    public Camera targetCamera;
    public float initialOrthoSize;


   
 

  
    public void Init(CtrlPainting canvas)
    {


        Camera = GetComponent<Camera>();
       

        //   Camera.backgroundColor = Color.white;
        Camera.orthographicSize = canvas.Width * (float)Screen.height / (float)Screen.width * 0.5f;
        ZoomBounds[0] = Camera.orthographicSize*0.3f;
        ZoomBounds[1] = Camera.orthographicSize;
        ResetZoom();


    }

  
 
    public Vector3 InitPos;
    bool press = true;
    // Update is called once per frame

    void LateUpdate()
    {
        if (IsClickUI.IsClick)
        {
            return;
        }
      
        int touch = Input.touchCount;
        switch (touch)
        {



            case 2: // Zooming
             
                Debug.Log("Update Camera");
                StartPichZoom();
             
          
                break;

            default:

                isZoomCamera = false;


                break;
        }


      
    }
   
    void zoom(float increment)
    {
      
        if (increment == 0)
        {
            return;
        }



        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, ZoomBounds[0], ZoomBounds[1]);
    }
  
    public void MoveCamera(Vector3 pos)
    {

        Vector3 position = pos;

       position.x = Mathf.Clamp(position.x, BoundsX[0], BoundsX[1]);

       position.y = Mathf.Clamp(position.y, BoundsY[0], BoundsY[1]);

       transform.position = Vector3.MoveTowards(transform.position, new Vector3(position.x,position.y , -100), Time.deltaTime *500);
       // transform.position = position;
    }

    public void setBoundXZCamera(float sizeCam)
    {
        BoundsX = new float[2] { -sizeCam * cache, sizeCam * cache };
        BoundsY = new float[2] { -100-(sizeCam * cacheY),-100+(sizeCam * cacheY) };
      
    }

    public void StartPichZoom()
    {
        if (!isZoomCamera)
        {



            Touch1 = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
            Touch2 = Camera.main.ScreenToViewportPoint(Input.GetTouch(1).position);
            InitPos = new Vector3((Input.GetTouch(0).position.x + Input.GetTouch(1).position.x)/ 2, (Input.GetTouch(0).position.y + Input.GetTouch(1).position.y) / 2,-100);
            InitPos = Camera.main.ScreenToWorldPoint(InitPos);
            InitPos.z = -100;

            newdistance = Vector3.Distance(Touch1, Touch2);
            lastdistance = newdistance;
            isZoomCamera = true;

        }
        else
        {
            UpdatePinchZoom();
            setBoundXZCamera(ZoomBounds[1] - Camera.main.orthographicSize);
            MoveCamera(InitPos);
        }

    }

    public void UpdatePinchZoom()
    {
     
      
       
        Touch1 = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
        Touch2 = Camera.main.ScreenToViewportPoint(Input.GetTouch(1).position);
      

          newdistance = Vector3.Distance(Touch1, Touch2);
    
            float offset = newdistance - lastdistance;
            Debug.Log("Zoom");




            zoom(offset * speedScroll);


            lastdistance = newdistance;

        





    }


    public float[] BoundsX;
    public float[] BoundsY;
    public float cache;
    public float cacheY;
    //

    public Vector3 posZoom;
    public Vector3 Touch1;
    public Vector3 Touch2;

    public float lastdistance;
    public float newdistance;
    public bool isZoomCamera;


    public void ResetZoom()
    {
        transform.position = new Vector3(0, -100, -100);
        Camera.main.orthographicSize = ZoomBounds[1];
    }
}
