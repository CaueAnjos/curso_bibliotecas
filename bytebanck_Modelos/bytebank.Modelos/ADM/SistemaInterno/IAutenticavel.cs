namespace bytebank.Modelos.ADM.SistemaInterno;

public interface IAutenticavel
{
    bool Autenticar(Senha senha);
}
