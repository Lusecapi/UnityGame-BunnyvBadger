using UnityEngine;
using System.Collections;

public class GrassNPath : MonoBehaviour {

    public bool isLimitPoint = false;
    public Vector3 nextBadgerLookRotation;
    public Vector2 nextBadgerDirection;
    bool rotated = false;

    public static Vector3 rotationLookLeft = new Vector3(0f, 0f, 180f);
    public static Vector3 rotationLookDown = new Vector3(0f, 0f, 270f);
    public static Vector3 rotationLookRight = new Vector3(0f, 0f, 0f);
    public static Vector3 rotationLookUp = new Vector3(0f, 0f, 90f);

    // Use this for initialization
    void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isLimitPoint)
        {
            if (!rotated)
            {
                if (other.tag.Equals("Badger"))
                {
                    other.gameObject.GetComponent<Badger>().lookDirection = nextBadgerLookRotation;
                    other.gameObject.GetComponent<Badger>().moveDirection = nextBadgerDirection;
                    rotated = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isLimitPoint)
        {
            rotated = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
