using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class SecondShaderScriptCAM : MonoBehaviour
{

    public Material mat;

    //Source is the fully rendered scene that gets sent to the monitor
    //now we are intercepting between this step to we can do work on it
    //before we sent it / pass it onto the screen for whoever to see.
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        Graphics.Blit(source, destination, mat);
    }
}
