using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float floatSpeed;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Invoke("DestroyThis", 1f);
        gameObject.transform.LookAt(player.transform);
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
