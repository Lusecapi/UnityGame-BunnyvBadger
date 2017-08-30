using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    Camera camera;

	// Use this for initialization
	void Start () {

        camera = Camera.main;
	}

    // Update is called once per frame
    void Update() {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //float z = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButtonDown(0))
        {
            onClick();
        }

        //if (h != 0 || v != 0)
        //{
        //    camera.transform.position = new Vector3(camera.transform.position.x + h * 0.2f, camera.transform.position.y + v * 0.2f, camera.transform.position.z);
        //}

        //if(z != 0)
        //{
        //    camera.orthographicSize += -z;
        //}
        //Debug.Log(Screen.width);


    }

    void onClick()
    {
        Debug.Log("Mouse Down");
        GameObject[] bunnies = GameObject.FindGameObjectsWithTag("Bunny");
        for(int i = 0; i< bunnies.Length; i++)
        {
            bunnies[i].GetComponent<CircleCollider2D>().radius = 0.4f;
        }
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);

        if (!hit.Equals(null))
        {
            Debug.Log(hit.transform.gameObject.ToString());
            if (hit.transform.tag.Equals("Grass"))
            {
                Instantiate(Resources.Load("smallBunny"), hit.transform.position, Quaternion.identity);
            }
        }

        for (int i = 0; i < bunnies.Length; i++)
        {
            bunnies[i].GetComponent<CircleCollider2D>().radius = 2f;
        }
    }
}
