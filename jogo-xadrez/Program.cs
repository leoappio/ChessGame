using System;
using tabuleiro;
using xadrez;

namespace jogo_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {

            PartidaDeXadrez partida = new PartidaDeXadrez();
            
            Tela.ImprimirTabuleiro(partida.tab);



            Console.ReadLine();
        }
    }
}
