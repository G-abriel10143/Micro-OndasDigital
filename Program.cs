using System;
using System.Configuration;

public class Program
{
    public static void Main()
    {
        // Obter a string de conexão do arquivo de configuração
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MicroondasDigitalDb"].ConnectionString;

        var controller = new MicroondasController(connectionString);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Listar todos os programas");
            Console.WriteLine("2. Adicionar programa customizado");
            Console.WriteLine("3. Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    var programas = controller.ListarTodosProgramas();
                    Console.WriteLine("\nProgramas de Aquecimento:");
                    foreach (var programa in programas)
                    {
                        var estilo = programa.IsCustom ? " (customizado, itálico)" : "";
                        Console.WriteLine($"- {programa.Nome}{estilo}: {programa.Alimento}, Potência: {programa.Potencia}, Tempo: {programa.Tempo / 60}:{programa.Tempo % 60:D2}");
                    }
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Write("Nome do programa: ");
                    var nome = Console.ReadLine();
                    Console.Write("Alimento: ");
                    var alimento = Console.ReadLine();
                    Console.Write("Potência (1-10): ");
                    var potencia = int.Parse(Console.ReadLine());
                    Console.Write("Caractere de aquecimento: ");
                    var caractereAquecimento = Console.ReadLine();
                    Console.Write("Tempo em segundos: ");
                    var tempo = int.Parse(Console.ReadLine());
                    Console.Write("Instruções (opcional): ");
                    var instrucoes = Console.ReadLine();

                    var mensagem = controller.AdicionarProgramaCustomizado(nome, alimento, potencia, caractereAquecimento, tempo, instrucoes);
                    Console.WriteLine(mensagem);
                    Console.ReadKey();
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
