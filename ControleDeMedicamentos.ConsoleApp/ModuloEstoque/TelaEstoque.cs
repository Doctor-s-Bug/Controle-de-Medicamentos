using System.Globalization;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class TelaEstoque : ITelaOpcoes
{
    private readonly IRepositorio<Fornecedor> repositorioFornecedor;
    private readonly IRepositorio<Funcionario> repositorioFuncionario;
    private readonly IRepositorio<Paciente> repositorioPaciente;
    private readonly IRepositorio<Medicamento> repositorioMedicamento;

    public TelaEstoque(IRepositorio<Paciente> repositorioPaciente, IRepositorio<Fornecedor> repositorioFornecedor, IRepositorio<Medicamento> repositorioMedicamento, IRepositorio<Funcionario> repositorioFuncionario)
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }
    public string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Estoque");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Dar Entrada no Estoque");
        Console.WriteLine("2 - Retirar do Estoque");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    private EntradaEstoque? ObterDadosCadastrais()
    {
        Console.Clear();

        DateTime data = ObterData();
        Funcionario? funcionarioSelecionado = ObterFuncionario();
        Medicamento? medicamentoSelecionado = ObterMedicamento();

        System.Console.Write("Digite a Quantidade a ser adicionada ao estoque: ");
        int quantidadeEntrada = Convert.ToInt32(Console.ReadLine());
        return new EntradaEstoque(data, medicamentoSelecionado, funcionarioSelecionado, quantidadeEntrada);

    }

    public void EntradaEstoque()
    {
        EntradaEstoque? entrada = ObterDadosCadastrais();

        if (entrada == null)
        {
            Notificador.ExibirMensagem("Preencha todos os campos para dar entrada no estoque!");
            EntradaEstoque();
            return;
        }


        List<string> erros = entrada.Validar();

        if (erros.Count > 0)
        {
            Notificador.ExibirMensagensErro(erros);
            EntradaEstoque();
            return;
        }

        entrada.Medicamento.AdiconarQuantidadeAoMedicamento(entrada.QuantidadeEntrada);


    }
    private Medicamento? ObterMedicamento()
    {
        Console.Clear();
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -25} | {3, -20} | {4, -15}",
            "Id", "Nome", "Descrição", "Quantidade Estoque", "Fornecedor"
        );

        List<Medicamento> registrosMedicamentos = repositorioMedicamento.SelecionarTodos();

        foreach (Medicamento m in registrosMedicamentos)
        {
            Console.WriteLine(
                        "{0, -7} | {1, -20} | {2, -25} | {3, -20} | {4, -15}",
                        m.Id, m.Nome, m.Descricao, m.QuantidadeEstoque, m.Fornecedor.Nome
                    );
        }
        string idSelecionado;
        do
        {
            Console.Write("Digite o ID do registro que deseja Selecionar: ");
            idSelecionado = Console.ReadLine() ?? string.Empty;

            if (idSelecionado.Length == 7)
                break;
        } while (true);
        Medicamento? medicamentoSelecionado = repositorioMedicamento.SelecionarPorId(idSelecionado);

        if (medicamentoSelecionado == null)
            return null;

        return medicamentoSelecionado;
    }
    public Funcionario? ObterFuncionario()
    {
        Console.Clear();
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

        Console.WriteLine(
        "{0, -7} | {1, -20} | {2, -25} {3, -20}",
        "Id", "Nome", "Telefone", "Cnpj"
    );

        foreach (Funcionario funcionario in funcionarios)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -25} {3, -20}",
                funcionario.Id, funcionario.Nome, funcionario.Telefone, funcionario.Cpf
            );
        }
        string idSelecionado;
        do
        {
            Console.Write("Digite o ID do registro que deseja Selecionar: ");
            idSelecionado = Console.ReadLine() ?? string.Empty;

            if (idSelecionado.Length == 7)
                break;
        } while (true);
        Funcionario? funcionarioSelecionado = repositorioFuncionario.SelecionarPorId(idSelecionado);

        if (funcionarioSelecionado == null)
            return null;

        return funcionarioSelecionado;
    }
    private DateTime ObterData()
    {
        while (true)
        {
            Console.Write("Digite uma data (dd/MM/yyyy): ");
            string? entrada = Console.ReadLine();

            bool conseguiuConverter = DateTime.TryParseExact(
                entrada,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime data
            );

            if (conseguiuConverter)
                return data;

            Notificador.ExibirMensagem("Digite uma data válida! (dd/MM/yyyy)");
        }
    }

    internal void RetirarEstoque()
    {
        throw new NotImplementedException();
    }
}
