using Microsoft.EntityFrameworkCore;

public class ProgramaAquecimento
{
    public int Id { get; set; }  // Chave primária
    public string Nome { get; set; }
    public string Alimento { get; set; }
    public int Tempo { get; set; }  // Tempo em segundos
    public int Potencia { get; set; }  // Potência do micro-ondas
    public string Instrucoes { get; set; }  // Instruções adicionais (opcional)
    public string StringDeAquecimento { get; set; }  // String de aquecimento
    public bool IsCustomizado { get; set; }  // Identifica se o programa é customizado
}

public class MicroondasContext : DbContext
{
    public DbSet<ProgramaAquecimento> ProgramasAquecimento { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Substitua pela sua string de conexão
        optionsBuilder.UseSqlServer("SuaConnectionStringAqui");
    }
}
