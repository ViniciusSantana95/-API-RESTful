using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Umfg.Programacaoiv2023.Primeira.Api
{
    public class Program
    {
       
        private static List<Cliente> _lista = new List<Cliente>()
        {
            new Cliente("Teste Um"),
            new Cliente("Teste Dois"),
        };

        private static List<Produto> _lista2 = new List<Produto>()
        {
            new Produto("Produto Um", 1500),
            new Produto("Produto Dois", 2000),
        };

        public static void Main(string[] args)
        {
            var app = WebApplication.Create(args);

            app.MapGet("api/v1/cliente", ObterTodosClientesAsync);
            app.MapGet("api/v1/cliente/{id}", ObterClientePorIdAsync);
            app.MapPost("api/v1/cliente", CadastrarClienteAsync);
            app.MapPut("api/v1/cliente/{id}", AtualizarClienteAsync);
            app.MapDelete("api/v1/cliente", RemoverTodosClientesAsync);
            app.MapDelete("api/v1/cliente/{id}", RemoverClienteAsync);

            app.MapGet("api/v1/produto", ObterTodosProdutosAsync);
            app.MapGet("api/v1/produto/{id}", ObterProdutoPorIdAsync);
            app.MapPost("api/v1/produto", CadastrarProdutoAsync);
            app.MapPut("api/v1/produto/{id}", AtualizarProdutoAsync);
            app.MapDelete("api/v1/produto", RemoverTodosProdutosAsync);
            app.MapDelete("api/v1/produto/{id}", RemoverProdutoAsync);

            app.Run();
        }

        public static async Task ObterTodosClientesAsync(HttpContext context)
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsJsonAsync(_lista);
        }

        public static async Task ObterClientePorIdAsync(HttpContext context)
        {
            if (!context.Request.RouteValues.TryGet("id", out Guid id))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Parâmetro id não foi enviado! Verifique.");
                return;
            }

            var cliente = _lista.FirstOrDefault(x => x.Id == id);

            if (cliente == null)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync($"Cliente não encontrado para o id: {id}. Verifique.");
                return;
            }

            context.Response.StatusCode = 200;
            await context.Response.WriteAsJsonAsync(cliente);
        }

        public static async Task CadastrarClienteAsync(HttpContext context)
        {
            var cliente = await context.Request.ReadFromJsonAsync<Cliente>();

            if (cliente == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Não foi possivel cadastrar o cliente! Verifique.");
                return;
            }

            _lista.Add(cliente);

            context.Response.StatusCode = (int)HttpStatusCode.Created;
            await context.Response.WriteAsJsonAsync(cliente);
        }

        public static async Task AtualizarClienteAsync(HttpContext context)
        {
            if (!context.Request.RouteValues.TryGet("id", out Guid id))
            {
                context.Response.StatusCode= (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Parâmetro id não foi enviado! Verifique.");

                return;
            }

            var cliente = _lista.FirstOrDefault(x => x.Id == id);

            if (cliente == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync($"Cliente não encontrado para o id: {id}. Verfique.");

                return;
            }

            _lista.Remove(cliente);

            cliente.Nome = (await context.Request.ReadFromJsonAsync<Cliente>()).Nome;

            _lista.Add(cliente);

            context.Response.StatusCode =(int)HttpStatusCode.OK;
            await context.Response.WriteAsJsonAsync(cliente);
        }

        public static async Task RemoverTodosClientesAsync(HttpContext context)
        {
            _lista.Clear();

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsync("Todos os clientes foram removidos com sucesso!");
        }

        public static async Task RemoverClienteAsync(HttpContext context)
        {
            if (!context.Request.RouteValues.TryGet("id", out Guid id))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Parâmetro id não foi enviado! Verifique.");

                return;
            }

            var cliente = _lista.FirstOrDefault(x => x.Id == id);

            if (cliente == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync($"Cliente não encontrado para o id: {id}. Verifique.");

                return;
            }

            _lista.Remove(cliente);
            await context.Response.WriteAsync("Cliente removido com sucesso!");
        }

        public static async Task ObterTodosProdutosAsync(HttpContext context)
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsJsonAsync(_lista2);
        }

        public static async Task ObterProdutoPorIdAsync(HttpContext context)
        {
            if (!context.Request.RouteValues.TryGet("id", out Guid id))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Parâmetro id não foi enviado! Verifique.");
                return;
            }

            var Produto = _lista2.FirstOrDefault(x => x.Id == id);

            if (Produto == null)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync($"Produto não encontrado para o id: {id}. Verifique.");
                return;
            }

            context.Response.StatusCode = 200;
            await context.Response.WriteAsJsonAsync(Produto);
        }

        public static async Task CadastrarProdutoAsync(HttpContext context)
        {
            var Produto = await context.Request.ReadFromJsonAsync<Produto>();

            if (Produto == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Não foi possivel cadastrar o Produto! Verifique.");
                return;
            }

            _lista2.Add(Produto);

            context.Response.StatusCode = (int)HttpStatusCode.Created;
            await context.Response.WriteAsJsonAsync(Produto);
        }

        public static async Task AtualizarProdutoAsync(HttpContext context)
        {
            if (!context.Request.RouteValues.TryGet("id", out Guid id))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Parâmetro id não foi enviado! Verifique.");

                return;
            }

            var Produto = _lista2.FirstOrDefault(x => x.Id == id);

            if (Produto == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync($"Produto não encontrado para o id: {id}. Verfique.");

                return;
            }

            _lista2.Remove(Produto);

            Produto.Descricao = (await context.Request.ReadFromJsonAsync<Produto>()).Descricao;

            _lista2.Add(Produto);

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsJsonAsync(Produto);
        }

        public static async Task RemoverTodosProdutosAsync(HttpContext context)
        {
            _lista2.Clear();

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsync("Todos os Produtos foram removidos com sucesso!");
        }

        public static async Task RemoverProdutoAsync(HttpContext context)
        {
            if (!context.Request.RouteValues.TryGet("id", out Guid id))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Parâmetro id não foi enviado! Verifique.");

                return;
            }

            var Produto = _lista2.FirstOrDefault(x => x.Id == id);

            if (Produto == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync($"Produto não encontrado para o id: {id}. Verifique.");

                return;
            }

            _lista2.Remove(Produto);
            await context.Response.WriteAsync("Produto removido com sucesso!");
        }

    }
}