using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : Attribute
{

    public ReadOnlyAttribute()
    {
    }
    
}