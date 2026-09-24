
using UnityEngine;

public class CamaraSeguidora : MonoBehaviour
{
    public Transform objetivo;
    public float suavidad = 5f;

    private float alturaInicial;

    void Start()
    {
        alturaInicial = transform.position.y;
    }

    void LateUpdate()
    {
        if (objetivo == null)
            return;

        float nuevaAltura = Mathf.Min(
            alturaInicial,
            objetivo.position.y
        );

        Vector3 posicionDeseada = new Vector3(
            transform.position.x,
            nuevaAltura,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            posicionDeseada,
            suavidad * Time.deltaTime
        );
    }
}