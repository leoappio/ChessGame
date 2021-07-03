using System.Collections.Generic;
using tabuleiro;
namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }

        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro();
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();

        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = tab.retirarPeca(origem);
            peca.IncrementarQteMovimentos();

            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(peca, destino);

            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> capturadasCor = new HashSet<Peca>();
            foreach( Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    capturadasCor.Add(x);
                }
            }

            return capturadasCor;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> pecasNoJogo = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    pecasNoJogo.Add(x);
                }
            }
            pecasNoJogo.ExceptWith(pecasCapturadas(cor));

            return pecasNoJogo;

        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);

        }

        private void ColocarPecas()
        {
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));

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
