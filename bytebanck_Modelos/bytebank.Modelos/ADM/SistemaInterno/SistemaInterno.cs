namespace bytebank.Modelos.ADM.SistemaInterno;

public class SistemaInterno
{
    public bool Logar(IAutenticavel funcionario, string senha)
    {
        bool usuarioAutenticado = funcionario.Autenticar(senha);

        if (usuarioAutenticado)
            Console.WriteLine("Bem-vindo ao sistema!");
        else
            Console.WriteLine("Senha incorreta!");

        return usuarioAutenticado;
    }
}
