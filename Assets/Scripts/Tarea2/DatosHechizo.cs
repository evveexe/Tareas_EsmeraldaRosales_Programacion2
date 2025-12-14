using UnityEngine;

[CreateAssetMenu(fileName = "NuevoHechizo", menuName = "Hechizos o Nuevo Hechizo")]
public class DatosHechizo : ScriptableObject
{
    [Header("Hechizodatos")]
    public string nombre;
    public int dano;
    public int costeMana;

    [TextArea(2, 5)]
    public string descripcion;
}