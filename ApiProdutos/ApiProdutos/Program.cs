using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var produtos = new List<Produto> {
    new Produto {Id = 2, Descricao = "Arroz", Quantidade = 50, Valor = 10.50M  },
    new Produto {Id = 3, Descricao = "Feijão", Quantidade = 50, Valor = 4.50M  },
    new Produto {Id = 4, Descricao = "Copo", Quantidade = 50, Valor = 2.99M  },
    new Produto {Id = 5, Descricao = "Prato", Quantidade = 50, Valor = 3.00M  },
    new Produto {Id = 6, Descricao = "Sabonete", Quantidade = 50, Valor = 0.90M  },
    new Produto {Id = 7, Descricao = "Pasta de Dente", Quantidade = 50, Valor = 3.50M  },
    new Produto {Id = 8, Descricao = "Celular", Quantidade = 50, Valor = 500.99M  },
    new Produto {Id = 9, Descricao = "Escova", Quantidade = 50, Valor = 7.50M  },
    new Produto {Id = 10, Descricao = "Pente", Quantidade = 50, Valor = 1.20M  },
    new Produto {Id = 1, Descricao = "Caixa de som", Quantidade = 50, Valor = 123.35M  },
};

List<Login> Usuarios = new()
{
   new Login {Nome = "bat", Senha = "bat11"},
   new Login {Nome = "zat", Senha = "zat222"}
};

var tokensPermitidos = new List<string>();

app.MapGet("/api/getprodutos", (HttpRequest request, ILoggerFactory loggerFactory) =>
{
    var log = loggerFactory.CreateLogger("API_APP");

    if (!Login.TokenValido(request, tokensPermitidos))
    {
        log.LogError("[ERRO] token invalido ou inexistente");
        return Results.StatusCode(401);
    }

    log.LogInformation("[SUCESSO] produtos retornados");
    return Results.Ok(produtos);

});

app.MapPost("/api/login", (ILoggerFactory loggerFactory, [FromBody] Login login) =>
{
    var log = loggerFactory.CreateLogger("API_APP");

    if (!login.LoginValido(Usuarios))
    {
        log.LogError("[ERRO] credenciais invalidas");
        return Results.BadRequest("Credenciais Incorretas");
    }

    var token = $"{Guid.NewGuid()}={DateTime.Now.AddDays(1):yyyy-MM-dd}";
    tokensPermitidos.Add(token);

    log.LogInformation("[SUCESSO] login ok, token retornado");
    return Results.Ok(token);
});

app.Run();


internal record Produto()
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public int Quantidade { get; set; }
    public decimal Valor { get; set; }
}

internal record Login()
{
    public string Nome { get; set; }
    public string Senha { get; set; }



    public bool LoginValido(List<Login> Usuarios)
    {
        return Usuarios.Any(x => x.Nome == Nome && x.Senha == Senha);
    }

    public static bool TokenValido(HttpRequest request, List<string> tokens)
    {
        request.Headers.ContainsKey("token_app");

        if (request.Headers.TryGetValue("token_app", out StringValues tokenheader))
        {
            if (StringValues.IsNullOrEmpty(tokenheader))
            {
                return false;
            }

            if (tokens.Contains(tokenheader[0]))
            {
                var tokenExpireDate = DateOnly.Parse(tokenheader[0].Split("=")[1]);

                return tokenExpireDate >= DateOnly.FromDateTime(DateTime.Now) ;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}
