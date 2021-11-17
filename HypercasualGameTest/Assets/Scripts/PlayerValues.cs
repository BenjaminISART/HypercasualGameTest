using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject posCamFight;
    public GameObject posCamRun;
    public GameObject lookAtRun;
    public Animator animator;
    public PlayerController playerController;
    public CollectibleCollector collectibleCollector;
    public Rigidbody playerRigidbody;
    public float cameraSpeed;

    [HideInInspector]
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("COUCOULESLOULOUS");
        if (other.gameObject.tag == "Obstacle")
        {
            isDead = true;
        }
    }
}
