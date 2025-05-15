using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _Instance { get; private set;}
    public float speed;
    private Vector2 move;
    public int CoinCounter = 0;

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

        rB = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);

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
        
    }
}
