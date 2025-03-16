using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.ValueObjects;

namespace ApplicationCore.Domain.Models;

public sealed class Review: BaseIdentity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public Rate Rate { get; set; }
    public Guid MovieId { get; set; }
}