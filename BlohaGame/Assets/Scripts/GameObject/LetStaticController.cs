using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetStaticController : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private float indent;
    [SerializeField] private float border;

    public float GetLetSize()
    {
        return size;
    }

    public float GetLetIndent()
    {
        return indent;
    }

    public float GetLetBorder()
    {
        return border;
    }
}
