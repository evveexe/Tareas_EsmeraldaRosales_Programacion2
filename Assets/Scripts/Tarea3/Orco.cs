using UnityEngine;

public class Orco : Enemigo
{
    protected override void Start()
    {
        vidaMaxima = 250;
        base.Start();
    }

    public override void Atacar()
    {
        Debug.Log("Ataque de Orco maloso");
    }

    public override void Morir()
    {
        Debug.Log("Gruñidito");
        base.Morir();
    }
}
