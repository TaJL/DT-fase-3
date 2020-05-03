using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public enum Interaction
    {
        BARK,
        BOSS_DEFEATH
    }

    [SerializeField, Range(0.1f, 10f)]
    private float area_range;

    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float animation_time = 0.5f;
    private bool opened = false;
    private Vector3 start;
    private Vector3 end;

    private Transform physical_part;


    void Awake()
    {
        //Setup
        physical_part = transform.FindChildWithTag("Start");
        start = physical_part.position;
        end = transform.FindChildWithTag("End").position;
    }
    void Start()
    {
        //Bindings
        Events.OnBark += CheckBark;
        Events.OnBossDeath += Open;
    }

    public void CheckBark(Vector3 position)
    {
        if (Vector3.Distance(position, transform.position) <= area_range)
        {
            Open();
        }
    }

    private IEnumerator ToggleState(bool new_state)
    {
        //Determine start and end based on current state
        Vector3 start_pos = physical_part.position;
        Vector3 end_pos = new_state ? end : start;
        //Toggle
        opened = new_state;

        float counter = 0f;
        while (counter < 1)
        {
            counter = Mathf.Clamp(counter + Time.deltaTime / animation_time, 0, 1);
            physical_part.position = Vector3.LerpUnclamped(start_pos, end_pos, curve.Evaluate(counter));
            yield return null;
        }
    }
    public void Open()
    {
        StopAllCoroutines();
        StartCoroutine(ToggleState(true));
    }
    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(ToggleState(false));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Open();
        if (Input.GetKeyDown(KeyCode.C))
            Close();
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, area_range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.FindChildWithTag("End").position, transform.FindChildWithTag("Start").GetComponent<MeshRenderer>().bounds.size);
    }
#endif
}
