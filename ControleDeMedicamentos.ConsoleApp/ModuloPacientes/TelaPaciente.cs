using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;
namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>, ITelaCrud, ITelaOpcoes
{
    public TelaPaciente(IRepositorio<Paciente> repositorio) : base("Paciente", repositorio)
    {

    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            ExibirCabecalho("Visualizando Pacientes...");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -15} {3, -20} {4, -15}",
            "Id", "Nome", "Telefone", "Cartao SUS", "CPF"
        );

        List<Paciente> registrosPaciente = repositorio.SelecionarTodos();

        foreach (Paciente p in registrosPaciente)
        {
            Console.WriteLine(
                        "{0, -7} | {1, -20} | {2, -10} {3, -15} {4, -15}",
                        p.Id, p.Nome, p.Telefone, p.CartaoSus, p.Cpf
                    );
        }
        if (deveExibirCabecalho)
        {
            Notificador.ExibirMensagem("");
        }
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
