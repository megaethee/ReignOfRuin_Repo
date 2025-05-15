using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    public static MenuManager _Instance { get; private set; }
    
    public bool pause;
    public GameObject menu;
    // Update is called once per frame
    void Awake()
    {
        if (null == _Instance)
            _Instance = this;
        else
            Destroy(gameObject);

        menu.SetActive(false);
        pause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            menu.SetActive(true);
    }
}
