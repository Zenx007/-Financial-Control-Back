using FamilyFinancialControl.API.Extensions;
using FamilyFinancialControl.Communication.ViewObjects.API;
using FamilyFinancialControl.Core.Profiles;
using FamilyFinancialControl.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        string message = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(e => e.ErrorMessage)
            .FirstOrDefault();

        APIResponse response = APIResponse.Fail(
            message: message,
            obj: null,
            number: StatusCodes.Status400BadRequest
        );

        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddCors(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("ClientPermission");

app.Run();
