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

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition += new Vector3(0f, .5f, 2f) * speed * Time.fixedDeltaTime;
        transform.Rotate(new Vector3(0f, 0f, speed * Time.fixedDeltaTime * 50f));
    }

    public void Use()
    {
        isUsed = true;
        gameObject.SetActive(true);
    }

    public void ResetShuriken()
    {
        transform.position = basePosition.position;
        isUsed = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            collectibleCollector.RemoveCollectible();
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
