using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHandler : MonoBehaviour
{
    public PlayerValues playerValues;
    public Animator bossAnimator;

    [HideInInspector]
    public int maxLife;
    public int life = 10;

    public Image lifeBar;

    public GameObject model1;
    public GameObject ps1;
    public GameObject model2;
    public GameObject ps2;
    public GameObject model3;
    public GameObject ps3;

    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // when player meet a boss
    public void StartFight()
    {
        playerValues.playerController.Stop();
        playerValues.animator.SetBool("Wait", true);

        StartCoroutine(MoveCameraToFight());
    }

    // just stops fight animation and go to Idle
    public void PreEndFight()
    {
        playerValues.animator.SetBool("Wait", true);
        playerValues.animator.SetBool("Fight", false);
    }

    // kill the boss if the fight is successfull
    public void EndFight()
    {
        //explosion here

        if (model1.activeInHierarchy) Die(model1, ps1);
        if (model2.activeInHierarchy) Die(model2, ps2);
        if (model3.activeInHierarchy) Die(model3, ps3);

        StartCoroutine(MoveCameraToRun());
    }

    // play boss' animation (karate kick)
    public void HitPlayer()
    {
        bossAnimator.Play("Hit");
    }

    // animation of the boss' death
    public void Die(GameObject model, GameObject ps)
    {
        ps.SetActive(true);
        ps.GetComponent<ParticleSystem>().Play();

        Rigidbody[] bodies = model.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = false;
        }

        bossAnimator.enabled = false;
    }

    // move the camera to the fight position and rotation
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

    // move the camera back to her original place
    IEnumerator MoveCameraToRun()
    {
        yield return new WaitForSeconds(1f);

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
