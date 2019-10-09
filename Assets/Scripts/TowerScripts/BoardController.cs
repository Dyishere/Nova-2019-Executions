using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    public string curTower;
    private Text protectFieldCDText;
    private Text protectFieldEffectText;
    private Text areaDamageCDText;
    private Text speedUpCDText;
    private Text speedUpEffectText;
    private Text healthText;
    private GameObject areaDamageTarget;
    private GameObject protectionFieldTarget;
    private GameObject healthTarget;
    private GameObject speedUpTarget;

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
        protectFieldCDText.text = "充能所需时间：" + (protectionFieldTarget.GetComponent<ProtectionField>().GetCurValue("CDTime")) + "秒" ;
        protectFieldEffectText.text = "剩余效果时间：" + (protectionFieldTarget.GetComponent<ProtectionField>().GetCurValue("EffectiveTime")) + "秒";
        areaDamageCDText.text = "充能所需时间：" + (areaDamageTarget.GetComponent<AreaDamageController>().GetCurValue("CDTime")) + "秒";
        healthText.text = "当前剩余血量：" + healthTarget.GetComponent<DamageSystem>().GetCurHealth();
        if (curTower == "c")
        {
            speedUpCDText.text = "充能所需时间：" + (speedUpTarget.GetComponent<ShootingSpeedUp>().GetCurValue("CDTime")) + "秒";
            speedUpEffectText.text = "剩余效果时间：" + (speedUpTarget.GetComponent<ShootingSpeedUp>().GetCurValue("EffectiveTime")) + "秒";
        }
    }
    private void InitTowerBoard()
    {
        if (curTower == "c")
        {
            speedUpEffectText = GameObject.Find("c/Board/BoardCanvas/SpeedUpBoard/Effect").gameObject.GetComponent<Text>();
            speedUpCDText = GameObject.Find("c/Board/BoardCanvas/SpeedUpBoard/CD").gameObject.GetComponent<Text>();
            speedUpTarget = GameObject.Find("platform/" + curTower + "/SpeedUpButton/SpeedUpPoint");
        }
        protectFieldEffectText = GameObject.Find(curTower + "/Board/BoardCanvas/ProtectionFieldBoard/Effect").gameObject.GetComponent<Text>();
        protectFieldCDText = GameObject.Find(curTower + "/Board/BoardCanvas/ProtectionFieldBoard/CD").gameObject.GetComponent<Text>();
        areaDamageCDText = GameObject.Find(curTower + "/Board/BoardCanvas/AreaDamageBoard/CD").gameObject.GetComponent<Text>();
        healthText = GameObject.Find(curTower + "/Board/BoardCanvas/HealthBoard/Health").gameObject.GetComponent<Text>();
        areaDamageTarget = GameObject.Find("platform/" + curTower + "/AreaDamageButton/AreaDamagePoint");
        protectionFieldTarget = GameObject.Find("platform/" + curTower + "/ProtectionFieldButton/ProtectionFieldPoint");
        healthTarget = GameObject.Find("platform/" + curTower);
    }
}
public enum BoardType
{
    protectFieldCDType, protectFieldEffectType, areaDamegeCDType, speedUpEffectType, speedUpCDType, healthType,
}
