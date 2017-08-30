using UnityEngine;
using System.Collections;

public class Bunny : MonoBehaviour
{

    public float timeToShoot = 0.2f;
    public float shootSpeed = 0.2f;//[0, 1]
    public int shootDamage = 2;
    [SerializeField]
    GameObject targetObject;
    [SerializeField]
    bool isShooting = false;
    [SerializeField]
    bool hasTarget = false;
    Vector3 targetPoint;
    [SerializeField]
    LayerMask whatIsBadger;
    Transform badgerCheck;
    public float badgerCheckZoneRadius = 2;

    //private enum TagMask
    //{
    //    Bullet,
    //    Badger,
    //    Grass,
    //    PathNGrass,
    //}

    // Use this for initialization
    void Start()
    {
        badgerCheck = transform.FindChild("BadgerCheck");

    }

    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTarget)
        {
            if (other.tag.Equals("Badger"))
            {
                hasTarget = true;
                targetObject = other.gameObject;
                targetPoint = targetObject.transform.position;
                transform.eulerAngles = new Vector3(0f, 0f, getAngle(targetPoint));
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (hasTarget)
        {
            if (other.gameObject.Equals(targetObject))
            {
                targetPoint = targetObject.transform.position;
                transform.eulerAngles = new Vector3(0f, 0f, getAngle(targetPoint));
                if (!isShooting)
                {
                    InvokeRepeating("shoot", 0, 0.2f);
                }
            }
        }
        else
        {
            if (other.tag.Equals("Badger"))
            {
                hasTarget = true;
                targetObject = other.gameObject;
                targetPoint = targetObject.transform.position;
                transform.eulerAngles = new Vector3(0f, 0f, getAngle(targetPoint));
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(targetObject))
        {
            hasTarget = false;
            isShooting = false;
            targetObject = null;
            CancelInvoke();
        }
    }

    bool targetIsDead(GameObject badger)
    {
        if (targetObject == null)
        {
            return true;
        }
        else
        {
            Badger b = badger.GetComponent<Badger>();
            if (b.lifePoints < 0)
            {
                return true;
            }
            return false;
        }
    }

    ////In Start() or wherever
    //StartCoroutine(Foo(p1, p2));

    //function Foo(param1, param2)                  //Coroutine structure
    //    {
    //        yield WaitForSeconds(timer);
    //        //Do stuff
    //    }


    float getAngle(Vector3 targetPoint)//Conversion of the target poit to polar cordinates to get the anlge
    {
        //float r = Mathf.Sqrt(Mathf.Pow(targetPoint.x, 2) + Mathf.Pow(targetPoint.y, 2));
        return toDegrees(Mathf.Atan2(targetPoint.y - transform.position.y, targetPoint.x - transform.position.x));//angle= arctan(y/x)
    }

    float toDegrees(float angleInRad)
    {
        return (180 * angleInRad) / Mathf.PI;
    }

    void shoot(Vector3 targetPoint)
    {
        GameObject arrow = Resources.Load("smallArrow") as GameObject;
        arrow.GetComponent<Arrow>().target = targetPoint;
        arrow.GetComponent<Arrow>().rotation = new Vector3(0f, 0f, getAngle(targetPoint));
        Instantiate(arrow, transform.position, Quaternion.identity);
    }

    void shoot()
    {
        if (!targetIsDead(targetObject))
        {
            isShooting = true;
            GameObject arrow = Resources.Load("smallArrow") as GameObject;
            arrow.GetComponent<Arrow>().target = targetPoint;
            arrow.GetComponent<Arrow>().rotation = new Vector3(0f, 0f, getAngle(targetPoint));
            arrow.GetComponent<Arrow>().speed = shootSpeed;
            arrow.GetComponent<Arrow>().damagePower = shootDamage;
            Instantiate(arrow, transform.position, Quaternion.identity);
        }
        else
        {
            hasTarget = false;
            isShooting = false;
            targetObject = null;
            CancelInvoke();
        }
    }
}
