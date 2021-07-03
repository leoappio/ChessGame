using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        public override string ToString()
        {
            return "" + coluna + linha;
        }

        public Posicao toPosicao()
        {
            int novaLinha = 8 - linha;
            int novaColuna = coluna - 'a';

            return new Posicao(novaLinha, novaColuna);
        }
    }
}
