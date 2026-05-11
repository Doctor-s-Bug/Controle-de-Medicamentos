using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud, ITelaOpcoes
{
    private IRepositorio<Fornecedor> repositorioFornecedor;
    public TelaMedicamento(IRepositorio<Medicamento> repositorio, IRepositorio<Fornecedor> repositorioFornecedor) : base("Medicamento", repositorio)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            ExibirCabecalho("Visualizando Medicamentos...");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -25} | {3, -20} | {4, -15}",
            "Id", "Nome", "Descrição", "Quantidade Estoque", "Fornecedor"
        );

        List<Medicamento> registrosMedicamentos = repositorio.SelecionarTodos();

        foreach (Medicamento m in registrosMedicamentos)
        {
            Console.WriteLine(
                        "{0, -7} | {1, -20} | {2, -25} {3, -20} | {4, -15}",
                        m.Id, m.Nome, m.Descricao, m.QuantidadeEstoque, m.Fornecedor.Nome
                    );
        }
        if (deveExibirCabecalho)
        {
            Notificador.ExibirMensagem();
        }
    }

    protected override Medicamento? ObterDadosCadastrais()
    {
        System.Console.Write("Digite o NOME do Medicamento: ");
        string? nome = Console.ReadLine();

        System.Console.Write("Digite o DESCRICAO do Medicamento: ");
        string? descricao = Console.ReadLine();

        System.Console.Write("Digite a QUANTIDADE do Medicamento: ");
        int quantidadeEstoque = Convert.ToInt32(Console.ReadLine());

        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();
        System.Console.WriteLine("-----------------------------");

        foreach (Fornecedor f in fornecedores)
        {
            Console.WriteLine($"ID: {f.Id}, Nome: {f.Nome}, Telefone: {f.Telefone}, CNPJ: {f.Cnpj}");
        }
        System.Console.WriteLine("-----------------------------");

        string? idSelecionado;
        do
        {
            System.Console.WriteLine("Digite o ID do Fornecedor do Medicamento: Digite 0 para sair");
            System.Console.Write("> ");
            idSelecionado = Console.ReadLine();


            if (!String.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;

            Notificador.ExibirMensagem("Digite um ID valido!");

        } while (true);

        Fornecedor? fornecedorSelecionado = repositorioFornecedor.SelecionarPorId(idSelecionado);

        if (fornecedorSelecionado == null)
            return null;

        return new Medicamento(nome, descricao, quantidadeEstoque, fornecedorSelecionado);
    }
}
