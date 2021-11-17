using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xSpeed = 1f;
    public float zSpeed = 1f;
    public float maxOffset = 0f;
    Vector2 lastMousePos;

    bool isRunning = true;



    void Start()
    {

    }



    void FixedUpdate()
    {
        if (!isRunning)
        {
            lastMousePos = Input.mousePosition;
            return;
        }

#if IOSBUILD

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            savedTouchPos = touch.deltaPosition;

            print(touch.deltaPosition);

            Vector3 modifiedPos = transform.position;
            modifiedPos.x += savedTouchPos.x;
            transform.position = modifiedPos;
        }

#else

        Vector3 pos = transform.position;

        if (Input.GetMouseButton(0))
        {
            Vector2 delta = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - lastMousePos;

            pos += new Vector3(delta.x, 0f, 0f) * xSpeed * Time.fixedDeltaTime;
            pos.x = Mathf.Clamp(pos.x, -maxOffset, maxOffset);
        }

        print("ONPASSELA");
        pos.z += zSpeed * Time.deltaTime;
        transform.position = pos;

        lastMousePos = Input.mousePosition;
#endif
    }



    public void Stop()
    {
        isRunning = false;
    }

    public void Run()
    {
        isRunning = true;
    }
}
