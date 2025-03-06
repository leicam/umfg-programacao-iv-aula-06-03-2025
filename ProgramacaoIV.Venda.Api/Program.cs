using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramacaoIV.Venda.Api.Context;
using ProgramacaoIV.Venda.Api.Entidades;

var connectionString = "Server=localhost;Port=3306;Database=umfg_venda_api;Uid=root;Pwd=root;";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VendaContext>(options => options.UseMySQL(connectionString));

#region endpoints

var app = builder.Build();

#region cliente

app.MapGet("/clientes", async (VendaContext context) => await context.Clientes.Where(x => x.IsAtivo).ToListAsync());

app.MapGet("/clientes/{id}", async (string id, VendaContext context) =>
    await context.Clientes.Where(x => x.Id == Guid.Parse(id) && x.IsAtivo).FirstOrDefaultAsync() is Cliente cliente ? Results.Ok(cliente) : Results.NotFound());

app.MapPost("/clientes", async (Cliente cliente, VendaContext context) =>
{
    context.Clientes.Add(cliente);
    await context.SaveChangesAsync();

    return Results.Created($"/clientes/{cliente.Id}", cliente);
});

app.MapPut("/clientes/{id}", async (string id, Cliente input, VendaContext context) =>
{
    var cliente = await context.Clientes.Where(x => x.Id == Guid.Parse(id) && x.IsAtivo).FirstOrDefaultAsync();

    if (cliente is null)
        return Results.NotFound();

    cliente.Nome = input.Nome;
    cliente.CPF = input.CPF;
    cliente.Endereco = input.Endereco;
    cliente.Telefone = input.Telefone;

    cliente.AtualizarDataAtualizacao();

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/clientes/{id}", async (string id, VendaContext context) =>
{
    var cliente = await context.Clientes.Where(x => x.Id == Guid.Parse(id) && x.IsAtivo).FirstOrDefaultAsync();

    if (cliente is null)
        return Results.NotFound();

    cliente.Inativar();

    await context.SaveChangesAsync();

    return Results.NoContent();
});

#endregion cliente

#region produto

app.MapGet("/produtos", async (VendaContext context) => await context.Produtos.Where(x => x.IsAtivo).ToListAsync());

app.MapGet("/produtos/{id}", async (string id, VendaContext context) =>
    await context.Produtos.Where(x => x.Id == Guid.Parse(id) && x.IsAtivo).FirstOrDefaultAsync() is Produto produto ? Results.Ok(produto) : Results.NotFound());

app.MapPost("/produtos", async (Produto produto, VendaContext context) =>
{
    context.Produtos.Add(produto);
    await context.SaveChangesAsync();

    return Results.Created($"/produtos/{produto.Id}", produto);
});

app.MapPut("/produtos/{id}", async (string id, Produto input, VendaContext context) =>
{
    var produto = await context.Produtos.Where(x => x.Id == Guid.Parse(id) && x.IsAtivo).FirstOrDefaultAsync();

    if (produto is null)
        return Results.NotFound();

    produto.EAN = input.EAN;
    produto.Descricao = input.Descricao;
    produto.PrecoCompra = input.PrecoCompra;
    produto.PrecoVenda = input.PrecoVenda;
    produto.Estoque = input.Estoque;

    produto.AtualizarDataAtualizacao();

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/produtos/{id}", async (string id, VendaContext context) =>
{
    var produto = await context.Produtos.Where(x => x.Id == Guid.Parse(id) && x.IsAtivo).FirstOrDefaultAsync();

    if (produto is null)
        return Results.NotFound();

    produto.Inativar();

    await context.SaveChangesAsync();

    return Results.NoContent();
});

#endregion produto

#endregion endpoints

app.UseHttpsRedirection();
app.Run();