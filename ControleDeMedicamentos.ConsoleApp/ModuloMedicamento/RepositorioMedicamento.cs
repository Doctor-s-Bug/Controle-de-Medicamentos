using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class RepositorioMedicamento : RepositorioBaseEmArquivo<Medicamento>, IRepositorio<Medicamento>
{
    public RepositorioMedicamento(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Medicamento> CarregarRegistros()
    {
        return contexto.Medicamentos;
    }
}
