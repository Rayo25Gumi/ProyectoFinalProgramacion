public class Guerrero : Personaje
{
    public Guerrero(double nivel, double habilidad)
        : base(12, nivel, habilidad, 2) { }

    public override void Atacar(Enemigo enemigo)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        EscribirConAnimacion("💥 El Guerrero lanza un golpe brutal.");
        Random rnd = new Random();
        int dado1 = rnd.Next(1, 7);
        int dado2 = rnd.Next(1, 7);
        EscribirConAnimacion($"🎲 Dados: {dado1} y {dado2}");
        NumTirada++;
        double damage = dado1 + dado2 + Ataque + (Habilidad * 0.10);
        if (NumTirada >= 4) NumTirada = 3;
        enemigo.RecibirDaño(damage);
        EstadoHabilidad();
        Console.ResetColor();
    }

    public void EstadoHabilidad()
    {
        int restante = 3 - NumTirada;
        Console.ForegroundColor = ConsoleColor.Yellow;
        EscribirConAnimacion($"⏳ Turnos restantes para bloqueo: {Math.Max(restante, 0)}");
        Console.ResetColor();
    }

    public override void MostrarEstado()
    {
        base.MostrarEstado();
        Console.ForegroundColor = ConsoleColor.Yellow;
        EscribirConAnimacion($"   🛡️ Turnos para próximo bloqueo: {3 - NumTirada}");
        Console.ResetColor();
    }

    public override void PostCombate(Enemigo enemigo)
    {
        base.PostCombate(enemigo);
        MostrarEstado();
    }
}