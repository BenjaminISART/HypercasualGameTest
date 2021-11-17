using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleCollector : MonoBehaviour
{
    public List<string> collectibleTags = new List<string> { null };
    int nbCollectibleCollected;

    public Text associatedUIText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (associatedUIText != null)
        {
            associatedUIText.text = nbCollectibleCollected.ToString();
        }
    }

    public void AddCollectible(int nb = 1)
    {
        nbCollectibleCollected += nb;
    }

    public void RemoveCollectible(int nb = 1)
    {
        nbCollectibleCollected -= nb;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collectibleTags.Find(x => x == other.gameObject.tag) != null)
        {
            other.gameObject.SetActive(false);
            nbCollectibleCollected++;
        }
    }
}
