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
}
