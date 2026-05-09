using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class Fornecedor : EntidadeBase
{
    public Fornecedor(string nome, string telefone, string cnpj)
    {
        Nome = nome;
        Telefone = telefone;
        Cnpj = cnpj;
    }

    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Cnpj { get; set; }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Fornecedor? fornecedorAtualizado = (Fornecedor?)entidadeAtualizada;

        Nome = fornecedorAtualizado.Nome;
        Telefone = fornecedorAtualizado.Telefone;
        Cnpj = fornecedorAtualizado.Cnpj;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (String.IsNullOrWhiteSpace(Nome))
            erros.Add("Nome não pode ser Null;");

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("Nome deve conter entre 3 a 100 caracteres!;");


        string telefoneEncurtado = Telefone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
        bool contemLetraOuSimbolo = false;
        int contadorDigitos = 0;

        for (int i = 0; i < telefoneEncurtado.Length; i++)
        {
            char c = telefoneEncurtado[i];
            if (char.IsDigit(c))
                contadorDigitos++;
            else
            {
                contemLetraOuSimbolo = true;
                break;
            }
        }

        if (contadorDigitos < 10 || contadorDigitos > 11)
            erros.Add("O campo \"Telefone\" deve conter entre 10 e 11 dígitos;");

        if (contemLetraOuSimbolo)
            erros.Add("O campo \"Telefone\" deve conter apenas dígitos;");

        if (Cnpj.Length != 14)
            erros.Add("O campo CNPJ deve conter 14 digitos!;");

        return erros;
    }
}
