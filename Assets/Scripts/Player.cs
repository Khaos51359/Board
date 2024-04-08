using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentStep { get; private set; }
    public int ID;
    public string Name;
    public int Diamond { get; private set; }
    public Color Color;

    public static event Action OnTurnFinished;
    public static event Action<Player> OnPlayerSpawned;

    [SerializeField]
    public SpriteRenderer _spriteRenderer;

    public SpriteRenderer PlayerRenderer
    {
        get { return _spriteRenderer; }
        private set { _spriteRenderer = value; }
    }

    private void Start()
    {
        Diamond = 0;
        CurrentStep = -1;
        OnPlayerSpawned?.Invoke(this);
    }

    public void AddDiamond(int value)
    {
        Diamond += value;
    }

    public void Move(Tile destinationTile)
    {
        CurrentStep = destinationTile.ID;
        transform.position = destinationTile.transform.position;

        StartCoroutine(Delay(2));
    }

    private IEnumerator Delay(float amount)
    {
        yield return new WaitForSeconds(amount);
        OnTurnFinished?.Invoke();
    }

    public void Move(Transform destination)
    {
        transform.position = destination.position;
    }
}
