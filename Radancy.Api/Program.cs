using Microsoft.EntityFrameworkCore;
using Radancy.Api.Data;
using Radancy.Api.Models;
using Radancy.Api.Repositories;
using Radancy.Api.Repositories.Contracts;
using Radancy.Api.Services;
using Radancy.Api.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// db context
builder.Services.AddDbContext<RadancyDbContext>(options => options.UseInMemoryDatabase("RadancyInMemoryDatabase"));
// repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
// services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAccountService, AccountService>();
// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var accountGroup = app.MapGroup("/account");
var userGroup = app.MapGroup("/user");

userGroup.MapPost("/", async ( /*UserCreateRequestModel*/IUserService userService) =>
{
    var user = await userService.Create();
    return Results.Ok(user); // we wouldn't like to expose the id to the user, otherwise map to a response object
});

accountGroup.MapPost("/", async (CreateAccountRequestModel requestModel, IAccountService service) =>
{
    var account = await service.Create(requestModel.UserId);
    return Results.Ok(account);
});

accountGroup.MapPost("/withdraw", async (WithdrawRequestModel requestModel, IAccountService service) =>
{
    var result = await service.Withdraw(requestModel.AccountId, requestModel.Amount);
    return Results.Ok(result);
});

accountGroup.MapPost("/deposit", async (DepositRequestModel requestModel, IAccountService service) =>
{
    var result = await service.Deposit(requestModel.AccountId, requestModel.Amount);
    return Results.Ok(result);
});

app.Run();