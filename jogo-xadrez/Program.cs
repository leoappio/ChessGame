using System;
using tabuleiro;

namespace jogo_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro();

            
            Tela.ImprimirTabuleiro(tab);



            Console.ReadLine();
        }
    }
}
