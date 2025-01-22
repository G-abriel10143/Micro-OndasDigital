using System;
using Microsoft.Data.SqlClient;

public class MicroondasController
{
    private string connectionString;

    public MicroondasController(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Método para listar todos os programas
    public void ListarProgramas()
{
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        var query = "SELECT Id, Nome, Alimento, Potencia, Tempo, Instrucoes FROM ProgramasAquecimentoCustomizados";
        var command = new SqlCommand(query, connection);

        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string nome = reader.GetString(reader.GetOrdinal("Nome"));
                string alimento = reader.GetString(reader.GetOrdinal("Alimento"));
                int potencia = reader.GetInt32(reader.GetOrdinal("Potencia"));
                int tempo = reader.GetInt32(reader.GetOrdinal("Tempo"));
                string instrucoes = reader.IsDBNull(reader.GetOrdinal("Instrucoes")) ? null : reader.GetString(reader.GetOrdinal("Instrucoes"));

                Console.WriteLine($"Id: {id}, Nome: {nome}, Alimento: {alimento}, Potência: {potencia}, Tempo: {tempo}, Instruções: {instrucoes}");
            }
        }
    }
}

    // Método para adicionar um programa customizado
    public void AdicionarProgramaCustomizado()
{
    Console.WriteLine("Digite o nome do programa:");
    string nome = Console.ReadLine();

    Console.WriteLine("Digite o alimento do programa:");
    string alimento = Console.ReadLine();

    Console.WriteLine("Digite a potência (em número inteiro):");
    int potencia = int.Parse(Console.ReadLine());

    Console.WriteLine("Digite o tempo de aquecimento (em segundos):");
    int tempo = int.Parse(Console.ReadLine());

    Console.WriteLine("Digite as instruções (opcional, pressione Enter se não houver):");
    string instrucoes = Console.ReadLine();

    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        var query = @"
            INSERT INTO ProgramasAquecimentoCustomizados (Nome, Alimento, Potencia, Tempo, Instrucoes)
            VALUES (@Nome, @Alimento, @Potencia, @Tempo, @Instrucoes)";
        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Nome", nome);
        command.Parameters.AddWithValue("@Alimento", alimento);
        command.Parameters.AddWithValue("@Potencia", potencia);
        command.Parameters.AddWithValue("@Tempo", tempo);
        command.Parameters.AddWithValue("@Instrucoes", string.IsNullOrEmpty(instrucoes) ? (object)DBNull.Value : instrucoes);

        command.ExecuteNonQuery();

        Console.WriteLine("Programa customizado adicionado com sucesso!");
    }
}
public void ExecutarPrograma()
{
    // Conectar ao banco e buscar os programas
    Console.Clear();
    Console.WriteLine("Programas disponíveis:");

    // Listar os programas do banco de dados
    List<Programa> programasDisponiveis = new List<Programa>();

    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        var query = "SELECT Id, Nome, Alimento FROM ProgramasAquecimentoCustomizados";
        var command = new SqlCommand(query, connection);

        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var programa = new Programa
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                    Alimento = reader.GetString(reader.GetOrdinal("Alimento"))
                };

                programasDisponiveis.Add(programa);
            }
        }
    }

    if (programasDisponiveis.Count == 0)
    {
        Console.WriteLine("Nenhum programa disponível.");
        return;
    }

    // Exibe os programas e pede para o usuário escolher
    for (int i = 0; i < programasDisponiveis.Count; i++)
    {
        Console.WriteLine($"{programasDisponiveis[i].Id}. {programasDisponiveis[i].Nome} - {programasDisponiveis[i].Alimento}");
    }

    Console.WriteLine("Digite o ID do programa que deseja executar:");
    int idPrograma;

    // Verificar se o ID digitado é válido
    if (!int.TryParse(Console.ReadLine(), out idPrograma))
    {
        Console.WriteLine("ID inválido.");
        return;
    }

    // Verificar se o programa existe
    var programaSelecionado = programasDisponiveis.FirstOrDefault(p => p.Id == idPrograma);

    if (programaSelecionado == null)
    {
        Console.WriteLine("Programa não encontrado.");
        return;
    }

    // Exibe as informações do programa selecionado
    Console.Clear();
    Console.WriteLine($"Executando o programa {programaSelecionado.Nome}...");
    Console.WriteLine($"Alimento: {programaSelecionado.Alimento}");

    // Exibe as informações necessárias para executar o programa (potência, tempo, etc.)
    int potencia = 3;  // Pode ser ajustado conforme a lógica que você deseja (exemplo de valor fixo)
    int tempo = 10;    // Valor fixo para o tempo, pode ser ajustado conforme necessário
    string instrucoes = "Deixe sem tampa"; // As instruções podem ser recuperadas do banco, se necessário

    Console.WriteLine($"Potência: {potencia}W");
    Console.WriteLine($"Tempo de aquecimento: {tempo} segundos");
    Console.WriteLine($"Instruções: {instrucoes}");

    // Definir a quantidade de pontos por segundo com base na potência
    int pontosPorSegundo = potencia; // 1 ponto por segundo para potência 1, 2 pontos para potência 2, etc.

    string progresso = "";
    DateTime tempoFinal = DateTime.Now.AddSeconds(tempo);

    while (DateTime.Now < tempoFinal)
    {
        // Adiciona os pontos conforme a potência
        progresso += new string('.', pontosPorSegundo);

        // Exibe a string de progresso na tela
        Console.Clear();
        Console.WriteLine($"Aquecendo: {progresso}"); // Exibe a string de progresso
        Console.WriteLine($"Tempo restante: {tempoFinal.Subtract(DateTime.Now).Seconds} segundos");

        // Aguardar 1 segundo antes de atualizar
        System.Threading.Thread.Sleep(1000);
    }

    // Exibe a mensagem final de conclusão
    Console.Clear();
    Console.WriteLine($"Aquecendo: {progresso}");
    Console.WriteLine("Aquecimento concluído.");
}
public void ExcluirPrograma()
{
    Console.Clear();
    Console.WriteLine("Programas disponíveis para exclusão:");

    // Listar os programas do banco de dados
    List<Programa> programasDisponiveis = new List<Programa>();

    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        var query = "SELECT Id, Nome, Alimento FROM ProgramasAquecimentoCustomizados";
        var command = new SqlCommand(query, connection);

        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var programa = new Programa
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                    Alimento = reader.GetString(reader.GetOrdinal("Alimento"))
                };

                programasDisponiveis.Add(programa);
            }
        }
    }

    if (programasDisponiveis.Count == 0)
    {
        Console.WriteLine("Nenhum programa disponível.");
        return;
    }

    // Exibe os programas e pede para o usuário escolher
    for (int i = 0; i < programasDisponiveis.Count; i++)
    {
        Console.WriteLine($"{programasDisponiveis[i].Id}. {programasDisponiveis[i].Nome} - {programasDisponiveis[i].Alimento}");
    }

    Console.WriteLine("Digite o ID do programa que deseja excluir:");
    int idPrograma;

    // Verificar se o ID digitado é válido
    if (!int.TryParse(Console.ReadLine(), out idPrograma))
    {
        Console.WriteLine("ID inválido.");
        return;
    }

    // Verificar se o programa existe
    var programaSelecionado = programasDisponiveis.FirstOrDefault(p => p.Id == idPrograma);

    if (programaSelecionado == null)
    {
        Console.WriteLine("Programa não encontrado.");
        return;
    }

    // Confirmar a exclusão
    Console.WriteLine($"Você tem certeza que deseja excluir o programa {programaSelecionado.Nome}?");
    Console.WriteLine("Digite 'sim' para confirmar ou qualquer outra tecla para cancelar.");
    var resposta = Console.ReadLine();

    if (resposta?.ToLower() != "sim")
    {
        Console.WriteLine("Exclusão cancelada.");
        return;
    }

    // Exclui o programa do banco de dados
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        var query = "DELETE FROM ProgramasAquecimentoCustomizados WHERE Id = @Id";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", idPrograma);

        int linhasAfetadas = command.ExecuteNonQuery();
        
        if (linhasAfetadas > 0)
        {
            Console.WriteLine($"Programa {programaSelecionado.Nome} excluído com sucesso.");
        }
        else
        {
            Console.WriteLine("Erro ao excluir o programa.");
        }
    }
}
// Classe Programa para armazenar os dados
public class Programa
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Alimento { get; set; }
}


}
