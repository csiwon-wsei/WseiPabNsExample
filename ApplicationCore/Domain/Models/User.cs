using ApplicationCore.Application.Commons;

namespace ApplicationCore.Domain.Models;

public class User: BaseIdentity
{
    public required string Username { get; set; }
}