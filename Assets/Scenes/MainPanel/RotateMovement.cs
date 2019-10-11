using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMovement : MonoBehaviour
{
    float MAXROTATE = 300f;
    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles += new Vector3(0, MAXROTATE * Time.deltaTime, 0);
    }
}
