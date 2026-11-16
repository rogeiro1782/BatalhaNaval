using System.ComponentModel.Design;
using System.Numerics;

int qtdlinhas = 10;
int qtdcolunas = 10;

Console.WriteLine("*******BATALHA NAVAL********");
Console.WriteLine();

char[,] tabuleirojogador = new char[qtdlinhas, qtdcolunas];
char[,] tabuleirocomputador = new char[qtdlinhas, qtdcolunas];

void InicializarTabuleiro(char[,] tabuleiro)
{

    for (int linha = 0; linha < qtdlinhas; linha++)
    {
        for (int coluna = 0; coluna < qtdcolunas; coluna++)
        {
            tabuleiro[linha, coluna] = '~';
        }
    }
}

bool Navios(char[,] tabuleiro, int linhainicial, int colunainicial, int tamanho, bool horizontal)
{

    if (horizontal)
    {
        if (colunainicial + tamanho > qtdcolunas)
        {
            //Console.WriteLine("Ultrapassou o tamanho do tabuleiro");
            return false;
        }
        else
        {
            for (int col = colunainicial; col < colunainicial + tamanho; col++)

                tabuleiro[linhainicial, col] = 'N';

        }
    }

    else
    {

        if (linhainicial + tamanho > qtdlinhas)
        {
            //Console.WriteLine("Ultrapassou o tamanho do tabuleiro");
            return false;
        }
        else
        {
            for (int lin = linhainicial; lin < linhainicial + tamanho; lin++)

                tabuleiro[lin, colunainicial] = 'N';
        }

    }
    return true;

}

void NaviosComputador(char[,] tabuleiro)
{
    Random navio = new Random();
    int tamanho = 3;
    int posicaonavios = 0;

    while (posicaonavios < 3)
    {
        int linhaaleatoria = navio.Next(10);
        int colunaaleatoria = navio.Next(10);
        bool horizontal = navio.Next(2) == 1;

        bool n1 = Navios(tabuleiro, linhaaleatoria, colunaaleatoria, tamanho, horizontal);

        if (n1)
        {
            posicaonavios++;
        }
    }
}

void ExibirTabuleiro(char[,] tabuleiro)
{
    Console.Write("  ");
    for (int i = 0; i < qtdcolunas; i++)
    {
        Console.Write(i + " ");
    }
    Console.WriteLine();

    for (int linha = 0; linha < qtdlinhas; linha++)
    {
        Console.Write(linha + " ");
        Console.Write("");

        for (int coluna = 0; coluna < qtdcolunas; coluna++)
        {
            Console.Write(tabuleiro[linha, coluna] + " ");
        }

        Console.WriteLine();
    }
    Console.WriteLine();
}

void JogaJogador(char[,] tabuleiro)
{
    int linha = 0;
    int coluna = 0;
    Console.Write("Digite a linha da jogada: ");
    linha = int.Parse(Console.ReadLine()!);
    Console.Write("Digite a coluna da jogada: ");
    coluna = int.Parse(Console.ReadLine()!);
    Console.WriteLine();

    if (linha < 0 || linha >= qtdlinhas || coluna < 0 || coluna >= qtdcolunas)
    {
        Console.WriteLine("Coordenada inválida!");
        return;
    }

    if (tabuleiro[linha, coluna] == 'X' || tabuleiro[linha, coluna] == 'O')
    {
        Console.WriteLine("Ja teve ataque nessa área!");
        return;
    }

    if (tabuleiro[linha, coluna] == 'N')
    {
        Console.WriteLine("Jogador Acertou um navio!");
        tabuleiro[linha, coluna] = 'X';
    }
    else
    {
        Console.WriteLine("Jogador Errouuu!");
        tabuleiro[linha, coluna] = 'O';
    }
    Console.WriteLine();
}

void JogaComputador(char[,] tabuleiro)
{
    Random ataque = new Random();

    int linhaAleatoria;
    int colunaAleatoria;

    do
    {
        linhaAleatoria = ataque.Next(10);
        colunaAleatoria = ataque.Next(10);
    }
    while (tabuleiro[linhaAleatoria, colunaAleatoria] == 'X' ||
            tabuleiro[linhaAleatoria, colunaAleatoria] == 'O');

    if (tabuleiro[linhaAleatoria, colunaAleatoria] == 'N')
    {
        tabuleiro[linhaAleatoria, colunaAleatoria] = 'X';
        Console.WriteLine("Inimigo Acertou seu navio!");
    }
    else
    {
        tabuleiro[linhaAleatoria, colunaAleatoria] = 'O';
        Console.WriteLine("Inimigo Errouuu!");
    }
    Console.WriteLine();
}

bool Naviosinteiros(char[,] tabuleiro)
{
    for (int linha = 0; linha < qtdlinhas; linha++)
    {
        for (int coluna = 0; coluna < qtdcolunas; coluna++)
        {
            if (tabuleiro[linha, coluna] == 'N')
            {
                return true;
            }
        }
    }

    return false;
}

InicializarTabuleiro(tabuleirocomputador);
InicializarTabuleiro(tabuleirojogador);
Navios(tabuleirojogador, 2, 5, 3, true);    // Linha, Coluna e Tamnho. Navio horizontal
Navios(tabuleirojogador, 1, 3, 3, false);  // Linha, Coluna e Tamnho. Navio Vertical
Navios(tabuleirojogador, 6, 1, 3, true);   // Linha, Coluna e Tamnho. Navio Vertical
NaviosComputador(tabuleirocomputador);

while (Naviosinteiros(tabuleirojogador) &&
       Naviosinteiros(tabuleirocomputador))
{
    JogaJogador(tabuleirocomputador);
    JogaComputador(tabuleirojogador);

    ExibirTabuleiro(tabuleirojogador);
}

