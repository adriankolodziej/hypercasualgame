using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag
{
    None = 0,
    Citizen = 1,
    Building = 2,
}

public static class TagsComparer
{
    public static bool Compare(string tagOne, Tag tagTwo)
    {
        return tagOne == tagTwo.ToString();
    }
}
