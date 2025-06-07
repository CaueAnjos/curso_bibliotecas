using bytebank.Modelos.ADM.SistemaInterno;

namespace bytebank.Modelos.ADM.Utilitario;

public class ParceiroComercial : IAutenticavel
{
    public Senha Senha { get; set; } = new Senha();

    public bool Autenticar(Senha senha)
    {
        return Senha.Equals(senha);
    }
}
