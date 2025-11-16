namespace MonitoramentoEscolarAPI.DTOs
{
    public record AlunoCreateDto(string nome, Guid responsavelId, double? latitude, double? longitude);
    public record AlunoUpdateDto(Guid id, string nome, Guid responsavelId, double? latitude, double? longitude);
    public record AlunoResponseDto(Guid id, string nome, Guid responsavelId, double? latitude, double? longitude);

    
}
