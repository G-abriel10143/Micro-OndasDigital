public class MicroondasController
{
    private readonly ProgramaAquecimentoRepository _repository;

    public MicroondasController(string connectionString)
    {
        _repository = new ProgramaAquecimentoRepository(connectionString);
    }

    public string AdicionarProgramaCustomizado(string nome, string alimento, int potencia, string caractereAquecimento, int tempo, string instrucoes = null)
    {
        if (potencia < 1 || potencia > 10)
            return "Erro: A potência deve estar entre 1 e 10.";

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(alimento) || string.IsNullOrEmpty(caractereAquecimento))
            return "Erro: Nome, Alimento e Caractere de Aquecimento são obrigatórios.";

        if (caractereAquecimento == ".")
            return "Erro: O caractere '.' é reservado e não pode ser usado.";

        var programasExistentes = _repository.ObterTodosProgramas();
        if (programasExistentes.Exists(p => p.CaractereAquecimento == caractereAquecimento))
            return $"Erro: O caractere '{caractereAquecimento}' já está em uso.";

        var programa = new ProgramaAquecimento
        {
            Nome = nome,
            Alimento = alimento,
            Potencia = potencia,
            CaractereAquecimento = caractereAquecimento,
            Tempo = tempo,
            Instrucoes = instrucoes,
            IsCustom = true
        };

        _repository.AdicionarPrograma(programa);
        return $"Programa customizado '{nome}' adicionado com sucesso.";
    }

    public List<ProgramaAquecimento> ListarTodosProgramas()
    {
        return _repository.ObterTodosProgramas();
    }
}
