using ApplicationCore.Application.Commons;

namespace ApplicationCore.Domain.Models;

public class User: BaseIdentity
{
    public string Username { get; set; }
}