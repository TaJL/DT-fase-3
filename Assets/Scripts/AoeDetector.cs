using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AoeDetector<T> : MonoBehaviour where T: MonoBehaviour {
  public event System.Action<T> onDetected;
  public event System.Action<T> onUndetected;
  public T target;

  void OnTriggerStay (Collider c) {
    if (!gameObject.activeSelf) return;

    T found = c.GetComponentInParent<T>();
    if (found != target && found) {
      target = found;
      Detect();
      if (onDetected != null) onDetected(target);
    }
  }

  void OnTriggerExit (Collider c) {
    if (!gameObject.activeSelf) return;

    if (c.GetComponentInParent<T>() == target) {
      if (target != null) Undetect(target);
      if (onUndetected != null) onUndetected(target);
      target = null;
    }
  }

  public virtual void Undetect (T old) {}
  public virtual void Detect () {}
}
