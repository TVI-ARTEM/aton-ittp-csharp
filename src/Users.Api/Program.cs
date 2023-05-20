using System.Net;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Swashbuckle.AspNetCore.SwaggerGen;
using Users.Api.ActionFilters;
using Users.Bll.Extensions;
using Users.Dal.Contexts;
using Users.Dal.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
    .AddControllers().Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(ConfigureSwagger)
    .AddFluentValidation(conf =>
    {
        conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
        conf.AutomaticValidationEnabled = true;
    })
    .AddBllInfrastructure(builder.Configuration)
    .AddDalInfrastructure(builder.Configuration)
    .AddMvc(ConfigureMvc);


var app = builder.Build();

app.MigrateUp();

ReloadSqlTypes(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();


void ReloadSqlTypes(IServiceProvider appServices)
{
    using var scope = appServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<UserContext>();

    using var conn = (NpgsqlConnection)context.Database.GetDbConnection();
    conn.Open();
    conn.ReloadTypes();
}

void ConfigureMvc(MvcOptions x)
{
    x.Filters.Add(new ExceptionFilterAttribute());
    x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.InternalServerError));
    x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.BadRequest));
    x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.Forbidden));
    x.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.OK));
}

void ConfigureSwagger(SwaggerGenOptions o)
{
    o.CustomSchemaIds(x => x.FullName);
}