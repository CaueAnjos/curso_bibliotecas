using bytebank.Modelos.ADM.SistemaInterno;

namespace bytebank.Modelos.ADM.Utilitario;

public class ParceiroComercial : IAutenticavel
{
    public string? Senha { get; set; }
    public bool Autenticar(string senha)
    {
        if (string.IsNullOrEmpty(Senha))
            throw new NullReferenceException("Senha is null");

        return Senha == senha;
    }
}
