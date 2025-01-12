using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    private float maxtime;
    private float timeleft;
    private float precentleft;
    public Image bar;
    public Color yesTime;
    public Color noTime;



    public void RestartTimer(float time){
        maxtime=time;
        timeleft=maxtime;
        StartCoroutine(timer());
    }

    IEnumerator timer(){
        bool running=true;
        while(running){
            if(timeleft>0){
                timeleft-=Time.deltaTime;
                UpdateBar();
            }
            else{
                running=false;
            }
            
            yield return null;
        }
    }

    void UpdateBar(){
        precentleft=timeleft/maxtime;
        bar.fillAmount=precentleft;

        bar.color=Color.Lerp(noTime, yesTime, Mathf.Clamp01(precentleft));
    }
}
