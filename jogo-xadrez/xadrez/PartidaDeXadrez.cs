using System;
using tabuleiro;
namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }

        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro();
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            ColocarPecas();

        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = tab.retirarPeca(origem);
            peca.IncrementarQteMovimentos();

            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(peca, destino);
        }

        private void ColocarPecas()
        {
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('a', 1).toPosicao());
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }

        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.getPeca(pos) == null)
            {
                throw new ExcecoesTabuleiro("Posição vazia");
            }
            if (jogadorAtual != tab.getPeca(pos).cor)
            {
                throw new ExcecoesTabuleiro("Peça não é da cor "+ jogadorAtual);
            }
            if (!tab.getPeca(pos).existeMovimentosPossiveis())
            {
                throw new ExcecoesTabuleiro("Não existe movimentos possíveis para a peça");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.getPeca(origem).podeMoverPara(destino))
            {
                throw new ExcecoesTabuleiro("Posição inválida, tente novamente");
            }
        }
    }
}
