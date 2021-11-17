using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public Animator playerAnimator;
    public PlayerController playerController;
    public PlayerValues playerValues;

    public GameObject menuPanel;

    public Image diePanel;
    public Text youAreDeadText;
    public Text youAreDeadTextBack;
    public Text retryText;
    public Text retryTextBack;

    bool gameStarted = false;
    bool canRetry = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator.SetBool("Wait", true);
        playerAnimator.SetBool("Fight", false);
        playerController.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // First step of the game, click to Play
        if (Input.GetMouseButton(0) && !gameStarted)
        {
            menuPanel.SetActive(false);

            playerAnimator.SetBool("Wait", false);
            playerAnimator.SetBool("Fight", false);
            playerController.Run();

            gameStarted = true;
        }

        // launch player's death animation if condition is fullfilled
        if (playerValues.isDead)
        {
            playerValues.Die();
            playerController.Stop();

            StartCoroutine(FadeToMenu());
        }

        // Final step of the game, tap to retry
        if (playerValues.isDead && canRetry && Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }

    // Death Menu animation
    IEnumerator FadeToMenu()
    {
        yield return new WaitForSeconds(1f);

        while(diePanel.color.a < 1f)
        {
            Color c = diePanel.color;
            c.a += Time.deltaTime;
            diePanel.color = c;

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(.5f);

        while (youAreDeadText.color.a < 1f)
        {
            Color c = youAreDeadText.color;
            Color c2 = youAreDeadTextBack.color;
            c.a += Time.deltaTime;
            c2.a += Time.deltaTime;
            youAreDeadText.color = c;
            youAreDeadTextBack.color = c2;

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(.5f);

        while (retryText.color.a < 1f)
        {
            Color c = retryText.color;
            Color c2 = retryTextBack.color;
            c.a += Time.deltaTime;
            c2.a += Time.deltaTime;
            retryText.color = c;
            retryTextBack.color = c2;

            yield return new WaitForSeconds(0.2f);
        }

        canRetry = true;

        yield return null;
    }
}
