using System.Linq;

public class Player
{
    public Player()
    {
        name = "Joe";
        isAI = false;
        token = GameAssets.instance.tokens.FirstOrDefault();
    }

    public string name;
    public bool isAI;
    public Token token;
}
