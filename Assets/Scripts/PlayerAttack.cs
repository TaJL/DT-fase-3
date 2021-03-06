using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour{
  public event System.Action onAttack;

  public int damage;
  public float pushBackStrength = 5;
  public Transform pushBackSource;
  public GameObject attackAoe;

  void OnTriggerEnter (Collider c) {
    Attackable attackable = c.GetComponent<Attackable>();
    if (c.gameObject.activeInHierarchy && attackable &&
        attackable != Player.Instance.attackable) {
      attackable.GetDamage(damage, pushBackSource.position, pushBackStrength);
    }
  }

  void Update () {
    if (PlayerDecisions.isActive || NpcDialoguePlaceholder.Instance.IsVisible) return;

    if (Input.GetButtonDown("Fire1")) {
      StopAllCoroutines();
      StartCoroutine(_Attack());
      if (onAttack != null) onAttack();
    }
  }

  IEnumerator _Attack () {
    attackAoe.SetActive(true);
    yield return null;
    attackAoe.SetActive(false);
  }
}
