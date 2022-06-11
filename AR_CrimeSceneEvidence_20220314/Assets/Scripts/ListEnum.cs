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

	public enum TypeChooseTool
	{
		None, FlashLight, EvidenceBag, DNA, Scale, FingerPrint, Camera
	}

	//public class EnumFlagsAttribute : PropertyAttribute
	//{
	//	public EnumFlagsAttribute() { }
	//}

	//[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
	//public class EnumFlagsAttributeDrawer : PropertyDrawer
	//{
	//	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	//	{
	//		// base.OnGUI(position, property, label);

	//		property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
	//	}
	//}
}