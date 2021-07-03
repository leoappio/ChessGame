using System;
using tabuleiro;
namespace jogo_xadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)        
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if(tab.getPeca(i,j) == null)
                    {
                        Console.Write(". ");
                    }
                    else
                    {
                        ImprimirPeca(tab.getPeca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                // logica para trocar a cor das peças na hora de imprimir no console
                ConsoleColor corAtual = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(peca);
                Console.ForegroundColor = corAtual;
            }
        }
    }
}
