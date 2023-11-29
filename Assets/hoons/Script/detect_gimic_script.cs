using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class detect_gimic_script : MonoBehaviour
{
    public TMP_Text uiText;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Mushroom") && other.GetComponent<Mushroom>().is_growth == false)
        {
            uiText.text = "버섯을 성장시킬수 있다면...";
        }
        else if (other.CompareTag("FireWall"))
        {
            uiText.text = "불을 끄지 않으면 못지나가겠어";
        }
        else if (other.CompareTag("IceWall"))
        {
            uiText.text = "부수지는 못하지만 녹인다면 어떨까?";
        }
        else if (other.CompareTag("Torch"))
        {
            uiText.text = "불이 있다면 공간을 밝힐수 있겠군";
        }
        else if (other.CompareTag("Press_plate"))
        {
            uiText.text = "...함정은..아니겠지";
        }
        else if (other.CompareTag("Raser"))
        {
            uiText.text = "구식 감지형 레이저로군...연기를 발생시키면 무력화 됬었나?";
        }
        else if (other.CompareTag("Press_wall"))
        {
            uiText.text = "큰 바위를 사이에 두면 시간을 벌수 있을거야";
        }
    }


    private void OnTriggerExit(Collider other)
    {
        uiText.text = "";
    }
}

