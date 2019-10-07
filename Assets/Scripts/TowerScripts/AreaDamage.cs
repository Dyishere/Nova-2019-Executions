using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public float movingSpeed;
    public GameObject endPos;
    public float CDTime;
    private Vector3 startPos;
    private bool isActive;
    private bool isCD;

    // Start is called before the first frame update
    void Start()
    {
        InitAreaDamage();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Working();
    }

    public void BeginAreaDamage()
    {
        if (isActive || isCD)
        { 
            return;
        }
        else if (GetComponentInParent<DamageSystem>().GetCurState() == DamageState.DEATH)
        {
            Debug.Log("该塔已损坏，无法发动技能");
            return;
        }
        else
        {
            gameObject.SetActive(true);
            isActive = true;
            StartCoroutine(CD());
        }
    }
    private void Working()
    {
        if (!isActive)
        {
            return;
        }
        else
        {
            transform.Translate((endPos.transform.position - startPos) * movingSpeed * Time.deltaTime);
            if (endPos.transform.position.z < transform.position.z)
            {
                gameObject.SetActive(false);
                isActive = false;
                transform.position = startPos;
            }
        }
    }
    private void InitAreaDamage()
    {
        isCD = false;
        gameObject.SetActive(false);
        isActive = false;
        startPos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            other.gameObject.GetComponent<DamageSystem>().Damage(other.gameObject.GetComponent<DamageSystem>().GetCurHealth());
        }
    }
    IEnumerator CD()
    {
        isCD = true;
        yield return new WaitForSeconds(CDTime);
        isCD = false;
    }
}
