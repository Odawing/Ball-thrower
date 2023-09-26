using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Distance between the obstacles")]
    private float obstaclePerMeter = 5;

    [SerializeField]
    private GameObject obstaclePref;

    private MeshFilter spawnPlane;

    private void Awake()
    {
        spawnPlane = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        // Spawn obstacles

        Mesh planeMesh = spawnPlane.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        // in range of plane mesh
        var rangeX = transform.position.x - transform.localScale.x * bounds.size.x * 0.5f;
        var rangeZ = bounds.size.z;
        for (var i = -rangeZ; i < rangeZ;)
        {
            SpawnObstacle(new Vector3(Random.Range(-rangeX, rangeX), 0, transform.position.z + i));

            i += obstaclePerMeter;
        }
    }

    public void SpawnObstacle(Vector3 pos)
    {
        Instantiate(obstaclePref, pos, Quaternion.identity);
    }
}