using System.Runtime.CompilerServices;
using UnityEngine;

public class Sway : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float swayClamp = 0.09f;
    [Space]
    [SerializeField] private float smoothing = 3;

    private Vector3 _origin;
    private void Start()
    {
        _origin = transform.localPosition;
    }
    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        input.x = Mathf.Clamp(input.x, -swayClamp, swayClamp);
        input.y = Mathf.Clamp(input.y, -swayClamp, swayClamp);

        Vector3 target = new Vector3(-input.x, -input.y, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, target + _origin, Time.deltaTime * smoothing);
    }
}
