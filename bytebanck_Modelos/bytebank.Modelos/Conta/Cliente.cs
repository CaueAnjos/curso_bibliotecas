namespace bytebank.Modelos.Conta
{
    public class Cliente
    {
        public string Cpf { get; set; } = string.Empty;

        private string _nome = string.Empty;
        public string Nome
        {
            get => _nome;
            set => _nome = value.Length >= 3 ? value : string.Empty;
        }

        public string Profissao { get; set; } = string.Empty;

        public static int TotalClientesCadastrados { get; set; }

        public Cliente()
        {
            TotalClientesCadastrados = TotalClientesCadastrados + 1;
        }
    }
}
