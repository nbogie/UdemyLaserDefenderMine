using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesScript : MonoBehaviour
{
    public Text todoText;
    public TextAsset todoTextAsset;


    void Start()
    {
        
        todoText.text = todoTextAsset.text;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            todoText.enabled = !todoText.enabled;
        }
    }
}
