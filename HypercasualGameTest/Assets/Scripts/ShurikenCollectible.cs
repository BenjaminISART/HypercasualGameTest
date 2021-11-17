using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenCollectible : MonoBehaviour
{
    public float rotationSpeed = 1f;
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.y += rotationSpeed * Time.fixedDeltaTime;
        transform.localRotation = Quaternion.Euler(rotation);

        Vector3 position = transform.localPosition;
        position.y = Mathf.Sin(time) * 0.5f;
        time += Time.fixedDeltaTime * 5f;
        transform.localPosition = position;
    }
}
