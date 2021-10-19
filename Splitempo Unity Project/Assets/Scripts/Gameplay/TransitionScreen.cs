using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private AnimationCurve _enterCurve, _exitCurve;
    [SerializeField] private float _xOffset;
    [SerializeField] private Text transitionText, scoreText, highscoreText;
    [SerializeField] private Image logo;
    [SerializeField] private Sprite logoGood, logoBad;

    private void Awake() {
        _transform = transform;
    }

    public IEnumerator StartTransition(){

        Vector3 startPosition = new Vector3(_xOffset, 0, 0);
        float t = 0;
        float waitTime = BeatManager.BeatToSeconds(BeatManager.TimeUntilBeatInPhrase(0));
        while(t < waitTime){
            _transform.position = Vector3.Lerp(startPosition, Vector3.zero, _enterCurve.Evaluate(t/waitTime));
            t += Time.deltaTime;
            yield return 0;
        }
        _transform.position = Vector3.zero;
    }

    public IEnumerator ShowRebootUI(){
        
        transitionText.text = "REBOOT";
        logo.sprite = logoBad;
        transitionText.color = Color.clear;
        logo.color = Color.clear;
        
        yield return StartCoroutine(UIFadeTo(Color.red));

        yield return StartCoroutine(BeatManager.WaitForBeatDuration(1));
        transitionText.text = "REBOOT.";
        yield return StartCoroutine(BeatManager.WaitForBeatDuration(1));
        transitionText.text = "REBOOT..";
        yield return StartCoroutine(BeatManager.WaitForBeatDuration(1));
        transitionText.text = "REBOOT...";
        yield return StartCoroutine(BeatManager.WaitForBeatDuration(1));

        yield return StartCoroutine(UIFadeTo(Color.clear));

    }


    public IEnumerator ShowNextLevelUI(){
        transitionText.text = "CORE STABILIZED";
        transitionText.color = Color.clear;
        logo.color = Color.clear;
        logo.sprite = logoGood;

        StartCoroutine(UIFadeTo(Color.green));

        scoreText.text = "Shots : " + GM.I.gp.shotsTaken;
        yield return StartCoroutine(BeatManager.WaitForBeatDuration(3));
        highscoreText.text = "Pierre's highscore : " + GM.I.gp.CurrentLevel.devHighscore;
        yield return StartCoroutine(BeatManager.WaitForBeatDuration(4));
        scoreText.text = "";
        highscoreText.text = "";

        yield return StartCoroutine(UIFadeTo(Color.clear));
    }

    public IEnumerator EndTransition(){
        Vector3 targetPosition = new Vector3(_xOffset, 0, 0);
        float t = 0;
        float waitTime = BeatManager.BeatToSeconds(BeatManager.TimeUntilBeatInPhrase(0));
        while(t < waitTime){
            _transform.position = Vector3.Lerp(targetPosition, Vector3.zero, _exitCurve.Evaluate(t/waitTime));
            t += Time.deltaTime;
            yield return 0;
        }
        _transform.position = targetPosition;
    }

    IEnumerator UIFadeTo(Color color)
    {
        float t = 0;
        float waitTime = BeatManager.BeatToSeconds(2);
        while(t < waitTime){
            transitionText.color = Color.Lerp(transitionText.color, color, _enterCurve.Evaluate(t/waitTime));
            logo.color = Color.Lerp(transitionText.color, color, _enterCurve.Evaluate(t/waitTime));
            t += Time.deltaTime;
            yield return 0;
        }
        transitionText.color = color;
        logo.color = color;
    }
}