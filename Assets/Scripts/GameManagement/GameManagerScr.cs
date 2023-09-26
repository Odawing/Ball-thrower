using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScr : MonoBehaviour
{
    public static GameManagerScr Instance;

    public Player player;

    public Animator doorAnim;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public GameResult thisGameResult;

    public void EndGame(GameResult result)
    {
        thisGameResult = result;

        StartCoroutine(EndGameCour());
    }

    private IEnumerator EndGameCour()
    {
        yield return new WaitForSeconds(2F);

        SceneManager.LoadScene(0);
    }

    public void ShowWinAnim()
    {
        EndGame(GameResult.Win);

        doorAnim.Play("Open");
        player.playerAnim.Play("Win");
    }
}

public enum GameResult
{
    None,
    Win,
    Lose
}