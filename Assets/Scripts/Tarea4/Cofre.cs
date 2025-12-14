using UnityEngine;

public class Cofre : MonoBehaviour, IInteractuable
{
    [Header("Configuración del Cofre")]
    public int oro = 10;
    public bool yaAbierto = false;

    public void Interactuar()
    {
        if (yaAbierto)
        {
            Debug.Log("El cofre ya está abierto...");
            return;
        }

        yaAbierto = true;
        Debug.Log($"{oro} de oro obtenido");
                
        GetComponent<Renderer>().material.color = Color.yellow; 
    }
}