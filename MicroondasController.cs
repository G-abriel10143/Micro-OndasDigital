using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class MicroondasController
{
    private List<ProgramaAquecimento> programas = new List<ProgramaAquecimento>
    {
        new ProgramaAquecimento { Nome = "Pipoca", Alimento = "Milho", Tempo = 180, Potencia = 7, CaractereDeAquecimento = ".", Instrucoes = "Observar o barulho de estouros do milho." },
        new ProgramaAquecimento { Nome = "Leite", Alimento = "Leite", Tempo = 120, Potencia = 5, CaractereDeAquecimento = "." },
        new ProgramaAquecimento { Nome = "Pizza", Alimento = "Pizza", Tempo = 300, Potencia = 8, CaractereDeAquecimento = "." }
    };

    private List<ProgramaAquecimento> customizados = new();
    private const string CaminhoJson = "programas_customizados.json";

    public MicroondasController()
    {
        CarregarProgramasCustomizados();
    }

    private void CarregarProgramasCustomizados()
    {
        if (File.Exists(CaminhoJson))
        {
            var json = File.ReadAllText(CaminhoJson);
            customizados = JsonConvert.DeserializeObject<List<ProgramaAquecimento>>(json) ?? new List<ProgramaAquecimento>();
        }
    }

    private void SalvarProgramasCustomizados()
    {
        var json = JsonConvert.SerializeObject(customizados, Formatting.Indented);
        File.WriteAllText(CaminhoJson, json);
    }

    public string CadastrarProgramaCustomizado(string nome, string alimento, int tempo, int potencia, string caractere, string instrucoes = "")
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(alimento) || tempo <= 0 || potencia < 1 || potencia > 10 || string.IsNullOrWhiteSpace(caractere))
        {
            return "Erro: Todos os campos obrigatórios devem ser preenchidos corretamente.";
        }

        if (caractere == "." || programas.Any(p => p.CaractereDeAquecimento == caractere) || customizados.Any(c => c.CaractereDeAquecimento == caractere))
        {
            return "Erro: O caractere de aquecimento já está em uso ou é inválido.";
        }

        var programa = new ProgramaAquecimento
        {
            Nome = nome,
            Alimento = alimento,
            Tempo = tempo,
            Potencia = potencia,
            CaractereDeAquecimento = caractere,
            Instrucoes = instrucoes,
            IsCustomizado = true
        };

        customizados.Add(programa);
        SalvarProgramasCustomizados();
        return "Programa customizado cadastrado com sucesso!";
    }

    public void ExibirProgramas()
    {
        Console.WriteLine("Programas disponíveis:");

        foreach (var programa in programas)
        {
            Console.WriteLine($"- {programa.Nome} (Tempo: {programa.Tempo / 60}:{programa.Tempo % 60:D2}, Potência: {programa.Potencia})");
        }

        foreach (var programa in customizados)
        {
            Console.WriteLine($"* {programa.Nome} (Tempo: {programa.Tempo / 60}:{programa.Tempo % 60:D2}, Potência: {programa.Potencia}) [Customizado, Itálico]");
        }
    }

    public string IniciarPrograma(string nome)
    {
        var programa = programas.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)) ??
                       customizados.FirstOrDefault(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

        if (programa == null)
        {
            return "Erro: Programa não encontrado.";
        }

        Console.WriteLine($"Programa '{programa.Nome}' iniciado para {programa.Alimento}. Tempo: {programa.Tempo / 60}:{programa.Tempo % 60:D2}, Potência: {programa.Potencia}.");
        Console.WriteLine($"Instruções: {programa.Instrucoes ?? "Sem instruções específicas."}");

        for (int i = 0; i < programa.Tempo; i++)
        {
            Console.Write(programa.CaractereDeAquecimento);
            System.Threading.Thread.Sleep(500);
        }

        return "\nAquecimento concluído.";
    }
}
