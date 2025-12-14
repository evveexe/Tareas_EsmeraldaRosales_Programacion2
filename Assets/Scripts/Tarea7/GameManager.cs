using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public enum GameState
    {
        MainMenu,   
        Playing,    
        Paused,     
        GameOver    
    }
        
    [Header("Estado del Juego")]
    public GameState currentState = GameState.MainMenu;

    [Header("Configuración de Teclas")]
    public KeyCode startKey = KeyCode.Return;     
    public KeyCode pauseKey = KeyCode.P;          
    public KeyCode gameOverKey = KeyCode.G;       
    public KeyCode restartKey = KeyCode.R;        

    [Header("Mensajes en Consola")]
    public bool mostrarLogs = true;

    void Start()
    {        
        currentState = GameState.MainMenu;
        LogEstado("MainMenu");
    }

    void Update()
    {
        
        switch (currentState)
        {
            case GameState.MainMenu:
                LogEstado("En Menú Principal");

                if (Input.GetKeyDown(startKey))
                {
                    CambiarEstado(GameState.Playing);
                    Debug.Log("Comenzando juego");
                }
                break;

            case GameState.Playing:
                LogEstado("Jugando");

                if (Input.GetKeyDown(pauseKey))
                {
                    CambiarEstado(GameState.Paused);
                }

                if (Input.GetKeyDown(gameOverKey))
                {
                    CambiarEstado(GameState.GameOver);
                }
                break;

            case GameState.Paused:
                LogEstado("Juego Pausado");

                if (Input.GetKeyDown(pauseKey))
                {
                    CambiarEstado(GameState.Playing);
                }
               
                if (Input.GetKeyDown(startKey))
                {
                    CambiarEstado(GameState.MainMenu);
                }
                break;

            case GameState.GameOver:
                LogEstado("GAME OVER");

                if (Input.GetKeyDown(restartKey))
                {
                    CambiarEstado(GameState.Playing);
                    Debug.Log("Reiniciando juego...");
                }

                if (Input.GetKeyDown(startKey))
                {
                    CambiarEstado(GameState.MainMenu);
                }
                break;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Salir del juego");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
        
    void CambiarEstado(GameState nuevoEstado)
    {
        GameState estadoAnterior = currentState;
        currentState = nuevoEstado;

        Debug.Log($"Estado cambiado de {estadoAnterior} a {nuevoEstado}");
                
        OnStateChange(estadoAnterior, nuevoEstado);
    }
        
    void OnStateChange(GameState anterior, GameState nuevo)
    {
        switch (nuevo)
        {
            case GameState.Playing:
                Time.timeScale = 1f; 
                Debug.Log("Tiempo: Normal (x1)");
                break;

            case GameState.Paused:
                Time.timeScale = 0f; 
                Debug.Log("Tiempo: Pausado (x0)");
                break;

            case GameState.GameOver:
                Time.timeScale = 0.5f; 
                Debug.Log("Tiempo: Cámara lenta (x0.5)");
                break;

            case GameState.MainMenu:
                Time.timeScale = 1f; 
                Debug.Log("Volviendo al menú principal");
                break;
        }
    }

    void LogEstado(string mensaje)
    {
        if (mostrarLogs)
        {
            
            if (Time.frameCount % 120 == 0) 
            {
                Debug.Log($"[{currentState}] {mensaje}");
            }
        }
    }
      
    public void IniciarJuego() => CambiarEstado(GameState.Playing);
    public void PausarJuego() => CambiarEstado(GameState.Paused);
    public void ReanudarJuego() => CambiarEstado(GameState.Playing);
    public void TerminarJuego() => CambiarEstado(GameState.GameOver);
    public void VolverAlMenu() => CambiarEstado(GameState.MainMenu);
       
    public bool EstaJugando => currentState == GameState.Playing;
    public bool EstaPausado => currentState == GameState.Paused;
    public bool EsGameOver => currentState == GameState.GameOver;      
  
}