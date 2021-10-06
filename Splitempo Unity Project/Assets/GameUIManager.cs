using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Image stabilityFiller, stabilityBkg;
    public Text shots, stability;
    // Update is called once per frame
    void Update()
    {
        stabilityFiller.color = GM.I.gameplay.stability > (GM.I.gameplay.maxStability/2) ? Color.white : Color.red;
        stabilityBkg.color = GM.I.gameplay.stability > (GM.I.gameplay.maxStability/4) ? Color.white : Color.red;
        stability.color = GM.I.gameplay.stability > (GM.I.gameplay.maxStability/2) ? Color.white : Color.red;
        stabilityFiller.fillAmount = (float)GM.I.gameplay.stability / GM.I.gameplay.maxStability;
        shots.text = ""+GM.I.gameplay.shotsTaken;
        stability.text = ""+GM.I.gameplay.stability;
        
    }
}
