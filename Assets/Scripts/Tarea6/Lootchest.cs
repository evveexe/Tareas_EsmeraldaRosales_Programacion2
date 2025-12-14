using UnityEngine;

public class LootChest : MonoBehaviour
{
    
    public enum ItemRarity
    {
        Comun,      
        Raro,       
        Epico,      
        Legendario  
    }

    
    [Header("Configuración del Cofre")]
    public ItemRarity itemEncontrado = ItemRarity.Comun;

    [Header("Mensajes Personalizables")]
    public string mensajeComun = "Poti mediocre jsj";
    public string mensajeRaro = "Iten raruu!";
    public string mensajeEpico = "Epicardo";
    public string mensajeLegendario = "LEYENDARI";

    void Start()
    {        
        AbrirCofre();
    }

    void AbrirCofre()
    {
        Debug.Log("El cofre se abre...");

        switch (itemEncontrado)
        {
            case ItemRarity.Comun:
                Debug.Log($"<color=#808080>{mensajeComun}</color>"); 
                Debug.Log("<color=#808080>[Ítem Común]</color> Valor: 10 monedas");
                break;

            case ItemRarity.Raro:
                Debug.Log($"<color=green>{mensajeRaro}</color>"); 
                Debug.Log("<color=green>[Ítem Raro]</color> Valor: 50 monedas");
                break;

            case ItemRarity.Epico:
                Debug.Log($"<color=purple>{mensajeEpico}</color>"); 
                Debug.Log("<color=purple>[Ítem Épico]</color> Valor: 200 monedas");
                break;

            case ItemRarity.Legendario:
                Debug.Log($"<color=orange>{mensajeLegendario}</color>"); 
                Debug.Log("<color=yellow>[Ítem Legendario]</color> Valor: 1000 monedas");
                break;

            default:
                Debug.Log("Error: Rareza desconocida");
                break;
        }
       
    }
   
    [ContextMenu("Probar Abrir Cofre")]
    void ProbarAbrirCofre()
    {
        AbrirCofre();
    }
   
    public void CambiarRareza(ItemRarity nuevaRareza)
    {
        itemEncontrado = nuevaRareza;
        Debug.Log($"Rareza cambiada a: {nuevaRareza}");
    }
}