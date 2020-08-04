using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo1 : MonoBehaviour
{
    public Texture2D texture;
    public SpriteRenderer sprite;
    public Vector3 PosInit;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        texture = sprite.sprite.texture;

        float width = texture.width;
        float height = texture.height;
        Debug.Log(width + "  " + height);
        float offset = Mathf.Abs((Screen.height - height) / 2);

        PosInit = (new Vector3(0, Screen.height) - new Vector3(0, offset));
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Event currentEvent = Event.current;
            Vector2 mousePos = new Vector2();
          
         
            Vector3 screenPos =Input.mousePosition;
            Vector2 pixel = MousePosToPixelImage(screenPos);

            Debug.Log("target is " + pixel.x + " pixels from the left"+pixel.y);
            
            texture.Apply();
        }
    }

    public Vector2 MousePosToPixelImage(Vector3 moupos)
    {
        return new Vector2(moupos.x, Mathf.Abs(PosInit.y - moupos.y));
    }
}
