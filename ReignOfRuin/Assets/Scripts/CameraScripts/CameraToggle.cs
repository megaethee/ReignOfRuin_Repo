using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    private Camera Camera1;
    private Camera Camera2;

    public int CameraManager = 0;
    public int LaneManager = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Start(){
        Camera1 = GameObject.Find ("Main Camera").GetComponent<Camera> ();
        Camera2 = GameObject.Find ("Lanes Camera").GetComponent<Camera> ();
        
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            LaneOn();
        }
        if(Input.GetKeyDown(KeyCode.V)){
            LaneToggle();
        }
    }

    void LaneOn(){
        if (CameraManager == 1){
            Camera1.rect = new Rect(0, 0, .8f, 1);
            Camera2.rect = new Rect(.8f, 0, 1, 1);
            CameraManager = 0;
        }
        else{
            Camera1.rect = new Rect(0, 0, 1, 1);
            Camera2.rect = new Rect(0, 0, 0, 0);
            CameraManager = 1;
        }
    }

    void LaneToggle(){
        if (LaneManager == 1){
            Camera1.rect = new Rect(0, 0, .8f, 1);
            Camera2.rect = new Rect(.8f, 0, 1, 1);
            LaneManager = 0;
        }
        else{
            Camera1.rect = new Rect(.2f, 0, 1, 1);
            Camera2.rect = new Rect(0, 0, .2f, 1);
            LaneManager = 1;
        }
    }
}