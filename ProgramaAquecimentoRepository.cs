using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; 

public class ProgramaAquecimentoRepository
{
    private readonly string _connectionString;

    public ProgramaAquecimentoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AdicionarPrograma(ProgramaAquecimento programa)
    {
        const string query = @"
            INSERT INTO ProgramasAquecimento (Nome, Alimento, Potencia, CaractereAquecimento, Tempo, Instrucoes, IsCustom)
            VALUES (@Nome, @Alimento, @Potencia, @CaractereAquecimento, @Tempo, @Instrucoes, @IsCustom)";

        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Nome", programa.Nome);
            command.Parameters.AddWithValue("@Alimento", programa.Alimento);
            command.Parameters.AddWithValue("@Potencia", programa.Potencia);
            command.Parameters.AddWithValue("@CaractereAquecimento", programa.CaractereAquecimento);
            command.Parameters.AddWithValue("@Tempo", programa.Tempo);
            command.Parameters.AddWithValue("@Instrucoes", (object)programa.Instrucoes ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsCustom", programa.IsCustom);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public List<ProgramaAquecimento> ObterTodosProgramas()
    {
        const string query = "SELECT * FROM ProgramasAquecimento";
        var programas = new List<ProgramaAquecimento>();

        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(query, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    programas.Add(new ProgramaAquecimento
                    {
                        Nome = reader["Nome"].ToString(),
                        Alimento = reader["Alimento"].ToString(),
                        Potencia = Convert.ToInt32(reader["Potencia"]),
                        CaractereAquecimento = reader["CaractereAquecimento"].ToString(),
                        Tempo = Convert.ToInt32(reader["Tempo"]),
                        Instrucoes = reader["Instrucoes"]?.ToString(),
                        IsCustom = Convert.ToBoolean(reader["IsCustom"])
                    });
                }
            }
        }

        return programas;
    }
}
