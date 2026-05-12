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


}
