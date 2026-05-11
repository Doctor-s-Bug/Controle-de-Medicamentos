using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaOpcoes, ITelaCrud
{
    public TelaFornecedor(IRepositorio<Fornecedor> repositorio) : base("Fornecedor", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        List<Fornecedor> listadefornecedor = repositorio.SelecionarTodos();

        if (deveExibirCabecalho)
        {
            ExibirCabecalho("Visualizando Fornecedores...");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -15} | {3, -25}",
            "Id", "Nome", "Telefone", "Cnpj"
        );

        foreach (Fornecedor fornecedor in listadefornecedor)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -15} | {3, -25}",
                fornecedor.Id, fornecedor.Nome, fornecedor.Telefone, fornecedor.Cnpj
            );
        }

        if (deveExibirCabecalho)
        {
            Notificador.ExibirMensagem();
        }
    }

    protected override Fornecedor ObterDadosCadastrais()
    {
        Console.WriteLine("Digite o nome do fornecedor: ");
        string nome = Console.ReadLine();

        Console.WriteLine("Digite o telefone: ");
        string telefone = Console.ReadLine();

        Console.WriteLine("Digite o Cnpj: ");
        string Cnpj = Console.ReadLine();

        return new Fornecedor(nome, telefone, Cnpj);
    }
}
