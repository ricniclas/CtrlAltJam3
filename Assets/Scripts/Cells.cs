using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cells 
{
    public enum Type
    {
        Empty,
        Mine,
        Number,
    }

    public Vector3Int position;
    public Type type;
    public int number;
    public bool revealed;
    public bool flagged;
    public bool exploded;
}
