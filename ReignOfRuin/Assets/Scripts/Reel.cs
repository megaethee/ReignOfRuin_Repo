using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{

    public bool Spin;

    int speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spin = false;
        speed = 1500;
    }

    // Update is called once per frame
    void Update()
    {
        if(Spin){
            foreach(Transform image in transform){
                image.transform.Translate(Vector3.down * Time.smoothDeltaTime * speed, Space.World);

                if(image.transform.position.y <= 0){
                    image.transform.position = new Vector3(image.transform.position.x, image.transform.position.y + 600, image.transform.position.z);
                }
            }
        }
    }

    public void RandomPosition(){
        List<int> parts = new List<int>();

        parts.Add(200);
        parts.Add(100);
        parts.Add(0);
        parts.Add(-100);
        parts.Add(-200);
        parts.Add(-300);

        foreach (Transform image in transform){
            int rand = Random.Range(0, parts.Count);

            image.transform.position = new Vector3(image.transform.position.x, parts[rand] + transform.parent.GetComponent<RectTransform>().transform.position.y, image.transform.position.z);

            parts.RemoveAt(rand);
        }
    }
}
