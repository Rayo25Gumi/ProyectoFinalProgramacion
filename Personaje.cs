public class Personaje
{
    public double Vida { get; set; }
    public double Nivel { get; set; }
    public double Habilidad { get; set; }
    public double Ataque { get; set; }
    public int NumTirada { get; set; }
    public double Puntuacion { get; set; } = 0;
    public double PuntosDeNivel { get; set; } = 0;
    public double PuntosPorNivel { get; set; } = 2;
    public int PocionesVida { get; set; } = 0;
    public int PocionesAtaque { get; set; } = 0;
    public string NombreJugador { get; set; }

    public Personaje(double vida, double nivel, double habilidad, double ataque)
    {
        Vida = vida;
        Nivel = nivel;
        Habilidad = habilidad;
        Ataque = ataque;
        NumTirada = 0;
    }

    public virtual void Atacar(Enemigo enemigo)
    {
        NumTirada++;
        Random rnd = new Random();
        int dado1 = rnd.Next(1, 7);
        int dado2 = rnd.Next(1, 7);
        Console.ForegroundColor = ConsoleColor.Blue;
        EscribirConAnimacion($"🎲 Tirada de dados: [{dado1}] y [{dado2}]");
        Console.ResetColor();

        double damage = dado1 + dado2 + Ataque + (Habilidad * 0.10);
        enemigo.RecibirDaño(damage);
        if (enemigo.Vida == 0) { }
    }

    public virtual void RecibirDaño(double ataque)
    {
        Vida -= ataque;
        if (Vida < 0)
            Vida = 0;
        Console.ForegroundColor = ConsoleColor.Red;
        EscribirConAnimacion($"💔 {NombreJugador} ha perdido {Math.Round(ataque, 2)} de vida. Vida restante: {Math.Round(Vida, 2)}");
        Console.ResetColor();
    }

    public void SubirNivel()
    {
        Nivel++;
        Vida += 2;
        Console.ForegroundColor = ConsoleColor.Green;
        EscribirConAnimacion("⬆️ ¡Nivel aumentado! Has ganado +2 de vida!");
        Random rnd = new Random();
        if (rnd.Next(0, 2) == 1)
        {
            Habilidad++;
            EscribirConAnimacion("✨ ¡Habilidad mejorada en +1!");
        }
        Console.ResetColor();
    }

    public void AñadirPuntuacion(double puntos)
    {
        Puntuacion += puntos;
        Console.ForegroundColor = ConsoleColor.Cyan;
        EscribirConAnimacion($"🏅 Puntos ganados: +{puntos}. Total: {Puntuacion}");
        Console.ResetColor();
    }

    public virtual void MostrarEstado()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        EscribirConAnimacion("\n📊 ESTADO DEL PERSONAJE 📊");
        EscribirConAnimacion($"🧍 Nombre: {NombreJugador}");
        EscribirConAnimacion(
            $"❤️ Vida: {Vida} | ⭐ Nivel: {Nivel} | 🗡️ Ataque Extra: {Ataque} | ✨ Habilidad: {Habilidad}"
        );
        EscribirConAnimacion(
            $"📈 Puntos para subir de nivel: {Math.Round(PuntosPorNivel - PuntosDeNivel, 2)}"
        );
        EscribirConAnimacion($"🧪 Pociones ➤ Vida: {PocionesVida} | Ataque: {PocionesAtaque}");
        Console.ResetColor();
    }

    public void PrepararCombate()
    {
        if (PocionesVida == 0 && PocionesAtaque == 0)
            return;
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        EscribirConAnimacion("🔧 ¿Deseas usar un objeto antes del combate? (si/no)");
        Console.ResetColor();
        string respuesta = Console.ReadLine();
        if (respuesta.ToLower() == "si")
            UsarPocion();
    }

    public void UsarPocion()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        EscribirConAnimacion("Elige poción para usar: 1. Vida 🧪 | 2. Ataque 🧪");
        Console.ResetColor();
        string opcion = Console.ReadLine();

        if (opcion == "1" && PocionesVida > 0)
        {
            Vida += 7;
            PocionesVida--;
            EscribirConAnimacion("🧪 Has usado una poción de vida. +7 vida restaurada.");
        }
        else if (opcion == "2" && PocionesAtaque > 0)
        {
            Ataque += 6;
            PocionesAtaque--;
            EscribirConAnimacion("🧪 Has usado una poción de ataque. +6 ataque aumentado.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            EscribirConAnimacion("⚠️ No tienes esa poción disponible.");
            Console.ResetColor();
        }
    }

    public virtual void PostCombate(Enemigo enemigo)
    {
        Random rnd = new Random();

        if (enemigo is Boss)
        {
            AñadirPuntuacion(30);
            PuntosDeNivel += 5;
        }
        else if (enemigo is EnemigoEspecial)
        {
            AñadirPuntuacion(3);
            PuntosDeNivel += rnd.Next(2, 4);
        }
        else
        {
            AñadirPuntuacion(1);
            PuntosDeNivel += rnd.Next(2, 3);
        }

        Console.ForegroundColor = ConsoleColor.White;
        EscribirConAnimacion("✨ Has ganado experiencia para subir de nivel.");
        Console.ResetColor();

        do
        {
            if (PuntosDeNivel >= PuntosPorNivel)
            {
                PuntosDeNivel -= PuntosPorNivel;
                PuntosPorNivel += 0.2;
                SubirNivel();
            }
        } while (PuntosDeNivel >= PuntosPorNivel);

        int probabilidad = rnd.Next(0, 100);
        if (probabilidad < 70)
        {
            if (rnd.Next(0, 2) == 0)
            {
                PocionesVida++;
                Console.ForegroundColor = ConsoleColor.Green;
                EscribirConAnimacion("🎁 ¡Has encontrado una poción de vida!");
            }
            else
            {
                PocionesAtaque++;
                Console.ForegroundColor = ConsoleColor.Red;
                EscribirConAnimacion("🎁 ¡Has encontrado una poción de ataque!");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            EscribirConAnimacion("📦 No has encontrado ninguna poción esta vez.");
        }
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
