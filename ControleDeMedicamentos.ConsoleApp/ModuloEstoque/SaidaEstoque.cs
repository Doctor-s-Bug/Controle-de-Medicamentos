using System;
using System.Security.Cryptography;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class SaidaEstoque
{
    public string Id { get; set; }
    public DateTime Data { get; set; }
    public Paciente Paciente { get; set; }
    public Medicamento Medicamento { get; set; }
    public int QuantidadeSaida { get; set; }

    public SaidaEstoque(DateTime data, Paciente paciente, Medicamento medicamento, int quantidadeSaida)
    {
        Id = Convert
                .ToHexString(RandomNumberGenerator.GetBytes(4))
                .ToLower()
                .Substring(0, 7);

        Data = data;
        Paciente = paciente;
        Medicamento = medicamento;
        QuantidadeSaida = quantidadeSaida;
    }

    public List<String> Validar()
    {
        List<string> erros = new();

        if (Medicamento == null)
            erros.Add("O campo \"Medicamento\" não pode ser vazio!");

        if (Paciente == null)
            erros.Add("O campo \"Paciente\" não pode ser vazio!");

        return erros;
    }

}
