using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pokeBallRotation : MonoBehaviour
{
    public int speedRotation = 1;
    public bool rotateNegatif = false;
    public bool pokeBall = false;

    public float amplitude = 1f;
    public float speed = 1f;

    private float startPosY;
    private Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        startPosY = transform.position.y;
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pokeBall == true)
        {
            newPos.y = startPosY + amplitude * Mathf.Sin(speed * Time.time);
            transform.position = newPos;
        }
            
        if (rotateNegatif == false)
            transform.Rotate(Vector3.forward * Time.deltaTime * speedRotation);
        if (rotateNegatif == true)
            transform.Rotate(Vector3.back * Time.deltaTime * speedRotation);
    }
}
