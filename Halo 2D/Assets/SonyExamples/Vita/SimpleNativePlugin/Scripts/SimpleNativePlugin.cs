using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

class SimpleNativePlugin : MonoBehaviour
{
	[DllImport("NativePluginExample")]
	private static extern int GetInteger();
	
	[DllImport("NativePluginExample")]
	private static extern IntPtr GetString();
	
	[DllImport("NativePluginExample")]
	private static extern int AddTwoIntegers(int i1, int i2);
	
	[DllImport("NativePluginExample")]
	private static extern float AddTwoFloats(float f1, float f2);

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
	private struct ReturnedStructure
	{
		public int number;
		IntPtr _text;
		public string text { get { return Marshal.PtrToStringAnsi(_text); } }
	};

	[DllImport("NativePluginExample")]
	private static extern bool ReturnAStructure(out ReturnedStructure data);

	private string infoText;
	private int infoCount = 0;
	ReturnedStructure returnedStructure;
	
	void OnGUI()
	{
		GUI.TextArea(new Rect(0,0,Screen.width-1,Screen.height-64), infoText);
		GUI.TextArea(new Rect(0,Screen.height-32,Screen.width,31), infoCount.ToString());
	}

	void Update()
	{
		infoCount ++;
	}

	void Start()
	{
		infoText += "GetNumber: " + GetInteger();
		infoText += "\nGetString: " + StringFromNativeAscii(GetString());
		infoText += "\nAddTwoIntegers: " + AddTwoIntegers(3,4);
		infoText += "\nAddTwoFloats: " + AddTwoFloats(1.0f, 2.0f);

		returnedStructure = new ReturnedStructure();
		ReturnAStructure(out returnedStructure);
		infoText += "\nReturnedStructure: " + returnedStructure.text + ", " + returnedStructure.number;
	}

	public static string StringFromNativeAscii(IntPtr nativeUtf8)
	{
		var len = 0;
		while (Marshal.ReadByte(nativeUtf8, len) != 0) 
			++len;
		if (len == 0) 
			return string.Empty;
		var buffer = new byte[len];
		Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
		return Encoding.ASCII.GetString(buffer);
	}
}
