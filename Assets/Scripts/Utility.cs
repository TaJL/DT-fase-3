using UnityEngine;
public static class Utility
{

    public static Transform FindChildWithTag(this Transform parent, string tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).CompareTag(tag))
            {
                return parent.GetChild(i);
            }
        }
        return null;
    }

}