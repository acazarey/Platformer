using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
	public static PointManager Instance {get; private set;}
	
	public static event Action<int> OnPointChanged;
	private int points;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void AddPoint(int amount)
	{
		points += amount;
		OnPointChanged?.Invoke(points);
	}

	public void ResetPoints()
	{
		points = 0;
		OnPointChanged?.Invoke(points);
	}

	public int GetPoints() => points;
}
