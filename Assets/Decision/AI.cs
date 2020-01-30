using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI : MonoBehaviour
{
    public GameObject rayOrigin;
    public GameObject decision;
    public GameObject projectile;
    public GameObject aim;
    public GameObject poof;
    public GameObject leftRayOrigin;
    public GameObject rightRayOrigin;
    public float rayLength=1;
    RaycastHit2D aimHit;
    RaycastHit2D lookHit;
    public float boost=5;
    public float boostLimit = 5;
    public float boopForce = 100;
    Rigidbody2D drb;
    public int hp = 5;
    public Text playerInfo;
    GameObject map;
    
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map");
        drb = decision.GetComponent<Rigidbody2D>();
        playerInfo.text = decision.name + ": " + hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTarget())
        {
            Fire();
        }

        if (boost >= boostLimit)
        {
            Thrust();
        }

        if (boost < boostLimit)
        {
            if (boost < 0)
            {
                boost = 0;
            }
            boost += Random.Range(-1f, 1f);
            boost += Time.deltaTime;
        }
        Steer();
        //UpdateUI();
    }

    public void Steer()
    {

        Vector3 rayPosition = rayOrigin.transform.position;
        Vector3 leftRayPosition = leftRayOrigin.transform.position;
        Vector3 rightRayPosition = rightRayOrigin.transform.position;

        RaycastHit2D rayLeft = Physics2D.Raycast(leftRayPosition, leftRayOrigin.transform.right, rayLength);
        RaycastHit2D rayRight = Physics2D.Raycast(rightRayPosition, rightRayOrigin.transform.right, rayLength);
        RaycastHit2D rayFront = Physics2D.Raycast(rayPosition, rayOrigin.transform.right, rayLength);
        Debug.DrawRay(leftRayPosition, leftRayOrigin.transform.right * rayLength, Color.yellow);
        Debug.DrawRay(rightRayPosition, rightRayOrigin.transform.right * rayLength, Color.yellow);
        Debug.DrawRay(rayPosition, rayOrigin.transform.right * rayLength, Color.yellow);

        if (rayFront.collider != null && rayLeft.collider != null && rayRight.collider != null)
        {
            AimDown();
        }
        if (rayFront.collider != null && rayRight.collider != null)
        {
            if (rayRight.collider.CompareTag("Decision"))
            {
                AimDown();
            }
            else
            {
                AimUp();
            }
        }

        if (rayFront.collider != null && rayLeft.collider != null)
        {
            if (rayLeft.collider.CompareTag("Decision"))
            {
                AimUp();
            }
            else
            {
                AimDown();
            }

        }

        if (rayLeft.collider != null)
        {
            if (rayLeft.collider.CompareTag("Decision"))
            {
                AimUp();
            }
            else
            {
                AimDown();
            }

        }

        if(rayRight.collider != null)
        {
            if (rayRight.collider.CompareTag("Decision"))
            {
                AimDown();
            }
            else
            {
                AimUp();
            }

        }




    }

    public bool IsClicked()
    {
        if (Input.mousePosition.x<decision.transform.position.x-5 && Input.mousePosition.x < decision.transform.position.x+5)
        {
            if (Input.mousePosition.y < decision.transform.position.y - 5 && Input.mousePosition.y < decision.transform.position.y + 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        } 
    }

    public bool IsTarget()
    {
        Vector3 rayPosition = rayOrigin.transform.position;
        Debug.DrawRay(rayPosition, rayOrigin.transform.right * 20, Color.white);
        aimHit = Physics2D.Raycast(rayPosition, rayOrigin.transform.right, 15);
        if(aimHit.collider != null)
        {
            if (aimHit.collider.tag.Equals("Decision"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    public void Scan()
    {
        int maxRays = 200;
        Quaternion q = Quaternion.AngleAxis(5, Vector3.forward);
        Vector3 direction = Vector3.up;
        for (int i = 0; i < maxRays; i++)
        {

            direction = q * direction;
            Debug.DrawRay(decision.transform.position, direction * 5, Color.red);
        }
    }

    void Thrust()
    {
        decision.GetComponent<Rigidbody2D>().AddForce(decision.transform.right * boopForce);
        for (int i=0; i<50; i++)
        {
            float maxR = 0.8f;
            float minR = -maxR;
            Vector3 spawnP = new Vector3(decision.transform.position.x + Random.Range(minR,maxR) , decision.transform.position.y + Random.Range(minR,maxR));
            GameObject foop = Instantiate(poof, spawnP, Quaternion.identity);
            Rigidbody2D f2d = foop.AddComponent<Rigidbody2D>();
            f2d.gravityScale = 0;
            f2d.AddForce(-decision.transform.right * 50);
            f2d.AddTorque(Random.Range(-20f, 20f));
            Destroy(foop, 4);
        }
        boost = 0;
    }

    void AimUp()
    {
        //transform.RotateAround(decision.transform.position, Vector3.forward, 2);
        decision.GetComponent<Rigidbody2D>().AddTorque(0.2f);
    }

    void AimDown()
    {
        //transform.RotateAround(decision.transform.position, Vector3.back, 2);
        decision.GetComponent<Rigidbody2D>().AddTorque(-0.2f);
    }

    void Fire()
    {
        GameObject newProjectile = Instantiate(projectile, projectile.transform.position, Quaternion.identity);
        newProjectile.SetActive(true);
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.AddForce(aim.transform.right * 600);
        decision.GetComponent<Rigidbody2D>().AddForce(-aim.transform.right * 30);
        decision.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-1, 1));
    }

    public void TakeDamage()
    {
        hp--;
        if (hp < 1)
        {
            map.GetComponent<MapSizing>().physicalPlayers.Remove(decision);
            Debug.Log(map.GetComponent<MapSizing>().physicalPlayers.Count);
            playerInfo.text = decision.name + ": " + "OUT";

            Destroy(decision);
        }
        else
        {
            playerInfo.text = decision.name + ": " + hp;
        }


    }

    //public void UpdateUI()
    //{
    //    playerInfo.text = decision.name + ": " + hp;
    //}
}
