using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private Material ignitedMat;
    [SerializeField]
    private MeshRenderer obstacleMesh;

    private bool isIgnited;

    public void IgniteObstacle()
    {
        isIgnited = true;

        // Change material and scale when collides with projectile
        obstacleMesh.material = ignitedMat;
        transform.localScale = new Vector3(0.8F, 0.8F, 0.8F);

        Destroy(gameObject, 0.5F);
    }

    private void FixedUpdate()
    {
        // Change scale if ignited before destroy
        if (isIgnited)
        {
            var newScale = transform.localScale.x + Time.deltaTime;
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}