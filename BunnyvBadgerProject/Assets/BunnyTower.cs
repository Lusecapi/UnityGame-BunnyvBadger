using UnityEngine;
using System.Collections;

public class BunnyTower : MonoBehaviour {

    
    public int lifePoints= 500;

    public static Vector3 rotationLookLeft = new Vector3(0f, 0f, 180f);
    public static Vector3 rotationLookDown = new Vector3(0f, 0f, 270f);
    public static Vector3 rotationLookRight = new Vector3(0f, 0f, 0f);
    public static Vector3 rotationLookUp = new Vector3(0f, 0f, 90f);

    // Use this for initialization
    void Start () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Badger"))
        {
            other.GetComponent<Badger>().moveDirection = Badger.moveStoped;
            StartCoroutine(recieveDamage(other.gameObject));
        }
    }
	
	// Update is called once per frame
	void Update () {
	
        if(lifePoints <= 0)
        {
            lifePoints = 0;
            //GameOver
        }
	}

    //Structure to invoke a method with parameters (Using Coroutine)
    IEnumerator recieveDamage(GameObject badger)
    {
        while (!badger.Equals(null))
        {
            Debug.Log("Daño a la torre");
            lifePoints -= badger.GetComponent<Badger>().damagePower;//Here goes the function we want to repeat
            yield return new WaitForSeconds(1);
        }
        StopAllCoroutines();
    }
}
