using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public float bobSpeed = 14f;
    public float bobAmount = 0.05f;

    private float defaultYpos = 0;
    private float timer = 0;

    void Start()
    {
        defaultYpos = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
            timer += Time.deltaTime * bobSpeed;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                defaultYpos + Mathf.Sin(timer) * bobAmount,
                transform.localPosition.z
            );
        }
        else {
            timer = 0;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                Mathf.Lerp(transform.localPosition.y, defaultYpos, Time.deltaTime * bobSpeed),
                transform.localPosition.z
            );
        }
    }
}
