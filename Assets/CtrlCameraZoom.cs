using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlCameraZoom : MonoBehaviour
{

    public Camera Camera { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(CtrlPainting canvas)
    {
         Camera = GetComponent<Camera>();


        //   Camera.backgroundColor = Color.white;
      Camera.orthographicSize = canvas.Width * (float)Screen.height / (float)Screen.width * 0.5f;
      
        // Camera.orthographicSize = (float)canvas.Height / 2.0f;
        //  Camera.aspect = (float)canvas.Width / (float)canvas.Height;


        // //  Camera.clearFlags = CameraClearFlags.Nothing;

        // //  Camera.targetTexture = canvasCtrl.BackLayerController.RenderTexture;

    }
}
