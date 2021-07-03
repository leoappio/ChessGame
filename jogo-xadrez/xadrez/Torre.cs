using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "T";
        }

        private bool podeMover(Posicao posicao)
        {
            Peca peca = tab.getPeca(posicao);
            return peca == null || peca.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[8, 8];

            Posicao pos = new Posicao(0, 0);

            // verifica acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if(tab.getPeca(pos) != null && tab.getPeca(pos).cor != this.cor)
                {
                    break;
                }

                pos.linha -= 1;
            }

            // verifica abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != this.cor)
                {
                    break;
                }

                pos.linha += 1;
            }

            // verifica direita
            pos.definirValores(posicao.linha, posicao.coluna+1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != this.cor)
                {
                    break;
                }

                pos.coluna += 1;
            }

            // verifica esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != this.cor)
                {
                    break;
                }

                pos.coluna -= 1;
            }



            return matriz;




        }
    }
}