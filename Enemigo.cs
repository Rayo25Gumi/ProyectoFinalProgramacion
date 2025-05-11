public class Enemigo
{
    public double Vida { get; set; }
    public double Ataque { get; set; }
    public double Nivel { get; set; }
    public string Nombre { get; set; }

    public Enemigo(double vida, double nivel, double ataque)
    {
        Vida = vida;
        Nivel = nivel;
        Ataque = ataque;
        Nombre = GenerarNombre();
    }

    public string GenerarNombre()
    {
        string[] nombres =
        {
            "Krull",
            "Zarnak",
            "Mordek",
            "Thalor",
            "Draknor",
            "Veldor",
            "Garnuk",
            "Branthor",
        };
        return nombres[new Random().Next(nombres.Length)];
    }

    public virtual void RecibirDaño(double ataque)
    {
        Vida -= ataque;
        if (Vida < 0)
            Vida = 0;
        Console.ForegroundColor = ConsoleColor.Red;
        EscribirConAnimacion(
            $"🗡️ {Nombre} ha perdido {Math.Round(ataque, 2)} de vida. Le queda {Math.Round(Vida, 2)} de vida."
        );
        Console.ResetColor();
    }

    public virtual void Atacar(Personaje personaje)
    {
        Random rnd = new Random();
        int dado1 = rnd.Next(1, 7);
        int dado2 = rnd.Next(1, 7);

        if (personaje is Guerrero && personaje.NumTirada >= 3)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            EscribirConAnimacion("💥 El Guerrero bloquea el ataque en su tercer turno.");
            personaje.NumTirada = 0;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            EscribirConAnimacion($"🎲 Dados del enemigo: {dado1} y {dado2}");
            double damage = dado1 + dado2;
            personaje.RecibirDaño(damage);
        }
        Console.ResetColor();
    }

    public virtual void MostrarEstado()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        EscribirConAnimacion($"📊 Estado de {Nombre}:");
        EscribirConAnimacion($"   ❤️ Vida: {Vida}");
        EscribirConAnimacion($"   ⭐ Nivel: {Nivel}");
        Console.ResetColor();
    }

    public void EscribirConAnimacion(string texto)
    {
        foreach (char c in texto)
        {
            Console.Write(c);
            Thread.Sleep(45);
        }
        Console.WriteLine();
    }
}
