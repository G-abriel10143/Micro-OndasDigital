public class MicroondasController
{
    private readonly AquecimentoService _service = new();
    private AquecimentoModel _model = new();

    private List<ProgramaAquecimento> programas = new List<ProgramaAquecimento>
    {
        new ProgramaAquecimento
        {
            Nome = "Pipoca",
            Alimento = "Pipoca (de micro-ondas)",
            Tempo = 180,  // 3 minutos
            Potencia = 7,
            Instrucoes = "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento.",
            StringDeAquecimento = new string('.', 180 * 7) // Um exemplo de string de progresso
        },
        new ProgramaAquecimento
        {
            Nome = "Leite",
            Alimento = "Leite",
            Tempo = 300,  // 5 minutos
            Potencia = 5,
            Instrucoes = "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras.",
            StringDeAquecimento = new string('.', 300 * 5)
        },
        new ProgramaAquecimento
        {
            Nome = "Carnes de boi",
            Alimento = "Carne em pedaço ou fatias",
            Tempo = 840,  // 14 minutos
            Potencia = 4,
            Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.",
            StringDeAquecimento = new string('.', 840 * 4)
        },
        new ProgramaAquecimento
        {
            Nome = "Frango",
            Alimento = "Frango (qualquer corte)",
            Tempo = 480,  // 8 minutos
            Potencia = 7,
            Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.",
            StringDeAquecimento = new string('.', 480 * 7)
        },
        new ProgramaAquecimento
        {
            Nome = "Feijão",
            Alimento = "Feijão congelado",
            Tempo = 480,  // 8 minutos
            Potencia = 9,
            Instrucoes = "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas.",
            StringDeAquecimento = new string('.', 480 * 9)
        }
    };

    public string IniciarPrograma(string nomePrograma)
    {
        var programa = programas.FirstOrDefault(p => p.Nome.Equals(nomePrograma, StringComparison.OrdinalIgnoreCase));

        if (programa == null)
        {
            return "Erro: Programa de aquecimento não encontrado.";
        }

        _model.TempoRestante = programa.Tempo;
        _model.Potencia = programa.Potencia;
        _model.EmExecucao = true;

        return $"Programa '{programa.Nome}' iniciado para {programa.Alimento}. Tempo: {programa.Tempo / 60}:{programa.Tempo % 60:D2}, Potência: {programa.Potencia}.\nInstruções: {programa.Instrucoes}";
    }

    public string AtualizarProgresso()
    {
        if (_model.EmExecucao)
        {
            _model.TempoRestante--;
            if (_model.TempoRestante <= 0)
            {
                _model.EmExecucao = false;
                return "Aquecimento concluído.";
            }

            return _service.GerarStringDeProgresso(_model.TempoRestante, _model.Potencia);
        }

        return "Nenhum aquecimento em execução.";
    }

    public void PausarOuCancelar()
    {
        if (_model.EmExecucao)
        {
            _model.Pausado = !_model.Pausado;
            _model.EmExecucao = !_model.Pausado;
        }
        else
        {
            _model = new AquecimentoModel();
        }
    }
}
