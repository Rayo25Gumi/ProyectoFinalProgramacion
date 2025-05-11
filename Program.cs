using System;
using System.Diagnostics;
using System.Threading;

// para probar cosas puedes darle a usar una pocion que tengas muchas para ver como funcionan las habilidades especiales de los enemigos etc, usa en genral las pociones para el testing
internal class Programa
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        DateTime inicioPartida = DateTime.Now;
        Process musicaProceso = null;
        try
        {musicaProceso = Process.Start(new ProcessStartInfo { FileName = "fondo.mp3", UseShellExecute = true });}
        catch (Exception)
        {Console.WriteLine("⚠️ No se pudo reproducir el archivo de música.");}

        Console.Title = "Aventura Épica: El Rescate de la Princesa";
        Console.Clear();
        EscribirConAnimacion("👑 ¡Bienvenido, valiente aventurero!\n");
        EscribirConAnimacion("Escribe tu nombre: ");
        string nombre = Console.ReadLine();

        EscribirConAnimacion("\nElige tu clase:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        EscribirConAnimacion("1. Guerrero 🛡️\n");
        Console.ForegroundColor = ConsoleColor.Cyan;
        EscribirConAnimacion("2. Mago ✨\n");
        Console.ForegroundColor = ConsoleColor.Green;
        EscribirConAnimacion("3. Arquero 🏹\n");
        Console.ResetColor();
        EscribirConAnimacion(">> ");
        int clase = Convert.ToInt32(Console.ReadLine());

        Personaje jugador;
        switch (clase)
        {
            case 1:
                jugador = new Guerrero(10, 0);
                break;
            case 2:
                jugador = new Mago(10, 0);
                break;
            case 3:
                jugador = new Arquero(10, 0);
                break;
            default:
                jugador = new Guerrero(10, 0);
                break;
        }

        string claseJugador;

        switch (clase)
        {
            case 1:
                claseJugador = " el Guerrero";
                break;
            case 2:
                claseJugador = " el Mago";
                break;
            case 3:
                claseJugador = " el Arquero";
                break;
            default:
                claseJugador = " el Aventurero";
                break;
        }

        jugador.NombreJugador = nombre + claseJugador;

        Console.Clear();
        EscribirConAnimacion($"\n🗡️ {jugador.NombreJugador}, tu misión es rescatar a la princesa secuestrada por el temible Darklord.\n");

        string[] eventos =
        {
            "🏚️ Escapas de la mazmorra y {0} bloquea tu camino.",
            "🌉 Cruzando un puente derrumbado, aparece {0}.",
            "🌲 En el bosque sombrío, {0} te embosca.",
            "⛰️ Subes por una colina empinada y {0} te espera.",
            "🏞️ Al salvar a un viajero, {0} te ataca por la espalda.",
            "🏕️ Acampando por la noche, {0} aparece en las sombras.",
            "🌊 Un río caudaloso trae a {0} nadando hacia ti.",
            "🌪️ Una tormenta repentina deja ver a {0} frente a ti.",
            "🔥 Tras un incendio en la aldea, {0} aparece.",
            "🏰 En las ruinas del castillo antiguo, {0} te desafía.",
            "🪨 Entre los escombros de una batalla, {0} sobrevive.",
            "🌫️ Una niebla espesa revela a {0} acechándote.",
            "⛩️ Al cruzar un templo abandonado, {0} se manifiesta.",
            "🌌 Una grieta mágica invoca a {0} en tu camino.",
            "⚰️ Junto a un cementerio, {0} surge entre los muertos.",
        };
        for (int i = 1; i <= 15; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            EscribirConAnimacion("\n────────────────────────────────────────────");
            Console.ResetColor();

            Random aleatorio = new Random();
            bool especial = (i % 5 == 0);
            Enemigo enemigo = null;

            if (especial)
            {
                enemigo = new EnemigoEspecial(
                    aleatorio.Next(10, 20),
                    aleatorio.Next(20, 30),
                    aleatorio.Next(2, 3),
                    aleatorio.Next(1, 5)
                );
            }
            else
            {
                enemigo = new Enemigo(
                    aleatorio.Next(8, 18),
                    aleatorio.Next(10, 16),
                    aleatorio.Next(1, 2)
                );
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            EscribirConAnimacion(
                $"{eventos[i - 1].Replace("{0}", enemigo.Nombre)} (Nivel {enemigo.Nivel}, Vida {enemigo.Vida})" //tuve que buscar como usar el replace
            );
            Console.ResetColor();

            jugador.PrepararCombate();

            if (jugador.Nivel > enemigo.Nivel)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                EscribirConAnimacion("💥 ¡Tu nivel es superior! Ganas automáticamente el combate.");
                Console.ResetColor();
                jugador.PostCombate(enemigo);
            }
            else
            {
                while (jugador.Vida > 0 && enemigo.Vida > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    EscribirConAnimacion("\n¿Qué deseas hacer?\n1. Atacar ⚔️\n2. Usar objeto 🧪");
                    Console.ResetColor();
                    string opcion = Console.ReadLine();
                    if (opcion == "2")
                    {
                        if (jugador.PocionesVida > 0 || jugador.PocionesAtaque > 0)
                            jugador.UsarPocion();
                        else
                            EscribirConAnimacion("❌ No tienes pociones!");
                        jugador.Atacar(enemigo);
                    }
                    else
                    {
                        jugador.Atacar(enemigo);
                    }

                    if (enemigo.Vida <= 0)
                        break;
                    enemigo.Atacar(jugador);
                }

                if (jugador.Vida <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    EscribirConAnimacion("☠️ Has caído en combate. Fin del juego.");
                    Console.ResetColor();
                    return;
                }

                jugador.PostCombate(enemigo);
            }
        }
        EscribirConAnimacion("\n🏰 ¡Has llegado al castillo del jefe final!");
        Boss jefeFinal = new Boss();
        jefeFinal.Nombre = "Darklord";
        EscribirConAnimacion("👹 Jefe final: Darklord (Nivel 20)");
        jugador.PrepararCombate();

        while (jugador.Vida > 0 && jefeFinal.Vida > 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            EscribirConAnimacion("\n¿Qué deseas hacer?\n1. Atacar ⚔️\n2. Usar objeto 🧪");
            Console.ResetColor();
            string opcion = Console.ReadLine();

            if (opcion == "2")
            {
                if (jugador.PocionesVida > 0 || jugador.PocionesAtaque > 0)
                    jugador.UsarPocion();
                else
                    EscribirConAnimacion("❌ No tienes pociones!");
                jugador.Atacar(jefeFinal);
            }
            else
                jugador.Atacar(jefeFinal);

            if (jefeFinal.Vida <= 0)
                break;
            jefeFinal.Atacar(jugador);
        }

        if (jugador.Vida <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            EscribirConAnimacion("☠️ Has sido derrotado por Darklord...");
        }
        else
        {
            jugador.PostCombate(jefeFinal);
            Console.ForegroundColor = ConsoleColor.Green;
            EscribirConAnimacion("🎉 ¡Has rescatado a la princesa y salvado el reino!");
        }
        Console.ResetColor();

        DateTime finPartida = DateTime.Now;
        TimeSpan duracion = finPartida - inicioPartida;
        EscribirConAnimacion($"\n🏆 Puntuación final: {jugador.Puntuacion}");
        EscribirConAnimacion($"⏱️ Tiempo total jugado: {duracion.Hours:D2}:{duracion.Minutes:D2}");
        EscribirConAnimacion($"🎉 ¡Bien jugado! {jugador.NombreJugador}");
        musicaProceso.Kill();

}
    public static void EscribirConAnimacion(string texto)
    {
        foreach (char c in texto)
        {
            Console.Write(c);
            Thread.Sleep(45); 
        }
        Console.WriteLine();
    }
}
