using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>, ITelaCrud, ITelaOpcoes
{
    public TelaPaciente(string nomeEntidade, IRepositorio<Paciente> repositorio) : base("Paciente", repositorio)
    {

    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        throw new NotImplementedException();
    }

    protected override Paciente ObterDadosCadastrais()
    {
        System.Console.Write("Digite o NOME do paciente: ");
        string? nome = Console.ReadLine();

        System.Console.Write("Digite o TELEFONE do paciente: ");
        string? telefone = Console.ReadLine();

        System.Console.Write("Digite o Cartao SUS do paciente: ");
        string? cartaoSus = Console.ReadLine();

        System.Console.Write("Digite o CPF do paciente: ");
        string? cpf = Console.ReadLine();

        return new Paciente(nome, telefone, cartaoSus, cpf);
    }
}
