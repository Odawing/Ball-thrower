using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    [HideInInspector]
    public bool isFired, isHitted;

    public void FireProjectile()
    {
        isFired = true;
        Destroy(gameObject, 4F);
    }

    private void Update()
    {
        if (!isFired || isHitted) return;

        transform.Translate(speed * Time.deltaTime * transform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isFired) return;

        GetComponent<Collider>().enabled = false;

        // Setting pos of the projectile to the collision center
        transform.position += Vector3.forward * transform.localScale.x / 2;

        // Check for near obstacles for ignite
        var nearObstacles = Physics.OverlapSphere(transform.position, transform.localScale.x);
        foreach (var obstacleColl in nearObstacles)
        {
            var obstacle = obstacleColl.GetComponentInParent<Obstacle>();
            if (obstacle)
            {
                isHitted = true;
                obstacle.IgniteObstacle();
            }
        }
        Destroy(gameObject, 0.5F);
    }

    private void OnDestroy()
    {
        if (GameManagerScr.Instance)
            GameManagerScr.Instance.player.CheckForWin();
    }
}