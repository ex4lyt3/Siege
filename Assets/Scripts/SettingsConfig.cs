using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SettingsConfig : MonoBehaviour
{
    public GameObject Settings;
    public InputField SensitivityInput;
    public MouseLock MouseLock;

    bool SettingsState = false;
    bool debounce = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && debounce == false){
            debounce = true;
            SettingsState = !(SettingsState);
            Debug.Log(SettingsState);
            SettingsOpen(SettingsState);
            Invoke("DebounceFalse",0.2f);
        }
    }

    void SettingsOpen(bool arg)
    {
        Settings.SetActive(arg);
        Cursor.visible = arg;
        if (arg == true){
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void DebounceFalse()
    {
        debounce = false;
    }

    public void OnSensitivityChange(string arg)
    {
        float sensitivity = float.Parse(SensitivityInput.text);
        MouseLock.ChangeSensitivity(sensitivity);
    }
}
