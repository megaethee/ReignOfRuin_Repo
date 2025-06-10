using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _Instance { get; private set;}
    public float speed;
    private Vector2 move;
    private bool isPaused = false;
    public int CoinCounter = 0;
    public GameObject Arrow;
    public Vector3 movement;
    public bool canMove;
    [SerializeField]
    private UIManager uiManager;


    [SerializeField]
    private Rigidbody rB;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) // Only trigger on button press, not hold/release
        {
            isPaused = !isPaused; // Toggle pause state

            if (isPaused)
            {
                Debug.Log("Game Paused");
                uiManager.HandleUISwitch("PauseMenu");
            }
            else
            {
                Debug.Log("Game Resumed");
                uiManager.HandleUISwitch("GameUI");
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        
    }
    private void Awake(){
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        if (null == _Instance)
        {
            _Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        canMove = true;
        
        rB = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
            movePlayer();        
    }

    private IEnumerator MoveBack()
    {
        transform.Translate(-movement * speed * Time.deltaTime, Space.World);
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    public void movePlayer()
    {
        movement = new Vector3(move.x, 0f, move.y);

        if (movement == Vector3.zero)
            GetComponent<Animator>().SetBool("isMoving", false);
        else
            GetComponent<Animator>().SetBool("isMoving", true);

        if (movement != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f); 
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        
        //rB.MovePosition(transform.position + movement * Time.deltaTime * speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            CoinCounter++;
        }

        if (other.tag == "Building")
        {
            Debug.Log("STOOOOP");
            StartCoroutine(MoveBack()); 
            canMove = false; 
        }
        
    }
}
