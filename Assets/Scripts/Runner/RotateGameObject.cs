using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    // Estas son las properties que puedes setear desde UnityEvent con set_<Nombre>
    public bool RotarEnX;
    public bool RotarEnY;
    public bool RotarEnZ;

    // Velocidad configurable
    public float velocidadRotacion = 90f; // grados por segundo

    void Update()
    {
        Vector3 rotacion = new Vector3(
            RotarEnX ? 1f : 0f,
            RotarEnY ? 1f : 0f,
            RotarEnZ ? 1f : 0f
        );

        transform.Rotate(rotacion * velocidadRotacion * Time.deltaTime);
    }
}
