using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHandler : MonoBehaviour
{
    public PlayerValues playerValues;

    [HideInInspector]
    public int maxLife;
    public int life = 10;

    public Image lifeBar;

    public GameObject model1;
    public GameObject model2;
    public GameObject model3;

    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFight()
    {
        print("coucou");
        playerValues.playerController.Stop();
        playerValues.animator.SetBool("Wait", true);

        StartCoroutine(MoveCameraToFight());
    }

    public void PreEndFight()
    {
        playerValues.animator.SetBool("Wait", true);
        playerValues.animator.SetBool("Fight", false);
    }

    public void EndFight()
    {
        //explosion here

        model1.SetActive(false);
        model2.SetActive(false);
        model3.SetActive(false);

        StartCoroutine(MoveCameraToRun());
    }

    IEnumerator MoveCameraToFight()
    {
        Vector3 fightPos = playerValues.posCamFight.transform.position;
        Vector3 camPos = playerValues.playerCamera.transform.position;
        Quaternion camRot = playerValues.playerCamera.transform.rotation;
        Quaternion fightRot = Quaternion.LookRotation(playerValues.posCamFight.transform.right);

        for(float f = 0f; f < 1f; f += Time.deltaTime * playerValues.cameraSpeed)
        {
            playerValues.playerCamera.transform.position = Vector3.Lerp(camPos, fightPos, f);
            playerValues.playerCamera.transform.rotation = Quaternion.Lerp(camRot, fightRot, f);

            yield return new WaitForSeconds(Time.deltaTime * playerValues.cameraSpeed);
        }

        playerValues.playerCamera.transform.position = fightPos;
        playerValues.playerCamera.transform.rotation = fightRot;

        yield return new WaitForSeconds(0.5f);

        playerValues.animator.SetBool("Wait", false);
        playerValues.animator.SetBool("Fight", true);

        yield return null;
    }

    IEnumerator MoveCameraToRun()
    {
        Vector3 fightPos = playerValues.posCamFight.transform.position;
        Vector3 camPos = playerValues.posCamRun.transform.position;
        Quaternion camRot = playerValues.posCamRun.transform.rotation;
        Quaternion fightRot = Quaternion.LookRotation(playerValues.posCamFight.transform.right);

        for (float f = 0f; f < 1f; f += Time.deltaTime * playerValues.cameraSpeed)
        {
            playerValues.playerCamera.transform.position = Vector3.Lerp(fightPos, camPos, f);
            playerValues.playerCamera.transform.rotation = Quaternion.Lerp(fightRot, camRot, f);

            yield return new WaitForSeconds(Time.deltaTime * playerValues.cameraSpeed);
        }

        playerValues.playerCamera.transform.position = camPos;
        playerValues.playerCamera.transform.rotation = camRot;

        playerValues.animator.SetBool("Wait", false);
        playerValues.animator.SetBool("Fight", false);

        playerValues.playerController.Run();

        gameObject.SetActive(false);

        yield return null;
    }
}
