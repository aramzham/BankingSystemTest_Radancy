namespace Radancy.Api.Models;

public record ErrorResponse(int StatusCode, string TraceId, string Message);

public record UserResponseModel(string Id);

public record AccountResponseModel(string Id, string UserId, decimal Balance);