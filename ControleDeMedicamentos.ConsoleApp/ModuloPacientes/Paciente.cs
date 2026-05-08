using System;
using System.Runtime.Serialization;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class Paciente : EntidadeBase
{
  public Paciente(string nome, string telefone, string cartaoSus, string cpf)
  {
    Nome = nome;
    Telefone = telefone;
    CartaoSus = cartaoSus;
    Cpf = cpf;
  }

  public string Nome { get; set; }
  public string Telefone { get; set; }
  public string CartaoSus { get; set; }
  public string Cpf { get; set; }


  public override void AtualizarDados(EntidadeBase entidadeAtualizada)
  {
    //cast = confia
    Paciente? pacienteAtualizado = (Paciente?)entidadeAtualizada;

    Nome = pacienteAtualizado.Nome;
    Telefone = pacienteAtualizado.Telefone;
    CartaoSus = pacienteAtualizado.CartaoSus;
    Cpf = pacienteAtualizado.Cpf;
  }

  public override List<string> Validar()
  {
    List<string> erros = new List<string>();

    if (String.IsNullOrWhiteSpace(Nome))
      erros.Add("Nome não pode ser Null!;");

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

    if (Cpf.Length != 14)
      erros.Add("O campo CPF deve conter 14 digitos!;");

    return erros;
  }
}
