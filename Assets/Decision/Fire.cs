using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public GameObject projectile;
    public GameObject aim;
    public GameObject decision;
    public GameObject rayOrigin;
    RaycastHit aimHit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {





    }

    void AimUp()
    {
        transform.RotateAround(decision.transform.position, Vector3.forward, 2);
    }

    void AimDown()
    {
        transform.RotateAround(decision.transform.position, Vector3.back, 2);
    }
}
