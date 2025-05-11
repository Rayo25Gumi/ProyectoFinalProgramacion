public class Arquero : Personaje
{
    public Arquero(double nivel, double habilidad)
        : base(10, nivel, habilidad, 4) { }

    public override void Atacar(Enemigo enemigo)
    {
        Random rnd = new Random();
        int dado1 = rnd.Next(1, 7);
        int dado2 = rnd.Next(1, 7);

        if (NumTirada >= 3)
        {
            int dado3 = rnd.Next(1, 7);
            int dado4 = rnd.Next(1, 7);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            EscribirConAnimacion($"🏹 El Arquero lanza una doble flecha certera.");
            EscribirConAnimacion($"🎲 Dados: [{dado1}], [{dado2}], [{dado3}], [{dado4}]");
            double damage = dado1 + dado2 + dado3 + dado4 + (Habilidad * 0.10) + Ataque;
            enemigo.RecibirDaño(damage);
            NumTirada = 0;
        }
        else if (NumTirada < 3)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            EscribirConAnimacion($"🎯 Primera flecha del Arquero.");
            EscribirConAnimacion($"🎲 Dados: [{dado1}] y [{dado2}]");
            double damage = dado1 + dado2 + (Habilidad * 0.10) + Ataque;
            enemigo.RecibirDaño(damage);
            NumTirada++;
        }

        EstadoHabilidad();
        Console.ResetColor();
    }

    public override void MostrarEstado()
    {
        base.MostrarEstado();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        EscribirConAnimacion($"⏳ Turnos para próximo disparo doble: {3 - NumTirada}");
        Console.ResetColor();
    }

    public override void PostCombate(Enemigo enemigo)
    {
        base.PostCombate(enemigo);
        MostrarEstado();
    }

    public void EstadoHabilidad()
    {
        int restante = 3 - NumTirada;
        if (restante <= 0)
            restante = 3;
        Console.ForegroundColor = ConsoleColor.Yellow;
        EscribirConAnimacion($"🎯 Turnos restantes para el disparo doble: {restante}");
        Console.ResetColor();
    }
}
