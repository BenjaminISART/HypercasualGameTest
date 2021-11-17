using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHandler : MonoBehaviour
{
    public GameObject shurikenPrefab;
    public CollectibleCollector collectibleCollector;

    BossHandler target;
    int nbShurikenNeeded;
    int targetMaxLife;
    float lifeBarSize;

    // Shuriken Pool
    public ShurikenThrown shuriken1;
    public ShurikenThrown shuriken2;
    public ShurikenThrown shuriken3;

    // Start is called before the first frame update
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


    public void ThrowShuriken()
    {
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


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Boss")
        {
            target = other.gameObject.GetComponent<BossHandler>();
            nbShurikenNeeded = target.life;
            targetMaxLife = target.life;
            lifeBarSize = target.lifeBar.rectTransform.rect.width;
            target.StartFight();
        }
    }
}
