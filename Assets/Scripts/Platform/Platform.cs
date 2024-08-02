using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private TargetJoint2D target;
    private BoxCollider2D boxColl;
    public Transform pivot;

    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
        transform.position = pivot.position;
    }

    void PlatformDown()
    {
        target.enabled = false;
        boxColl.isTrigger = true;
    }

    void InstantiatePlatform()
    {
        target.enabled = true;
        boxColl.isTrigger = false;
        Instantiate(pivot, pivot.position, Quaternion.identity);
        Destroy(pivot.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("PlatformDown", 0.8f);
            Invoke("InstantiatePlatform", 3.5f);
        }
    }
}
