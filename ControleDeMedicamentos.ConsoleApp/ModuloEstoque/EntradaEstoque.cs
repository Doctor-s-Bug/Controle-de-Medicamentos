using System;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class EntradaEstoque
{
    public DateTime dataEntrada { get; set; }
    public Medicamento Medicamento { get; set; }
    public Funcionario Funcionario { get; set; }
    public int QuantidadeEntrada { get; set; }

    public EntradaEstoque(DateTime dataEntrada, Medicamento medicamento, Funcionario funcionario, int quantidadeEntrada)
    {
        this.dataEntrada = dataEntrada;
        Medicamento = medicamento;
        Funcionario = funcionario;
        QuantidadeEntrada = quantidadeEntrada;
    }

    public List<String> Validar()
    {
        List<string> erros = new();

        if (Medicamento == null)
            erros.Add("O campo \"Medicamento\" não pode ser vazio!");

        if (Funcionario == null)
            erros.Add("O campo \"Funcionario\" não pode ser vazio!");

        if (QuantidadeEntrada <= 0)
            erros.Add("O campo \"Quantidade\" deve ser maior que zero!");


        return erros;
    }

}
