using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class TelaFuncionario : TelaBase<Funcionario>, ITelaCrud, ITelaOpcoes
{
    public TelaFuncionario(IRepositorio<Funcionario> repositorio) : base("Funcionario", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        List<Funcionario> listadefornecedor = repositorio.SelecionarTodos();

        if (deveExibirCabecalho)
        {
            ExibirCabecalho("Visualizando Funcionarios...");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -25} {3, -20}",
            "Id", "Nome", "Telefone", "CPF"
        );

        foreach (Funcionario funcionario in listadefornecedor)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -25} {3, -20}",
                funcionario.Id, funcionario.Nome, funcionario.Telefone, funcionario.Cpf
            );
        }

        if (deveExibirCabecalho)
        {
            Notificador.ExibirMensagem("");
        }
    }

    protected override Funcionario ObterDadosCadastrais()
    {
        System.Console.Write("Digite o NOME do Funcionario: ");
        string? nome = Console.ReadLine();

        System.Console.Write("Digite o TELEFONE do Funcionario: ");
        string? telefone = Console.ReadLine();

        System.Console.Write("Digite o CPF do Funcionario: ");
        string? cpf = Console.ReadLine();

        return new Funcionario(nome, telefone, cpf);
    }
}
