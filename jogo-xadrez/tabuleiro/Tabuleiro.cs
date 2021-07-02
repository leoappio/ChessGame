
namespace tabuleiro
{
    class Tabuleiro
    {

        public int linhas = 8;
        public int colunas = 8;
        private Peca[,] pecas;

        public Tabuleiro()
        {
            pecas = new Peca[8, 8];

        }

        public Peca getPeca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }
    }
}
