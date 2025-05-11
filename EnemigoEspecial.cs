public class EnemigoEspecial : Enemigo
{
    public double Resistencia { get; set; }

    public EnemigoEspecial(double vida, double nivel, double ataque, int resistencia)
        : base(vida, nivel, ataque)
    {
        Resistencia = resistencia;
        Nombre += " el Mágico";
    }

    public override void RecibirDaño(double ataque)
    {
        if (Resistencia > 0)
        {
            double dañoReducido = ataque - Resistencia;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            EscribirConAnimacion(
                $"🛡️ {Nombre} ha reducido el daño con su resistencia ({Resistencia})."
            );
            base.RecibirDaño(dañoReducido);
        }
        else
        {
            base.RecibirDaño(ataque);
        }
        Console.ResetColor();
    }

    public override void Atacar(Personaje personaje)
    {
        if (Resistencia > 0)
        {
            Resistencia--;
        }
        base.Atacar(personaje);
    }

    public override void MostrarEstado()
    {
        base.MostrarEstado();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        EscribirConAnimacion($"   🛡️ Resistencia: {Resistencia}");
        Console.ResetColor();
    }
}
