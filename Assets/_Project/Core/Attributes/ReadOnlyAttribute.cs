using UnityEngine;
using UnityEditor;

[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public sealed class ReadOnlyAttribute : PropertyAttribute
{
    // See the attribute guidelines at
    //  http://go.microsoft.com/fwlink/?LinkId=85236
    // This is a positional argument
    public ReadOnlyAttribute()
    {
    }
}