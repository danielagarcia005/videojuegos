using UnityEngine;

public class MovimientoJugadora : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 6f;
    public float fuerzaSalto = 8f;

    [Header("Referencias")]
    public Rigidbody2D rb;
    public Animator animator;
    public Transform personajeVisual;

    private float movimientoHorizontal;
    private bool mirandoDerecha = true;

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        // Si no asignaste un objeto visual,
        // se utilizará este mismo objeto.
        if (personajeVisual == null)
            personajeVisual = transform;
    }

    void Update()
    {
        if (rb == null || animator == null)
            return;

        // MOVIMIENTO HORIZONTAL
        movimientoHorizontal = Input.GetAxisRaw("Horizontal");

        // GIRAR HACIA LA DIRECCIÓN DEL MOVIMIENTO
        if (movimientoHorizontal > 0 && !mirandoDerecha)
        {
            GirarPersonaje();
        }
        else if (movimientoHorizontal < 0 && mirandoDerecha)
        {
            GirarPersonaje();
        }

        // CAMINAR Y OCIO
        animator.SetFloat(
            "Velocidad",
            Mathf.Abs(movimientoHorizontal)
        );

        // AGACHARSE: S o flecha abajo
        bool agachada =
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow);

        animator.SetBool("Agachado", agachada);

        // SALTAR: ESPACIO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                fuerzaSalto
            );

            animator.SetTrigger("Salto");
        }

        // ATACAR: F
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Ataque");
        }
    }

    void FixedUpdate()
    {
        if (rb == null)
            return;

        // MOVER SIN ALTERAR LA VELOCIDAD VERTICAL
        rb.linearVelocity = new Vector2(
            movimientoHorizontal * velocidad,
            rb.linearVelocity.y
        );
    }

    void GirarPersonaje()
    {
        mirandoDerecha = !mirandoDerecha;

        Vector3 escala = personajeVisual.localScale;
        escala.x = Mathf.Abs(escala.x) *
                   (mirandoDerecha ? 1f : -1f);

        personajeVisual.localScale = escala;
    }
}
