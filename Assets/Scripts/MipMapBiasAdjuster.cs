using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MipMapBiasAdjuster : MonoBehaviour
{
    public float target = -1f;

    public Texture[] texturesToAdjust;

    // Used for the ground texture. Lowers the mip map bias to reduce blur 
    void Start()
    {
        foreach (Texture tex in texturesToAdjust)
        {
            tex.mipMapBias = target;
        }
    }
}
