namespace CompetencyAssessment.Shared.DTOs
{
    public record AuthenticationOptionsDto(string ClientId, string Authority, bool ValidateAuthority);
}