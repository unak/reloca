using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reloca
{
	public class NativeWindowModoki
	{
		public const int WS_CHILD = 0x40000000;
		public const int WS_VISIBLE = 0x10000000;

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public Int32 x;
			public Int32 y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public Int32 left;
			public Int32 top;
			public Int32 right;
			public Int32 bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WINDOWPLACEMENT
		{
			public uint length;
			public uint flags;
			public uint showCmd;
			public POINT ptMinPosition;
			public POINT ptMaxPosition;
			public RECT rcNormalPosition;
		}

		[DllImport("user32.dll", SetLastError = true)]
		public extern static IntPtr GetParent(IntPtr hwnd);

		private const int GWL_EXSTYLE = -20;
		private const int GWL_STYLE = -16;

		[DllImport("user32.dll", SetLastError = true)]
		public extern static Int32 GetWindowLong(IntPtr hwnd, int index);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public extern static bool GetWindowPlacement(IntPtr hwnd, out WINDOWPLACEMENT wp);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public extern static bool GetWindowRect(IntPtr hwnd, out RECT rc);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public extern static int GetWindowText(IntPtr hwnd, StringBuilder text, int length);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public extern static int GetWindowTextLength(IntPtr hwnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public extern static int GetClassName(IntPtr hwnd, StringBuilder text, int length);

		[DllImport("user32.dll", SetLastError = true)]
		static extern uint GetWindowThreadProcessId(IntPtr hwnd, out int pid);

		private IntPtr hwnd;
		private RECT rect;
		private bool haveRect = false;
		private IntPtr[] frontWindows = { IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero };
		private IntPtr[] backWindows = { IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero };

		public IntPtr Handle
		{
			get
			{
				return hwnd;
			}
			private set
			{
				hwnd = value;
			}
		}

		public NativeWindowModoki Parent
		{
			get
			{
				var hwnd = GetParent(Handle);
				return hwnd == IntPtr.Zero ? null : new NativeWindowModoki(hwnd);
			}
		}

		public string Text
		{
			get
			{
				var sb = new StringBuilder(GetWindowTextLength(Handle) + 1);
				GetWindowText(Handle, sb, sb.Capacity);
				return sb.ToString();
			}
		}

		public int Left
		{
			get
			{
				if (!haveRect)
				{
					GetWindowRect(Handle, out rect);
					haveRect = true;
				}
				return (int)rect.left;
			}
		}

		public int Top
		{
			get
			{
				if (!haveRect)
				{
					GetWindowRect(Handle, out rect);
					haveRect = true;
				}
				return (int)rect.top;
			}
		}

		public int Right
		{
			get
			{
				if (!haveRect)
				{
					GetWindowRect(Handle, out rect);
					haveRect = true;
				}
				return (int)rect.right;
			}
		}

		public int Bottom
		{
			get
			{
				if (!haveRect)
				{
					GetWindowRect(Handle, out rect);
					haveRect = true;
				}
				return (int)rect.bottom;
			}
		}

		public int Width
		{
			get
			{
				if (!haveRect)
				{
					GetWindowRect(Handle, out rect);
					haveRect = true;
				}
				return (int)(rect.right - rect.left);
			}
		}

		public int Height
		{
			get
			{
				if (!haveRect)
				{
					GetWindowRect(Handle, out rect);
					haveRect = true;
				}
				return (int)(rect.bottom - rect.top);
			}
		}

		public int Style
		{
			get
			{
				return (int)GetWindowLong(Handle, GWL_STYLE);
			}
		}

		public bool Visible
		{
			get
			{
				return (GetWindowLong(Handle, GWL_STYLE) & WS_VISIBLE) != 0;
			}
		}

		public string ClassName
		{
			get
			{
				var sb = new StringBuilder(1024);
				GetClassName(Handle, sb, sb.Capacity);
				return sb.ToString();
			}
		}

		public string ProcessName
		{
			get
			{
				int pid;
				GetWindowThreadProcessId(Handle, out pid);
				//return Process.GetProcessById(pid).MainModule.FileName;
				return Process.GetProcessById(pid).ProcessName;
			}
		}

		public IntPtr[] FrontWindows
		{
			get
			{
				return frontWindows;
			}
			set
			{
				frontWindows = value;
			}
		}

		public IntPtr[] BackWindows
		{
			get
			{
				return backWindows;
			}
			set
			{
				backWindows = value;
			}
		}

		public NativeWindowModoki(IntPtr hwnd)
		{
			Handle = hwnd;
		}
	}
}
