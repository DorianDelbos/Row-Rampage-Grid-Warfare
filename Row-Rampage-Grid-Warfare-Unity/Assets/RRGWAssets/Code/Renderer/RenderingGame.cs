using Algorithm;
using UnityEngine;

public abstract class RenderingGame : MonoBehaviour
{
    [SerializeField] protected BoardManager boardManager;

    public abstract void UpdateRenderer(Board board);

    protected abstract void InstantiateToken(Board.State state, Vector2Int position);
}
