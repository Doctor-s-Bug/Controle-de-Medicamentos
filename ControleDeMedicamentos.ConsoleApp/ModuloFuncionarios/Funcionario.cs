using ControleDeMedicamentos.ConsoleApp.Compartilhado;

public class Funcionario : EntidadeBase
{
    public string Nome;
    public string Telefone;
    public string Cpf;

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        throw new NotImplementedException();
    }

    public override List<string> Validar()
    {
        throw new NotImplementedException();
    }
}