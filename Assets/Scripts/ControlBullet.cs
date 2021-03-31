using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBullet : MonoBehaviour
{
    public float BulletSpeed = 20;

    private Rigidbody bulletRigibody;

    void Start()
    {
        bulletRigibody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        bulletRigibody.MovePosition(
            transform.position + transform.forward * 
            BulletSpeed * Time.deltaTime
            );
    }

    void OnTriggerEnter(Collider objCollider)
    {
        if (objCollider.tag == "Inimigo")
            Destroy(objCollider.gameObject);

        Destroy(gameObject);
    }
}
