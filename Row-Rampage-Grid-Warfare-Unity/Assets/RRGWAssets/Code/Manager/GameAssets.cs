using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load("GameAssets") as GameObject).GetComponent<GameAssets>();

            return _instance;
        }
    }

    public Token[] tokens;

    public FMODUnity.EventReference playMoveSound;
    public FMODUnity.EventReference jamMusic;
}
