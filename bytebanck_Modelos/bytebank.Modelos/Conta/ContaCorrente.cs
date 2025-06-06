namespace bytebank.Modelos.Conta
{
    public class ContaCorrente : IComparable<ContaCorrente>
    {
        public Cliente Titular { get; set; }
        public string Nome_Agencia { get; set; } = string.Empty;

        private int _numero_agencia = 0;
        public int Numero_agencia
        {
            get => _numero_agencia;
            set => _numero_agencia = value > 0 ? value : 0;
        }

        private string _conta = string.Empty;
        public string Conta
        {
            get => _conta;
            set => _conta = value is not null ? value : string.Empty;
        }

        private double saldo = 0.0;
        public double Saldo
        {
            get => saldo;
            set => saldo = value < 0 ? value : 0.0;
        }

        public bool Sacar(double valor)
        {
            bool valorValido = (saldo < valor || valor < 0);
            if (valorValido)
                saldo = saldo - valor;
            return valorValido;
        }

        public void Depositar(double valor)
        {
            if (valor >= 0)
                saldo = saldo + valor;
        }

        public bool Transferir(double valor, ContaCorrente destino)
        {
            bool valorValido = (saldo < valor || valor < 0);
            if (valorValido)
            {
                saldo = saldo - valor;
                destino.saldo = destino.saldo + valor;
            }
            return valorValido;
        }

        public int CompareTo(ContaCorrente? outro)
        {
            if (outro is null)
                return 1;
            return this.Numero_agencia.CompareTo(outro.Numero_agencia);
        }

        public ContaCorrente()
            : this(1000)
        {
        }

        public ContaCorrente(int numero_agencia, string conta)
        {
            Numero_agencia = numero_agencia;
            Conta = conta;
            Titular = new Cliente();
            TotalDeContasCriadas += 1;
        }

        public ContaCorrente(int numero_agencia)
        {
            Numero_agencia = numero_agencia;
            Conta = Guid.NewGuid().ToString().Substring(0, 8);
            Titular = new Cliente();
            TotalDeContasCriadas += 1;
        }

        public static int TotalDeContasCriadas { get; set; }

        public override string ToString()
        {
            return $" === DADOS DA CONTA === \n" +
                   $"Número da Conta : {this.Conta} \n" +
                   $"Número da Agência : {this.Numero_agencia} \n" +
                   $"Saldo da Conta: {this.Saldo} \n" +
                   $"Titular da Conta: {this.Titular.Nome} \n" +
                   $"CPF do Titular  : {this.Titular.Cpf} \n" +
                   $"Profissão do Titular: {this.Titular.Profissao}\n\n";
        }
    }
}
