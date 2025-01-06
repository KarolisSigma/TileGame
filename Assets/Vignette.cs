using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Vignette : MonoBehaviour
{
    public Volume postProcessVolume;
    private Vignette vignette;
    private bool vignetteset = false;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {


        if (!vignetteset)
        {
            postProcessVolume.profile.TryGet(out vignette);
            vignetteset = true;
        }

        if (hit.gameObject.layer == 3)
        {
            if (hit.gameObject.tag == "red")
            {
                vignette.color.value = Color.red;
            }
            else
            {
                vignette.color.value = Color.green;
            }

        }
    }
}
