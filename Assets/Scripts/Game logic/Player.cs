using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSize;
    public float chargingSpeed;

    public Animator playerAnim;

    [SerializeField]
    private GameObject playerBall, pathModel, projectilePref;

    private Projectile curBallProjectile;

    private void Start()
    {
        // Init sizes
        playerBall.transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        pathModel.transform.localScale = new Vector3(playerSize / 5, pathModel.transform.localScale.y, pathModel.transform.localScale.z);
    }

    private void Update()
    {
        if (GameManagerScr.Instance.thisGameResult != GameResult.None) return;

        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            if (curBallProjectile == null) StartBallCharge();
            else OnBallCharging();
        }
        else
        {
            if (curBallProjectile != null && !curBallProjectile.isFired) EndBallCharge();
        }
    }

    private void StartBallCharge()
    {
        var ballOj = Instantiate(projectilePref, transform.position + Vector3.forward * 5, Quaternion.identity);
        curBallProjectile = ballOj.GetComponent<Projectile>();
    }

    private void OnBallCharging()
    {
        if (curBallProjectile == null || curBallProjectile.isFired) return;

        // Player spends own size
        var curPlayerSize = playerBall.transform.localScale.x;
        if (curPlayerSize - chargingSpeed * Time.deltaTime <= 0)
        {
            GameManagerScr.Instance.EndGame(GameResult.Lose);
            return;
        }

        var newPlayerSize = curPlayerSize - chargingSpeed * Time.deltaTime;
        playerBall.transform.localScale = new Vector3(newPlayerSize, newPlayerSize, newPlayerSize);

        // Make path size equivalent to ball size
        pathModel.transform.localScale = new Vector3(newPlayerSize / 5, pathModel.transform.localScale.y, pathModel.transform.localScale.z);

        // Projectile grows

        var newBallSize = curBallProjectile.transform.localScale.x + chargingSpeed * Time.deltaTime;
        curBallProjectile.transform.localScale = new Vector3(newBallSize, newBallSize, newBallSize);
    }

    private void EndBallCharge()
    {
        if (curBallProjectile == null) return;

        curBallProjectile.FireProjectile();
    }

    public void CheckForWin()
    {
        // If there is no obstacle in front of player, show win animation
        if (!Physics.BoxCast(transform.position, 
            new Vector3(pathModel.transform.localScale.x, pathModel.transform.localScale.x, pathModel.transform.localScale.x),
            transform.forward))
        {
            GameManagerScr.Instance.ShowWinAnim();
        }
    }
}