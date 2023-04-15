namespace Radancy.Api.Models;

public record CreateAccountRequestModel(Guid UserId);

public record WithdrawRequestModel(Guid AccountId, decimal Amount);

public record DepositRequestModel(Guid AccountId, decimal Amount);