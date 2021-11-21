using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AmnoUI : MonoBehaviour
{
    public Text amno;

    public void ChangeAmno(string arg)
    {
        amno.text = arg;
    }
}
