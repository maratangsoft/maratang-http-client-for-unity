using System;
using UnityEngine;

[Serializable]
public class Song
{
	[SerializeField] private string brand;
	[SerializeField] private string no;
	[SerializeField] private string title;
	[SerializeField] private string singer;
	[SerializeField] private string composer;
	[SerializeField] private string lyricist;
	[SerializeField] private string release;

	public string Brand { get => brand; }
	public string No { get => no; }
	public string Title { get => title; }
	public string Singer { get => singer; }
	public string Composer { get => composer; }
	public string Lyricist { get => lyricist; }
	public string Release { get => release; }
}

