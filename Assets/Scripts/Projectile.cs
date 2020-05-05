using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {
  public Vector3 direction;
  public GameObject dustPrototype;
  public float speed = 10;
  public float rotationSpeed = 360;
  public Attackable caster;
  public int damage = 1;
  public Sprite[] available;
  public SpriteRenderer r;

  void OnEnable () {
    r.sprite = available[Random.Range(0, available.Length)];
  }

  void OnTriggerEnter (Collider c) {
    Attackable attackable = c.GetComponentInParent<Attackable>();
    if (attackable && attackable != caster) {
      attackable.GetDamage(damage, transform.position, 0);
      Explode();
    }
  }

  void OnCollisionEnter (Collision c) {
    Explode(c);
  }

  void Update () {
    transform.position += direction * speed * Time.deltaTime;
    transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
  }

  public void Explode () {
    GameObject dust = Instantiate(dustPrototype);
    dust.transform.position = transform.position;
    dust.transform.forward = transform.position - Player.Instance.transform.position;
    Destroy(gameObject);
  }

  public void Explode (Collision c) {
    GameObject dust = Instantiate(dustPrototype);
    dust.transform.position = c.GetContact(0).point;
    dust.transform.forward = c.GetContact(0).normal;
    Destroy(gameObject);
  }
}
