using System;
using tabuleiro;
using xadrez;
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
                    ImprimirPeca(tab.getPeca(i, j));
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {

            ConsoleColor fundoPreto = Console.BackgroundColor;
            ConsoleColor fundoAlerado = ConsoleColor.Green;


            for (int i = 0; i < tab.linhas; i++)
            {
                Console.BackgroundColor = fundoPreto;
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlerado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoPreto;
                    }

                    ImprimirPeca(tab.getPeca(i, j));
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = fundoPreto;
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca == null)
            {
                Console.Write(". ");
            }
            else
            {

                if (peca.cor == Cor.Branca)
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
                Console.Write(" ");

            }


        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }
    }
}
