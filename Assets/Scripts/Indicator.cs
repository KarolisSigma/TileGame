using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Indicator : MonoBehaviour
{

    public Volume postProcessVolume;
    private Vignette vignette;

    private string lastTileName="";
    private string currentTileName;
    private string lastSetColor;
    public bool checkagain;

    void Awake(){
        postProcessVolume.profile.TryGet(out vignette);
    }


    void OnTriggerStay(Collider collider){
        currentTileName = collider.name;
        
        if(lastTileName!=currentTileName || checkagain){
            lastTileName=currentTileName;
            checkagain=false;
            if (collider.gameObject.layer == 3)
            {
                if (collider.gameObject.tag == "red")
                {
                    if(lastSetColor!="red")
                    {
                        vignette.color.value = Color.red;
                        lastSetColor="red";
                    }
                }
                else
                {
                    if(lastSetColor!="green")
                    {
                        vignette.color.value = Color.green;
                        lastSetColor="green";
                    }
                }

            }
        }



    }
}
