using bytebank.Modelos.ADM.SistemaInterno;


namespace bytebank.Modelos.ADM.Funcionarios
{
    public abstract class FuncionarioAutenticavel : Funcionario, IAutenticavel
    {
        public string? Senha { get; set; }

        public FuncionarioAutenticavel(double salario, string cpf)
            : base(salario, cpf)
        {
        }

        public bool Autenticar(string senha)
        {
            if (string.IsNullOrEmpty(Senha))
                throw new NullReferenceException("Senha is null");

            return Senha == senha;
        }
    }
}
