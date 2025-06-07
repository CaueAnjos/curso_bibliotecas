using bytebank.Modelos.ADM.SistemaInterno;

namespace bytebank.Modelos.ADM.Funcionarios;

public abstract class FuncionarioAutenticavel : Funcionario, IAutenticavel
{
    public Senha Senha { get; set; } = new Senha();

    public FuncionarioAutenticavel(double salario, string cpf)
        : base(salario, cpf) { }

    public bool Autenticar(Senha senha)
    {
        return Senha.Equals(senha);
    }
}
