using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public Animator playerAnimator;
    public PlayerController playerController;
    public PlayerValues playerValues;

    bool gameStarted = false;

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
        if (Input.GetMouseButton(0) && !gameStarted)
        {
            playerAnimator.SetBool("Wait", false);
            playerAnimator.SetBool("Fight", false);
            playerController.Run();

            gameStarted = true;
        }

        if (playerValues.isDead)
        {
            playerController.Stop();
        }
    }
}
