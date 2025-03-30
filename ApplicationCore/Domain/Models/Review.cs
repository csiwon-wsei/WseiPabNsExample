using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.ValueObjects;

namespace ApplicationCore.Domain.Models;

public sealed class Review: BaseIdentity
{
    public Guid MovieId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Rate Rate { get; set; }
}