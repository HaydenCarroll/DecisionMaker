using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(this.gameObject);
        if (collision.transform.CompareTag("Decision"))
        {
            collision.gameObject.GetComponent<AI>().TakeDamage();

        }
    }
}
