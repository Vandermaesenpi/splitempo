using UnityEngine;  
using System.Collections;  
  
public class AudioVisualizer : MonoBehaviour  
{  

    [SerializeField] Vector2 size;
    //An AudioSource object so the music can be played  
    private AudioSource aSource;  
    //A float array that stores the audio samples  
    private float[] samples = new float[128];  
    //A renderer that will draw a line at the screen  
    private LineRenderer lRenderer;  
    //The position of the current cube. Will also be the position of each point of the line.  
    private Vector3 currentPos;  
    //The velocity that the cubes will drop  
    private Vector3 gravity = new Vector3(0.0f,5f,0.0f);
    private Vector3[] points;

    void Awake ()  
    {  
        //Get and store a reference to the following attached components:  
        //AudioSource  
        this.aSource = AudioManager.I._musicSource; 
        //LineRenderer  
        this.lRenderer = GetComponent<LineRenderer>();  
        //Transform  
    }  
  
    void Start()  
    {  
        //The line should have the same number of points as the number of samples  
        lRenderer.positionCount = samples.Length;  
        //The cubesTransform array should be initialized with the same length as the samples array  
        points = new Vector3[samples.Length];  
  
    }  
  
    void Update ()  
    {  
        //Obtain the samples from the frequency bands of the attached AudioSource  
        aSource.GetSpectrumData(this.samples,0,FFTWindow.Triangle);  

        //For each sample  
        for(int i=0; i<samples.Length;i++)  
        {  
            /*Set the cubePos Vector3 to the same value as the position of the corresponding 
             * cube. However, set it's Y element according to the current sample.*/  
            currentPos.Set( i * size.x, Mathf.Min(samples[i] * size.y, 1), 0);
            if(currentPos.y > lRenderer.GetPosition(i).y){
                lRenderer.SetPosition(i, currentPos);  

            }else{
                currentPos = lRenderer.GetPosition(i) - gravity * Time.deltaTime;
                if(currentPos.y <= 0){
                    currentPos.Set( i * size.x, 0, 0);
                }
                lRenderer.SetPosition(i, currentPos);  
            }
        }  
    }  
}  
