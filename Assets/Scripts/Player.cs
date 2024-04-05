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
        OnPlayerSpawned?.Invoke(this);
    }

    public Player()
    {
        Diamond = 0;
        CurrentStep = 0;
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
