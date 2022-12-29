using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Camera viewCam;
    RenderTexture rt;
    int pixelScale = 256;
    // Start is called before the first frame update
    void Start()
    {

        Vector2 totalSize = sprite.transform.localScale * sprite.size * pixelScale;
        print(totalSize);
        //Mathf.Ceil //Need the RT (camera) to be bigger than the sprite
        rt = new RenderTexture((int)totalSize.x, (int)totalSize.y, 8);
        //if (!rt.Create()) print("couldnt create rt"); //doesnt need to be called

        viewCam.targetTexture = rt;
        //viewCam.orthographicSize = 0.5; // THis is very important if you want the camera size to reflect exactly 1 unity length

        //Instead of modifying rendertexture size, modify the orthographic size and enxure the render texture aspect ratio matextes that of the the sprite size

        //Ensure the string is same as teh reference
        sprite.material.SetTexture("_RenderTexture", rt);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
