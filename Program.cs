using System;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;  // Adicionar esta referência

class Program
{
    static void Main(string[] args)
    {
        // Lê a string de conexão do arquivo de configuração
        var connectionString = ConfigurationManager.ConnectionStrings["MicroondasDigitalDb"].ConnectionString;

        var controller = new MicroondasController(connectionString);

        while (true)
    {
        Console.Clear();
        Console.WriteLine("Menu:");
        Console.WriteLine("1. Listar todos os programas");
        Console.WriteLine("2. Adicionar programa customizado");
        Console.WriteLine("3. Executar programa");
        Console.WriteLine("4. Excluir programa");
        Console.WriteLine("5. Sair");
        Console.Write("Escolha uma opção: ");
        var opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                controller.ListarProgramas();
                break;
            case "2":
                controller.AdicionarProgramaCustomizado();
                break;
            case "3":
                controller.ExecutarPrograma();
                break;
            case "4":
                controller.ExcluirPrograma();
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
}
