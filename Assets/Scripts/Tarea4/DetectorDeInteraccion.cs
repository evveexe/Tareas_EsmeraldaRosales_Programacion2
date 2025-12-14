using UnityEngine;

public class DetectorDeInteraccion : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    public KeyCode teclaInteraccion = KeyCode.E;
    public float rangoInteraccion = 3f;
    public Camera camaraJugador;

    void Start()
    {
        if (camaraJugador == null)
            camaraJugador = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaInteraccion))
        {
            IntentarInteractuar();
        }
    }

    void IntentarInteractuar()
    {        
        Ray ray = camaraJugador.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * rangoInteraccion, Color.green, 1f);

        if (Physics.Raycast(ray, out hit, rangoInteraccion))
        {
            IInteractuable interactuable = hit.collider.GetComponent<IInteractuable>();

            if (interactuable != null)
            {
                Debug.Log($"Interactuando con {hit.collider.gameObject.name}");
                interactuable.Interactuar();
            }
        }
    }

    
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(teclaInteraccion))
        {
            IInteractuable interactuable = other.GetComponent<IInteractuable>();
            if (interactuable != null)
            {
                interactuable.Interactuar();
            }
        }
    }
}