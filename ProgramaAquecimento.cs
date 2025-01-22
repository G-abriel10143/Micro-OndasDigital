public class ProgramaAquecimento
{
    public string Nome { get; set; }
    public string Alimento { get; set; }
    public int Tempo { get; set; }
    public int Potencia { get; set; }
    public string CaractereDeAquecimento { get; set; } // Caractere usado no progresso
    public string Instrucoes { get; set; }
    public bool IsCustomizado { get; set; } // Diferencia customizados de pré-definidos
}
