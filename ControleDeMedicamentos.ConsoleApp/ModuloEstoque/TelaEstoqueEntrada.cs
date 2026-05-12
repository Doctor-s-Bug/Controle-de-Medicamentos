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
    private readonly IRepositorio<Medicamento> repositorioMedicamento;
    private readonly IRepositorio<Paciente> repositorioPaciente;

    public TelaEstoque(IRepositorio<Paciente> repositorioPaciente, IRepositorio<Fornecedor> repositorioFornecedor, IRepositorio<Medicamento> repositorioMedicamento, IRepositorio<Funcionario> repositorioFuncionario)
    {
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioPaciente = repositorioPaciente;
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

    private EntradaEstoque? ObterDadosEntrada()
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
        EntradaEstoque? entrada = ObterDadosEntrada();

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
    public void RetirarEstoque()
    {
        SaidaEstoque? saidaEstoque = ObterDadosSaida();

        if (saidaEstoque == null)
        {
            Notificador.ExibirMensagem("Preencha todos os campos para dar saida no estoque!");
            RetirarEstoque();
            return;
        }

        List<string> erros = saidaEstoque.Validar();

        if (erros.Count > 0)
        {
            Notificador.ExibirMensagensErro(erros);
            EntradaEstoque();
            return;
        }

        bool conseguiuRetirar = saidaEstoque.Medicamento.RetirarQuantidadeAoMedicamento(saidaEstoque.QuantidadeSaida);

        if (!conseguiuRetirar)
        {
            Notificador.ExibirMensagem("Valor digitado Invalido! Digite um valor Valido!");
            RetirarEstoque();
            return;
        }

        Notificador.ExibirMensagem($"Retirado a quantidade {saidaEstoque.QuantidadeSaida} de {saidaEstoque.Medicamento.Nome}");
        System.Console.WriteLine();
    }
    private SaidaEstoque ObterDadosSaida()
    {
        Paciente? pacienteSelecionado = ObterPaciente();
        Medicamento? medicamentoSelecionado = ObterMedicamento();

        DateTime data = ObterData();
        System.Console.Write("Digite a Quantidade a ser Retirada do estoque: ");
        int quantidadeSaida = Convert.ToInt32(Console.ReadLine());

        return new SaidaEstoque(data, pacienteSelecionado, medicamentoSelecionado, quantidadeSaida);
    }
    private Medicamento? ObterMedicamento()
    {
        Console.Clear();
        System.Console.WriteLine("Selecione o Medicamento: \n");

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

        System.Console.WriteLine("Selecione o Funcionario para Iniciar a : \n");

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

    private Paciente? ObterPaciente()
    {
        Console.Clear();

        System.Console.WriteLine("Selecione o Paciente para o Medicamento: \n");

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -15} {3, -20} {4, -15}",
            "Id", "Nome", "Telefone", "Cartao SUS", "CPF"
        );

        List<Paciente> registrosPaciente = repositorioPaciente.SelecionarTodos();

        foreach (Paciente p in registrosPaciente)
        {
            Console.WriteLine(
                        "{0, -7} | {1, -20} | {2, -15} {3, -20} {4, -15}",
                        p.Id, p.Nome, p.Telefone, p.CartaoSus, p.Cpf
                    );
        }

        string idSelecionado;
        do
        {
            Console.Write("Digite o ID do MEDICAMENTO que deseja Selecionar: ");
            idSelecionado = Console.ReadLine() ?? string.Empty;

            if (idSelecionado.Length == 7)
                break;
        } while (true);
        Paciente? medicamentoSelecionado = repositorioPaciente.SelecionarPorId(idSelecionado);

        if (medicamentoSelecionado == null)
            return null;

        return medicamentoSelecionado;
    }
}
