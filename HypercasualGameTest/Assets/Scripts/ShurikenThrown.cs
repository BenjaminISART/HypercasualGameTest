using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenThrown : MonoBehaviour
{
    public CollectibleCollector collectibleCollector;
    BossHandler target;

    [HideInInspector]
    public bool isUsed = false;

    public float speed = 3f;
    public Transform basePosition;



    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Simple Animation of the thrown shuriken
    void FixedUpdate()
    {
        transform.localPosition += new Vector3(0f, .5f, 2f) * speed * Time.fixedDeltaTime;
        transform.Rotate(new Vector3(0f, 0f, speed * Time.fixedDeltaTime * 200f));
    }



    public void Use()
    {
        isUsed = true;
        gameObject.SetActive(true);
        collectibleCollector.RemoveCollectible();
    }



    // Reset shuriken from pool to his original position
    public void ResetShuriken()
    {
        transform.position = basePosition.position;
        isUsed = false;
        gameObject.SetActive(false);
    }



    // Collision check between shuriken and boss, should be called on every shuriken (maybe ... :))
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            target = other.gameObject.GetComponent<BossHandler>();
            target.life--;

            Rect r = target.lifeBar.rectTransform.rect;
            float newWidth = ((float)target.life / (float)target.maxLife) * 58f;
            target.lifeBar.rectTransform.sizeDelta = new Vector2(newWidth, r.height);

            if (target.life == 0)
            {
                target.EndFight();
            }

            ResetShuriken();
        }
    }
}
