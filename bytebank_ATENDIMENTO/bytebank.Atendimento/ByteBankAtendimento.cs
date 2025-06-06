using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using bytebank.Modelos.Conta;

namespace bytebank_ATENDIMENTO.bytebank.Atendimento;

internal class ByteBankAtendimento
{

    private List<ContaCorrente> _listaDeContas = new List<ContaCorrente>(){
        new ContaCorrente(95, "123456-X"){Saldo=100,Titular = new Cliente{Cpf="11111",Nome ="Henrique"}},
        new ContaCorrente(95, "951258-X"){Saldo=200,Titular = new Cliente{Cpf="22222",Nome ="Pedro"}},
        new ContaCorrente(94, "987321-W"){Saldo=60,Titular = new Cliente{Cpf="33333",Nome ="Marisa"}}
    };

    public char PedirOpcao()
    {
        string? resposta = Console.ReadLine();
        if (string.IsNullOrEmpty(resposta) || resposta.Length > 1 || !Regex.IsMatch(resposta, "[0-9]"))
            return '?'; // Default option

        char opcao = resposta[0];
        return opcao;
    }

    public void AtendimentoCliente()
    {
        char opcao = '0';
        while (opcao != '7')
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===       Atendimento       ===");
            Console.WriteLine("===1 - Cadastrar Conta      ===");
            Console.WriteLine("===2 - Listar Contas        ===");
            Console.WriteLine("===3 - Remover Conta        ===");
            Console.WriteLine("===4 - Ordenar Contas       ===");
            Console.WriteLine("===5 - Pesquisar Conta      ===");
            Console.WriteLine("===6 - Exportar para JSON   ===");
            Console.WriteLine("===7 - Sair do Sistema      ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n\n");
            Console.Write("Digite a opção desejada: ");

            opcao = PedirOpcao();

            switch (opcao)
            {
                case '1':
                    CadastrarConta();
                    break;
                case '2':
                    ListarContas();
                    break;
                case '3':
                    RemoverContas();
                    break;
                case '4':
                    OrdenarContas();
                    break;
                case '5':
                    PesquisarContas();
                    break;
                case '6':
                    ExportarParaJSON();
                    break;
                case '7':
                    EncerrarAplicacao();
                    break;
                default:
                    Console.WriteLine("Opcao não implementada.");
                    break;
            }
        }
    }


    public void ExportarParaJSON()
    {
        Console.Clear();
        Console.WriteLine("================================");
        Console.WriteLine("===    EXPROTAR PARA JSON    ===");
        Console.WriteLine("================================");
        Console.WriteLine("\n");

        Console.WriteLine("How do you want to export the data? (xml or json)");
        string exportType = Console.ReadLine()?.ToLower() ?? "json";
        if (exportType != "xml" && exportType != "json")
            exportType = "json";

        Console.WriteLine("Where the file should be saved?");
        string fileName = $"Contas.{exportType}";
        string defaultPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                fileName);
        string filePath = Path.GetFullPath(defaultPath);

        string? userPath = Console.ReadLine();
        try
        {
            filePath = Path.GetFullPath(userPath!);
        }
        catch
        {
            Console.WriteLine("Caminho inválido.");
        }


        switch (exportType)
        {
            case "xml":
                XmlSerializer serializer = new XmlSerializer(typeof(List<ContaCorrente>));
                FileStream fs = new FileStream(filePath, FileMode.Create);
                serializer.Serialize(fs, _listaDeContas);
                fs.Close();
                break;
            case "json":
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                string json = JsonSerializer.Serialize(_listaDeContas, options);
                File.WriteAllText(filePath, json);
                break;
        }

        Console.WriteLine($"Salvando arquivo em {filePath}");
        Console.ReadKey();
    }

    private void EncerrarAplicacao()
    {
        Console.WriteLine("... Encerrando a aplicação ...");
        Console.ReadKey();
    }

    private void PesquisarContas()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("===    PESQUISAR CONTAS     ===");
        Console.WriteLine("===============================");
        Console.WriteLine("\n");
        Console.Write("Deseja pesquisar por (1) NÚMERO DA CONTA ou (2)CPF TITULAR ou " +
                " (3) Nº AGÊNCIA : ");

        int opcao = 0;
        int.TryParse(Console.ReadLine(), out opcao);

        switch (opcao)
        {
            case 1:
                Console.Write("Informe o número da Conta: ");
                string? numeroConta = Console.ReadLine();
                if (numeroConta is null)
                    return;

                ContaCorrente? consultaConta = ConsultaPorNumeroConta(numeroConta);
                Console.WriteLine(consultaConta?.ToString());
                Console.ReadKey();
                break;
            case 2:
                Console.Write("Informe o CPF do Titular: ");
                string? cpf = Console.ReadLine();
                if (cpf is null)
                    return;

                ContaCorrente? consultaCpf = ConsultaPorCPFTitular(cpf);
                Console.WriteLine(consultaCpf?.ToString());
                Console.ReadKey();
                break;
            case 3:
                Console.Write("Informe o Nº da Agência: ");
                int numeroAgencia = 0;
                if (!int.TryParse(Console.ReadLine(), out numeroAgencia))
                    return;

                List<ContaCorrente> contasPorAgencia = ConsultaPorAgencia(numeroAgencia);
                ExibirListaDeContas(contasPorAgencia);
                Console.ReadKey();
                break;
            default:
                Console.WriteLine("Opção não implementada.");
                break;
        }

    }

    private void ExibirListaDeContas(List<ContaCorrente> contasPorAgencia)
    {
        if (contasPorAgencia == null)
        {
            Console.WriteLine(" ... A consulta não retornou dados ...");
        }
        else
        {
            foreach (var item in contasPorAgencia)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }

    private List<ContaCorrente> ConsultaPorAgencia(int numeroAgencia)
    {
        var consulta = (
                from conta in _listaDeContas
                where conta.Numero_agencia == numeroAgencia
                select conta).ToList();
        return consulta;
    }

    private ContaCorrente? ConsultaPorCPFTitular(string? cpf)
    {

        return _listaDeContas.Where(conta => conta.Titular.Cpf == cpf).FirstOrDefault();
    }

    private ContaCorrente? ConsultaPorNumeroConta(string? numeroConta)
    {
        return _listaDeContas.Where(conta => conta.Conta == numeroConta).FirstOrDefault();
    }

    private void OrdenarContas()
    {
        _listaDeContas.Sort();
        Console.WriteLine("... Lista de Contas ordenadas ...");
        Console.ReadKey();
    }

    private void RemoverContas()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("===      REMOVER CONTAS     ===");
        Console.WriteLine("===============================");
        Console.WriteLine("\n");

        Console.Write("Informe o número da Conta: ");
        string? numeroConta = Console.ReadLine();
        if (numeroConta is null)
        {
            Console.WriteLine(" ... Conta para remoção não encontrada ...");
            return;
        }

        ContaCorrente? conta = _listaDeContas.Where(c => c.Conta == numeroConta).FirstOrDefault();

        if (conta is not null)
        {
            _listaDeContas.Remove(conta);
            Console.WriteLine("... Conta removida da lista! ...");
        }
        else
        {
            Console.WriteLine(" ... Conta para remoção não encontrada ...");
        }

        Console.ReadKey();
    }

    private void ListarContas()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("===     LISTA DE CONTAS     ===");
        Console.WriteLine("===============================");
        Console.WriteLine("\n");

        if (_listaDeContas.Count == 0)
        {
            Console.WriteLine("... Não há contas cadastradas! ...");
            Console.ReadKey();
            return;
        }

        foreach (ContaCorrente item in _listaDeContas)
        {
            Console.WriteLine(item.ToString());
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.ReadKey();
        }
    }

    private void CadastrarConta()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("===   CADASTRO DE CONTAS    ===");
        Console.WriteLine("===============================");
        Console.WriteLine("\n");
        Console.WriteLine("=== Informe dados da conta ===");
        Console.Write("Número da Agência: ");

        int numeroAgencia = 0;
        if (!int.TryParse(Console.ReadLine(), out numeroAgencia))
        {
            Console.WriteLine("Nenhum input foi recebido");
            return;
        }

        ContaCorrente conta = new ContaCorrente(numeroAgencia);
        Console.WriteLine($"Número da conta [NOVA] : {conta.Conta}");

        Console.Write("Informe o saldo inicial: ");
        double saldo = 0.0;
        if (!double.TryParse(Console.ReadLine(), out saldo))
        {
            Console.WriteLine("Nenhum input foi recebido");
            Console.ReadKey();
            return;
        }
        conta.Saldo = saldo;

        Console.Write("Infome nome do Titular: ");
        string? nome = Console.ReadLine();
        if (nome is not null)
            conta.Titular.Nome = nome;

        Console.Write("Infome CPF do Titular: ");
        string? cpf = Console.ReadLine();
        if (cpf is not null)
            conta.Titular.Cpf = cpf;

        Console.Write("Infome Profissão do Titular: ");
        string? profisao = Console.ReadLine();
        if (profisao is not null)
            conta.Titular.Profissao = profisao;

        _listaDeContas.Add(conta);

        Console.WriteLine("... Conta cadastrada com sucesso! ...");
        Console.ReadKey();
    }
}
