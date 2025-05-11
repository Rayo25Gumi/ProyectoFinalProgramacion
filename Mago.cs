public class Mago : Personaje
{

    public Mago(double nivel, double habilidad)
        : base(8, nivel, habilidad, 4) { }

    public override void Atacar(Enemigo enemigo)
    {
        EscribirConAnimacion("✨ El mago lanza un hechizo destructor.");
        Random rnd = new Random();
        int dado1 = rnd.Next(1, 7);
        int dado2 = rnd.Next(1, 7);
        EscribirConAnimacion($"🎲 Dados: {dado1} y {dado2}");
        double damage = dado1 + dado2 + Ataque + (Habilidad * 0.10) ;
                NumTirada++;

        if (NumTirada >= 3)
        {
            Vida += 2;
            EscribirConAnimacion("🔮 El mago ha recuperado 2 puntos de vida.");
            NumTirada = 0;
        }
        enemigo.RecibirDaño(damage);
        

        EstadoHabilidad();
    }

    public override void MostrarEstado()
    {
        base.MostrarEstado();
        EscribirConAnimacion($"   Turnos para próxima cura: {3 - NumTirada}");
    }

        public override void  PostCombate(Enemigo enemigo){
        base.PostCombate(enemigo);
        MostrarEstado();
    }

        public void EstadoHabilidad(){
        int restante = 3 - NumTirada;
        if (restante <= 0) restante = 3;
        EscribirConAnimacion($"   Turnos restantes para la habilidad: {restante}");
        }
}
