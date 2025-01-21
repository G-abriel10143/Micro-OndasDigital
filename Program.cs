using System;

public class Program
{
    public static void Main()
    {
        var controller = new MicroondasController();

        Console.WriteLine("Escolha o programa de aquecimento:");
        Console.WriteLine("1. Pipoca");
        Console.WriteLine("2. Leite");
        Console.WriteLine("3. Carnes de boi");
        Console.WriteLine("4. Frango");
        Console.WriteLine("5. Feijão");
        Console.Write("Digite o número do programa desejado: ");
        var escolha = Console.ReadLine();

        string nomePrograma = escolha switch
        {
            "1" => "Pipoca",
            "2" => "Leite",
            "3" => "Carnes de boi",
            "4" => "Frango",
            "5" => "Feijão",
            _ => null
        };

        if (nomePrograma != null)
        {
            Console.WriteLine(controller.IniciarPrograma(nomePrograma));
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }

        // Atualizando o progresso
        for (int i = 0; i < 30; i++)
        {
            Console.WriteLine(controller.AtualizarProgresso());
        }

        // Pausar ou cancelar
        controller.PausarOuCancelar();
        Console.WriteLine("Aquecimento pausado.");
        Console.ReadLine(); // Para manter a janela aberta
    }
}
