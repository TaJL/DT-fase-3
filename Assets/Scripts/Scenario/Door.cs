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
    private Vector3 start_position;
    private Vector3 end_position;

    private Quaternion start_rotation;
    private Quaternion end_rotation;
    private Transform physical_part;


    void Awake()
    {
        //Setup
        physical_part = transform.FindChildWithTag("Start");
        start_position = physical_part.position;
        start_rotation = physical_part.rotation;
        end_position = transform.FindChildWithTag("End").position;
        end_rotation = transform.FindChildWithTag("End").rotation;
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
        Quaternion start_rot = physical_part.rotation;
        Vector3 end_pos = new_state ? end_position : start_position;
        Quaternion end_rot = new_state ? end_rotation : start_rotation;
        //Toggle
        opened = new_state;

        float counter = 0f;
        while (counter < 1)
        {
            counter = Mathf.Clamp(counter + Time.deltaTime / animation_time, 0, 1);
            physical_part.position = Vector3.LerpUnclamped(start_pos, end_pos, curve.Evaluate(counter));
            physical_part.rotation = Quaternion.LerpUnclamped(start_rot, end_rot, curve.Evaluate(counter));
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
        Transform end_transform = transform.FindChildWithTag("End");
        Gizmos.matrix = Matrix4x4.TRS(end_transform.position, end_transform.rotation, end_transform.localScale);
        Gizmos.DrawWireCube(Vector3.zero, transform.FindChildWithTag("Start").GetComponent<MeshRenderer>().bounds.size);
    }
#endif
}
