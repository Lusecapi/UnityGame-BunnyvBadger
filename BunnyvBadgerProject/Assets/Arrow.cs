using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    public float speed;
    public int damagePower;
    public Vector3 target;
    public Vector3 rotation;
    
    
    
    // Use this for initialization
	void Start () {
        transform.eulerAngles = rotation;
        Destroy(gameObject, 0.5f);
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.Lerp(transform.position, target, speed);
    }
}
