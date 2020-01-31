using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    [SerializeField]
    private ScrollMove scrollMove;

    [SerializeField]
    private GameObject buttons;

    public void OpenArmor()
    {
        buttons.SetActive(false);
        scrollMove.gameObject.SetActive(true);
        scrollMove.MoveToFlag(1);
    }

    public void OpenWeapon()
    {
        scrollMove.gameObject.SetActive(true);
        buttons.SetActive(false);
        scrollMove.MoveToFlag(2);
    }

    public void OpenMagic()
    {
        scrollMove.gameObject.SetActive(true);
        buttons.SetActive(false);
        scrollMove.MoveToFlag("Magic");
    }

    public void Back()
    {
        scrollMove.gameObject.SetActive(false);
        buttons.SetActive(true);
    }

}
