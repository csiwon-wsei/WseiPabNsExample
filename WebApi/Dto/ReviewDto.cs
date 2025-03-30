using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApi.Dto;
public class ReviewDto
{
    [JsonProperty("movie-title")]
    public required string Title { get; set; }
    public required string Content { get; set; }
    [Range(minimum: 0, maximum: 10, ErrorMessage = "Rate must be between 1 and 10!")]
    public required uint Rate { get; set; }
}