using UnityEngine;

public class LanzadorDeHechizos : MonoBehaviour
{
    public DatosHechizo hechizo;

    private void Start()
    {
        if (hechizo != null)
        {
            Debug.Log($"Hechizo preparado: {hechizo.nombre}");
            Debug.Log($"Mana del hechizo: {hechizo.costeMana}");
            Debug.Log($"Daño del hechizo: {hechizo.dano}");
            Debug.Log($"Descripcion: {hechizo.descripcion}");
        }
        else
        {
            Debug.LogWarning("Sin hechizo asignado");
        }
    }
}