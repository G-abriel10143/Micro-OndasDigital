public class ProgramaAquecimento
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Alimento { get; set; }
    public int Potencia { get; set; }
    public string CaractereAquecimento { get; set; }
    public int Tempo { get; set; } // Em segundos ou minutos
    public string? Instrucoes { get; set; }
    public bool IsCustom { get; set; }
}
