using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsConfig : MonoBehaviour
{
    public GameObject Settings;

    bool SettingsState = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E)){
            SettingsState = !SettingsState;
            SettingsOpen(SettingsState);
        }
    }

    public void SettingsOpen(bool arg)
    {
        Settings.SetActive(arg);
        Cursor.visible = false;
    }
}
