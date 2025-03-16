using ApplicationCore.Application.Commons;

namespace ApplicationCore.Domain.Models;

public sealed class Movie: BaseIdentity
{
    public string Title { get; set; }
    public string Descritpion { get; set; }
    public List<Review> Reviews { get; set; } = new();

}