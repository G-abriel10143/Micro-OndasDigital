using System;

class Program
{
    static void Main()
    {
        var controller = new MicroondasController();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Exibir programas");
            Console.WriteLine("2. Iniciar um programa");
            Console.WriteLine("3. Cadastrar programa customizado");
            Console.WriteLine("4. Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    controller.ExibirProgramas();
                    break;

                case "2":
                    Console.Write("Digite o nome do programa desejado: ");
                    var nome = Console.ReadLine();
                    Console.WriteLine(controller.IniciarPrograma(nome));
                    break;

                case "3":
                    Console.Write("Nome do programa: ");
                    var nomePrograma = Console.ReadLine();
                    Console.Write("Alimento: ");
                    var alimento = Console.ReadLine();
                    Console.Write("Tempo (em segundos): ");
                    if (!int.TryParse(Console.ReadLine(), out var tempo)) tempo = 0;
                    Console.Write("Potência (1-10): ");
                    if (!int.TryParse(Console.ReadLine(), out var potencia)) potencia = 0;
                    Console.Write("Caractere de aquecimento: ");
                    var caractere = Console.ReadLine();
                    Console.Write("Instruções (opcional): ");
                    var instrucoes = Console.ReadLine();

                    Console.WriteLine(controller.CadastrarProgramaCustomizado(nomePrograma, alimento, tempo, potencia, caractere, instrucoes));
                    break;

                case "4":
                    Console.WriteLine("Encerrando...");
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
