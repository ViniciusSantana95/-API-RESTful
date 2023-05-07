using System.Text.Json.Serialization;
using System;

namespace Umfg.Programacaoiv2023.Primeira.Api
{
    public sealed class Produto
    {
        public Guid Id { get; private set; }
        [JsonPropertyName("codigoBarra")]
        public Guid CodigoBarra { get; set; }
        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }
        [JsonPropertyName("valor")]
        public decimal Valor { get; set; }

        public Produto(string descricao, decimal valor)
        {
            Id = Guid.NewGuid();
            Descricao = descricao;
            Valor = valor;
            CodigoBarra = Guid.NewGuid();
        }

    }
}
