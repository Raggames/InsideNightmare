using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugHelper : MonoBehaviour
{
    public static DebugHelper Instance;

    private bool inMenu;

    Text logText;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        var rt = DebugUIBuilder.instance.AddLabel("Debug");
        logText = rt.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Log(string msg)
    {
        logText.text = msg;
    }
}
