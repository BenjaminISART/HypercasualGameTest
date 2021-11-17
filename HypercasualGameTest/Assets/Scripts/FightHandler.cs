using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHandler : MonoBehaviour
{
    public PlayerValues playerValues;
    public GameObject shurikenPrefab;
    public CollectibleCollector collectibleCollector;

    BossHandler target;
    int nbShurikenNeeded;

    // Shuriken Pool
    public ShurikenThrown shuriken1;
    public ShurikenThrown shuriken2;
    public ShurikenThrown shuriken3;



    // Set the Shuriken Pool
    void Start()
    {
        shuriken1.ResetShuriken();
        shuriken2.ResetShuriken();
        shuriken3.ResetShuriken();
    }



    // Update is called once per frame
    void Update()
    {
        
    }



    // The name speaks for itself. 
    public void ThrowShuriken()
    {
        if (nbShurikenNeeded > 0 && collectibleCollector.NbCollectibleCollected == 0)
        {
            StartCoroutine(MakePlayerDie());
            return;
        }

        if (nbShurikenNeeded > 0)
        {
            if (!shuriken1.isUsed) shuriken1.Use();
            else if (!shuriken2.isUsed) shuriken2.Use();
            else if (!shuriken3.isUsed) shuriken3.Use();

            nbShurikenNeeded--;

            if (nbShurikenNeeded == 0)
            {
                target.PreEndFight();
            }
        }
    }


    // same
    IEnumerator MakePlayerDie()
    {
        target.PreEndFight();

        yield return new WaitForSeconds(0.5f);

        target.HitPlayer();

        yield return new WaitForSeconds(0.3f);

        playerValues.isDead = true;

        yield return null;
    }    



    // Starts fight if player meet a boss
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Boss")
        {
            target = other.gameObject.GetComponent<BossHandler>();
            nbShurikenNeeded = target.life;
            target.StartFight();
        }
    }
}
