using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud, ITelaOpcoes
{
    public TelaMedicamento(IRepositorio<Medicamento> repositorio) : base("Medicamento", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        throw new NotImplementedException();
    }

    protected override Medicamento ObterDadosCadastrais()
    {
        throw new NotImplementedException();
    }
}
