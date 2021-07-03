using System;
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
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro();
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            vulneravelEnPassant = null;
            xeque = false;
            ColocarPecas();

        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = tab.retirarPeca(origem);
            peca.IncrementarQteMovimentos();

            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(peca, destino);

            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // jogada especial - roque pequeno 
            if(peca is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemDaTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoDaTorre = new Posicao(origem.linha, origem.coluna + 1);

                Peca Torre = tab.retirarPeca(origemDaTorre);
                Torre.IncrementarQteMovimentos();
                tab.colocarPeca(Torre, destinoDaTorre);

            }


            // jogada especial - roque grande 
            if (peca is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemDaTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoDaTorre = new Posicao(origem.linha, origem.coluna - 1);

                Peca Torre = tab.retirarPeca(origemDaTorre);
                Torre.IncrementarQteMovimentos();
                tab.colocarPeca(Torre, destinoDaTorre);

            }

            return pecaCapturada;
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

        private Cor adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }

            return Cor.Branca;
        }

        private Peca rei (Cor cor)
        {
            foreach(Peca x in pecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }

            return null;

        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            foreach(Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();

                if(mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);

        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new ExcecoesTabuleiro("Você não pode se colocar em xeque");
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();

            if(pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            tab.colocarPeca(p, origem);

            // jogada especial - roque pequeno 
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemDaTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoDaTorre = new Posicao(origem.linha, origem.coluna + 1);

                Peca Torre = tab.retirarPeca(destinoDaTorre);
                Torre.decrementarQteMovimentos();
                tab.colocarPeca(Torre, origemDaTorre);

            }

            // jogada especial - roque grande 
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemDaTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoDaTorre = new Posicao(origem.linha, origem.coluna - 1);

                Peca Torre = tab.retirarPeca(destinoDaTorre);
                Torre.decrementarQteMovimentos();
                tab.colocarPeca(Torre, origemDaTorre);

            }


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
            if (!tab.getPeca(origem).movimentoPossivel(destino))
            {
                throw new ExcecoesTabuleiro("Posição inválida, tente novamente");
            }
        }

        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }

            foreach(Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();

                for(int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if(mat[i,j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
        private void ColocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));

        }

    }
}
