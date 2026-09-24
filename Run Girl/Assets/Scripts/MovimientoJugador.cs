
using UnityEngine;

public class MovimientoJugadora : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 6f;
    public float fuerzaSalto = 7f;

    [Header("Deteccion del suelo")]
    public Transform puntoSuelo;
    public float radioSuelo = 0.12f;
    public LayerMask capaSuelo;

    private Rigidbody2D rb;

    private float movimientoHorizontal;
    private bool solicitarSalto;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movimientoHorizontal =
            Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            solicitarSalto = true;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimientoHorizontal * velocidad,
            rb.linearVelocity.y
        );

        bool tocandoSuelo = Physics2D.OverlapCircle(
            puntoSuelo.position,
            radioSuelo,
            capaSuelo
        );

        if (solicitarSalto && tocandoSuelo)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                fuerzaSalto
            );
        }

        solicitarSalto = false;
    }
}
