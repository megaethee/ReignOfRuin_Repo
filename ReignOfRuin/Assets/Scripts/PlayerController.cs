using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _Instance { get; private set;}
    public float speed;
    private Vector2 move;
    public int CoinCounter = 0;
    public GameObject Arrow;
    public Vector3 movement;
    public bool canMove;

    [SerializeField]
    private Rigidbody rB;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Awake(){
        if(null == _Instance){
            _Instance = this;
        }
        else {
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
