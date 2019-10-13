using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    public string curTower;
    private Text CDText;
    private Text EffectText;
    private Text healthText;
    private GameObject skillTarget;
    private GameObject healthTarget;

    // Start is called before the first frame update
    private void Start()
    {
        InitTowerBoard();
    }
    private void Update()
    {
        if (Pause.GetInstance().GetState() == false)
        {
            CheckValue();
        }

    }
    public void CheckValue()
    {
        healthText.text = "当前剩余血量：" + healthTarget.GetComponent<DamageSystem>().GetCurHealth();
        switch (curTower)
        {
            case "a":
                CDText.text = "充能所需时间：" + (skillTarget.GetComponent<AreaDamageController>().GetCurValue("CDTime")) + "秒";
                break;
            case "b":
                CDText.text = "充能所需时间：" + (skillTarget.GetComponent<ProtectionField>().GetCurValue("CDTime")) + "秒";
                break;
            case "c":
                CDText.text = "充能所需时间：" + (skillTarget.GetComponent<ShootingSpeedUp>().GetCurValue("CDTime")) + "秒";
                break;
        }


    }
    private void InitTowerBoard()
    {
        CDText = GameObject.Find(curTower + "/Board/BoardCanvas/SkillBoard/CD").gameObject.GetComponent<Text>();
        healthText = GameObject.Find(curTower + "/Board/BoardCanvas/HealthBoard/Health").gameObject.GetComponent<Text>();
        skillTarget = GameObject.Find(curTower + "/SkillButton/SkillPoint");
        healthTarget = GameObject.Find("platform/" + curTower);
    }
}
public enum BoardType
{
    protectFieldCDType, protectFieldEffectType, areaDamegeCDType, speedUpEffectType, speedUpCDType, healthType,
}
