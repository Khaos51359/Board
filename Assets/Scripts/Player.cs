using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentStep { get; private set; }
    public int ID;
    public string Name;
    public int Diamond { get; private set; }
    public Color Color;

    private List<string> _niceThings = new List<string>();

    public List<string> NiceThings { get; private set; }

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
        NiceThings = _niceThings;
    }

    public void AddDiamond(int value)
    {
        Diamond += value;
        Debug.Log("Player " + ID + " dimaond changed to " + Diamond);
    }

    public int TakeDiamond(int value)
    {
        int substractedDiamond = UnityEngine.Mathf.Clamp(Diamond - value, 0, int.MaxValue);
        Diamond -= substractedDiamond;
        Debug.Log("Player " + ID + " diamond taken, currently = " + Diamond);
        return substractedDiamond;
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

    public void WriteNiceThing(string niceThing)
    {
        _niceThings.Add(niceThing);
    }

    public string TakeNiceThing(out bool successTakeNiceThing)
    {
        int randomNiceThingIndex = UnityEngine.Random.Range(0, _niceThings.Count);
        string niceThing = string.Empty;

        if (_niceThings.Count > 0)
        {
            niceThing = _niceThings[randomNiceThingIndex];
            _niceThings.RemoveAt(randomNiceThingIndex);
            successTakeNiceThing = true;
        }
        else
        {
            successTakeNiceThing = false;
        }
        return niceThing;
    }
}
