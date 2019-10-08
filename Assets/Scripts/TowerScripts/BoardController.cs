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

    // Start is called before the first frame update
    private void Start()
    {
        InitTowerBoard();
    }
    private void Update()
    {
    }
    public void CheckTime(BoardType curBoard, int preTime,int curTime)
    {

        switch (curBoard)
        {
            case BoardType.protectFieldCDType:
                protectFieldCDText.text = "充能所需时间：" + (preTime - curTime + 1) + "秒" ;
                break;
            case BoardType.protectFieldEffectType:
                protectFieldEffectText.text = "剩余效果时间：" + (preTime - curTime + 1) + "秒";
                break;
            case BoardType.areaDamegeCDType:
                areaDamageCDText.text = "充能所需时间：" + (preTime - curTime + 1) + "秒";
                break;
            case BoardType.speedUpCDType:
                speedUpCDText.text = "充能所需时间：" + (preTime - curTime + 1) + "秒";
                break;
            case BoardType.speedUpEffectType:
                speedUpEffectText.text = "剩余效果时间：" + (preTime - curTime + 1) + "秒";
                break;
        }
    }
    private void InitTowerBoard()
    {
        if (curTower == "c")
        {
            speedUpEffectText = GameObject.Find("c/Board/BoardCanvas/SpeedUpBoard/Effect").gameObject.GetComponent<Text>();
            speedUpCDText = GameObject.Find("c/Board/BoardCanvas/SpeedUpBoard/CD").gameObject.GetComponent<Text>(); 
        }
        protectFieldEffectText = GameObject.Find(curTower + "/Board/BoardCanvas/ProtectionFieldBoard/Effect").gameObject.GetComponent<Text>();
        protectFieldCDText = GameObject.Find(curTower + "/Board/BoardCanvas/ProtectionFieldBoard/CD").gameObject.GetComponent<Text>();
        areaDamageCDText = GameObject.Find(curTower + "/Board/BoardCanvas/AreaDamageBoard/CD").gameObject.GetComponent<Text>();
    }
}
public enum BoardType
{
    protectFieldCDType, protectFieldEffectType, areaDamegeCDType, speedUpEffectType, speedUpCDType,
}
