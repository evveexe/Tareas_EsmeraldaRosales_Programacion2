using UnityEngine;

public abstract class Enemigo : MonoBehaviour
{
    protected int vidaMaxima = 100;
    protected int vidaActual;

    protected virtual void Start()
    {
        vidaActual = vidaMaxima;
    }

    public abstract void Atacar();

    public virtual void Morir()
    {
        Debug.Log("Miren, se murio xdxd");
        Destroy(gameObject);
    }
}
