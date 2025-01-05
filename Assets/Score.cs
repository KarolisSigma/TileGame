using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    public int multiplier;
    private float timePlayed;

    public TextMeshProUGUI scoretxt;
    public TextMeshProUGUI multipliertxt;

    public void AddScore() {
        multiplier++;
        timePlayed = Time.timeSinceLevelLoad;
        score += Convert.ToInt32(timePlayed / 2f) * multiplier;
        UpdateUi();
    }

    void UpdateUi() {
        scoretxt.text = score.ToString();
        multipliertxt.text = multiplier.ToString() + "x";
        if (multiplier <= 1)
        {
            multipliertxt.color = Color.white;
        }
        else {
            multipliertxt.color = UnityEngine.Random.ColorHSV(0, 1, 0.6f, 1, 1, 1);
        }
    }

    public void ResetMultiplier() { 
        multiplier = 1;
        UpdateUi();
    }

    void Start()
    {
        multiplier = 1;
        UpdateUi();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
