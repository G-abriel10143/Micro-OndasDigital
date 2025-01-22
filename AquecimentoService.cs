public class AquecimentoService
{
    public bool ValidarTempo(int tempo)
    {
        return tempo >= 1 && tempo <= 120;
    }

    public bool ValidarPotencia(int potencia)
    {
        return potencia >= 1 && potencia <= 10;
    }

    public string ConverterTempoParaMinutos(int segundos)
    {
        int minutos = segundos / 60;
        int segundosRestantes = segundos % 60;
        return $"{minutos}:{segundosRestantes:D2}";
    }

    public string GerarStringDeProgresso(int tempo, int potencia)
    {
        int totalPontos = tempo * potencia;
        return new string('.', totalPontos);
    }

    public void ExecutarPrograma(AquecimentoModel aquecimento)
    {
        aquecimento.EmExecucao = true;
        while (aquecimento.TempoRestante > 0)
        {
            Console.Clear();
            Console.WriteLine($"Tempo restante: {ConverterTempoParaMinutos(aquecimento.TempoRestante)}");
            Console.WriteLine($"Progresso: {GerarStringDeProgresso(aquecimento.TempoRestante, aquecimento.Potencia)}");
            aquecimento.TempoRestante--;
            System.Threading.Thread.Sleep(1000); // Simula a execução (1 segundo de pausa)
        }
        aquecimento.EmExecucao = false;
        Console.WriteLine("Aquecimento concluído!");
    }
}
