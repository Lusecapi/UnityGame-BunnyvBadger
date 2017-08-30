using UnityEngine;
using System.Collections;

public class Badger : MonoBehaviour {

    public int badgerLever = 1;
    public int damagePower=5;
    public Vector2 moveDirection;
    public Vector3 lookDirection;
    public int lifePoints = 50;
    [SerializeField]
    float velocityMultiplier = 1f;

    Animator myAnimatorController;
    Rigidbody2D myRigidBody2d;

    public static Vector2 directionMoveLeft= new Vector2(-1f,0f);
    public static Vector2 directionMoveDown = new Vector2(0f, -1f);
    public static Vector2 directionMoveRight = new Vector2(1f, 0f);
    public static Vector2 directionMoveUp = new Vector2(0f, 1f);
    public static Vector2 moveStoped = new Vector2(0f, 0f);

    public static Vector3 rotationLookLeft = new Vector3(0f,0f,0f);
    public static Vector3 rotationLookDown = new Vector3(0f, 0f, 90f);
    public static Vector3 rotationLookRight = new Vector3(0f, 0f, 180f);
    public static Vector3 rotationLookUp = new Vector3(0f, 0f, 270f);

    // Use this for initialization
    void Start() {
        myAnimatorController = GetComponent<Animator>();
        myRigidBody2d = GetComponent<Rigidbody2D>();
        lookDirection = GameObject.Find("StartPoint").transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = lookDirection;
        if(lifePoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    void FixedUpdate()
    {
       
        myRigidBody2d.velocity = moveDirection;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Bullet"))
        {
            Destroy(other.gameObject);
            lifePoints -= other.gameObject.GetComponent<Arrow>().damagePower;
        }
        
    }
}
