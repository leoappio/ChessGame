using System;
using tabuleiro;
namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro();
            turno = 1;
            jogadorAtual = Cor.Branca;
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
    }
}
