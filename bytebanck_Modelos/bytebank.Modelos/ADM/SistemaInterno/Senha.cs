namespace bytebank.Modelos.ADM.SistemaInterno;

public class Senha
{
    public Senha(string? senha = null)
    {
        _senha = senha;
    }

    private string? _senha;

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return _senha is null;

        if (obj is Senha senha)
            return senha._senha == _senha;

        return _senha == obj.ToString();
    }

    public override int GetHashCode()
    {
        return _senha?.GetHashCode() ?? 0;
    }

    public override string ToString()
    {
        return _senha ?? string.Empty;
    }
}
