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
        stabilityFiller.color = GM.I.gp.stability > (GM.I.gp.maxStability/2) ? Color.white : Color.red;
        stabilityBkg.color = GM.I.gp.stability > (GM.I.gp.maxStability/4) ? Color.white : Color.red;
        stability.color = GM.I.gp.stability > (GM.I.gp.maxStability/2) ? Color.white : Color.red;
        stabilityFiller.fillAmount = (float)GM.I.gp.stability / GM.I.gp.maxStability;
        shots.text = ""+GM.I.gp.shotsTaken;
        stability.text = ""+GM.I.gp.stability;
        
    }
}
