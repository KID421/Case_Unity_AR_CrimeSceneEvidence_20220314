using System;
using UnityEngine;
using UnityEditor;

namespace KID
{
	/// <summary>
	/// 物件類型
	/// </summary>
	[Flags]
	public enum TypeObjectToCheck
	{
		None = 0, FlashLight = 1, EvidenceBag = 2, DNA = 4, Scale = 8, FingerPrint = 16, Camera = 32
	}

	public enum TypeEvidence
	{
		None, FlashLight, EvidenceBag, DNA, Scale, FingerPrint, Camera
	}

	/// <summary>
	/// 題目類型：無題目、文字、圖片、文字先再圖片
	/// </summary>
	public enum TypeQuestion
	{
		None, Text, Image, TextFirstThanImage
	}
}