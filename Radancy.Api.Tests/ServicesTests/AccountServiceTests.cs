using Bogus;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Radancy.Api.Data;
using Radancy.Api.Repositories.Contracts;
using Radancy.Api.Services;
using Radancy.Api.Services.Contracts;

namespace Radancy.Api.Tests.ServicesTests;

public class AccountServiceTests
{
    private readonly IAccountService _sut;
    private readonly IAccountRepository _accountRepositoryMock;

    public AccountServiceTests()
    {
        _accountRepositoryMock = Substitute.For<IAccountRepository>();
        _sut = new AccountService(_accountRepositoryMock);
    }

    [Fact]
    public async Task Create_Should_Return_User()
    {
        // arrange
        var id = new Faker().Random.Guid();
        _accountRepositoryMock.Create(Arg.Any<Guid>()).Returns(new Account { Id = id });

        // act
        var result = await _sut.Create(default);

        // assert
        result.Should().NotBeNull();
        result.Id.Should().Be(id);
        _accountRepositoryMock.Received(1);
    }

    [Fact]
    public async Task Withdraw_WhenAccountNotFound_ThrowsException()
    {
        // arrange
        _accountRepositoryMock.Get(Arg.Any<Guid>()).ReturnsNull();

        // act
        var act = () => _sut.Withdraw(default, default);

        // assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Account not found.");
        _accountRepositoryMock.Received(1);
    }

    [Fact]
    public async Task Withdraw_WhenAmountIsNegative_ThrowsException()
    {
        // arrange
        var account = new Faker<Account>().Generate();
        var invalidAmount = new Faker().Random.Decimal(int.MinValue, 0);
        _accountRepositoryMock.Get(Arg.Any<Guid>()).Returns(account);

        // act
        var act = () => _sut.Withdraw(default, invalidAmount);

        // arrange
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Withdrawal amount cannot be less than 0.");
        _accountRepositoryMock.Received(1);
    }

    [Fact]
    public async Task Withdraw_WhenMoreThan90PercentIsRequested_ThrowsException()
    {
        // arrange
        var account = new Faker<Account>().Generate();
        var invalidAmount = new Faker().Random.Decimal(account.Balance * 0.9m);
        _accountRepositoryMock.Get(Arg.Any<Guid>()).Returns(account);

        // act
        var act = () => _sut.Withdraw(default, invalidAmount);

        // assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Withdrawal amount is greater than 90% of the account balance.");
        _accountRepositoryMock.Received(1);
    }

    [Fact]
    public async Task Withdraw_WhenAmountIsValid_ReturnsAccount()
    {
        // arrange
        var initialBalance = new Faker().Random.Decimal(1000, 2000);
        var account = new Faker<Account>()
            .RuleFor(a => a.Balance, _ => initialBalance)
            .Generate();
        var amount = new Faker().Random.Decimal(0, account.Balance / 2);
        _accountRepositoryMock.Get(Arg.Any<Guid>()).Returns(account);

        // act
        var result = await _sut.Withdraw(default, amount);

        // assert
        result.Should().NotBeNull();
        result.Id.Should().Be(account.Id);
        result.UserId.Should().Be(account.UserId);
        result.Balance.Should().Be(initialBalance - amount);
        _accountRepositoryMock.Received(1);
    }
}