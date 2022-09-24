using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_Puzzle : MonoBehaviour
{
    public enum Book_Type
    {
        Magic,
        Clock,
        Gear
    }

    public Book_Type book_Type;
    public bool on_trigger;
    public bool isClear;

    private void Awake()
    {
        on_trigger = false;
        isClear = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        on_trigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        on_trigger = false;
    }
}
