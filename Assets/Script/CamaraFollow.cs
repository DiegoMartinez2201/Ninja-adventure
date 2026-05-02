using UnityEngine;

public class Camara : MonoBehaviour
{
    // Personaje a seguir
    public Transform target;

    // Distancia de la cámara respecto al personaje
    public Vector3 offset = new Vector3(0, 5, -10);

    // Velocidad de seguimiento
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Posición deseada
        Vector3 desiredPosition = target.position + offset;

        // Movimiento suave
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Aplicar posición
        transform.position = smoothedPosition;

        // Mirar al personaje
        transform.LookAt(target);
    }
}
