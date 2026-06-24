using MediatR;
using CollaborationService.Application.Commands.AddCollaborator;
using CollaborationService.Application.Interfaces;
using CollaborationService.Infrastructure.Context;
using CollaborationService.Infrastructure.Repositories;
using CollaborationService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CollaborationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<
    ICollaboratorRepository,
    CollaboratorRepository>();

builder.Services.AddHttpClient<
    IUserServiceClient,
    UserServiceClient>(client =>
    {
        client.BaseAddress =
            new Uri("https://localhost:7230/");
    });

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(AddCollaboratorCommand).Assembly);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();