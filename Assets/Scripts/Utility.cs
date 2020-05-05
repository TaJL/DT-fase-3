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

  public static void MakeScaleFaceTarget (Transform thing, Transform target) {
    Vector3 scale = thing.localScale;
    scale.x = Mathf.Abs(scale.x) *
      Mathf.Sign(target.position.x - thing.position.x);
    thing.localScale = scale;
  }
}
