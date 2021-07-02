
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

        public Peca getPeca(Posicao posicao)
        {
            return pecas[posicao.linha, posicao.coluna];
        }

        public void colocarPeca (Peca peca, Posicao posicao)
        {
            if (existePeca(posicao))
            {
                throw new ExcecoesTabuleiro("Posição já esta ocupada por uma peça!");
            }

            pecas[posicao.linha, posicao.coluna] = peca;

            peca.posicao = posicao;
        }

        public bool posicaoValida(Posicao posicao)
        {
            if(posicao.linha < 0 || posicao.coluna < 0 || posicao.linha >= linhas || posicao.coluna >= colunas)
            {
                return false;
            }

            return true;
        }

        public void validarPosicao(Posicao posicao)
        {
            if (!posicaoValida(posicao))
            {
                throw new ExcecoesTabuleiro("Posição Inválida!");
            }
        }

        public bool existePeca(Posicao posicao)
        {
            validarPosicao(posicao);
            return getPeca(posicao) != null;

        }
    }
}
