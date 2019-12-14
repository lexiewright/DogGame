using UnityEngine;
using System.Collections;
using TMPro;

public class ButtonClick : MonoBehaviour
{

    public GameObject sniffReply;
    public GameObject lickReply;

    public void SniffButtonClick()
    {
        if (lickReply.activeSelf == true)
        {
            lickReply.SetActive(false);
            sniffReply.SetActive(true);
        }
        else
        {
           sniffReply.SetActive(true);
        }
    }

    public void LickButtonClick()
    {
        if (sniffReply.activeSelf == true)
        {
            sniffReply.SetActive(false);
            lickReply.SetActive(true);
        }
        else
        {

            lickReply.SetActive(true);
        }
    }
}
