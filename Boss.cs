public class Boss : Enemigo
{
    public int Resistencia { get; set; } = 5;
    private int turnosDesdeUltimaRegen = 0;

    public Boss()
        : base(50, 34, 0) { }

    public override void Atacar(Personaje personaje)
    {
        Random rnd = new Random();
        int dado1 = rnd.Next(1, 7);
        int dado2 = rnd.Next(1, 7);
        int dado3 = rnd.Next(1, 7);
        int dado4 = rnd.Next(1, 7);

        Console.ForegroundColor = ConsoleColor.DarkRed;
        EscribirConAnimacion($"🎲 Dados del jefe: {dado1}, {dado2}, {dado3}, {dado4}");
        double damage = dado1 + dado2 + dado3 + dado4;

        if (personaje is Guerrero && personaje.NumTirada >= 3)
        {
            EscribirConAnimacion("💥 El Guerrero bloquea el ataque del jefe.");
            personaje.NumTirada = 0;
        }
        else
        {
            EscribirConAnimacion("👹 Darklord realiza un ataque devastador.");
            personaje.RecibirDaño(damage);
        }

        turnosDesdeUltimaRegen++;
        if (turnosDesdeUltimaRegen == 3)
        {
            Vida += 2;
            Console.ForegroundColor = ConsoleColor.Green;
            EscribirConAnimacion("🧬 Darklord se regenera y gana 2 puntos de vida.");
            turnosDesdeUltimaRegen = 0;
        }

        if (Resistencia > 0)
        {
            Resistencia--;
        }
        Console.ResetColor();
        MostrarEstado();
    }

    public override void RecibirDaño(double ataque)
    {
        double dañoReducido = ataque - Resistencia;
        if (dañoReducido < 0)
            dañoReducido = 0;
        Vida -= dañoReducido;
        if (Vida < 0)
            Vida = 0;

        if (Vida != 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            EscribirConAnimacion($"🛡️ Darklord ha reducido el daño recibido ({Resistencia} de resistencia). Vida restante: {Math.Round(Vida, 2)}");
            Console.ResetColor();
        }
    }

    public override void MostrarEstado()
    {
        base.MostrarEstado();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        EscribirConAnimacion($"   🛡️ Resistencia restante: {Resistencia}");
        Console.ResetColor();
    }
}
