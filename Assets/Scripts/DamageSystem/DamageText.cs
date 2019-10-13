using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float floatSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * floatSpeed);
    }

    public void DestroyThis()
    {
        Destroy(this);
    }
}
