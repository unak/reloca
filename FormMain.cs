using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reloca
{
	public partial class FormMain : Form
	{
		public delegate bool EnumWindowsDelegate(IntPtr hwnd, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public extern static bool EnumWindows(EnumWindowsDelegate callback, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public extern static IntPtr WindowFromPoint(NativeWindowModoki.POINT point);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public extern static bool MoveWindow(IntPtr hwnd, int x, int y, int width, int height, bool repaint);

		private int screenCount = 0;
		private int currentScreen = -1; // -1 means that not initialized
		private List<NativeWindowModoki> windows = new List<NativeWindowModoki>();
		private double scale;
		private NativeWindowModoki showing = null;
		private SaveData saveData = new SaveData();

		public FormMain()
		{
			InitializeComponent();

			this.ActiveControl = btnRelocate;

			ClearInformation();
			SetScreenCount();
		}

		protected override void WndProc(ref Message m)
		{
			const int WM_DISPLAYCHANGED = 0x007e;
			switch (m.Msg)
			{
				case WM_DISPLAYCHANGED:
					FormMain_DisplayChanged();
					break;
			}
			base.WndProc(ref m);
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			SetupPictureBox();
			pbxScreen.Refresh();
		}

		private void FormMain_DisplayChanged()
		{
			SetScreenCount();
		}

		private void btnPrevScreen_Click(object sender, EventArgs e)
		{
			if (currentScreen > 0)
			{
				currentScreen--;
				UpdateScreenInfo();
			}
		}

		private void btnNextScreen_Click(object sender, EventArgs e)
		{
			if (currentScreen < screenCount - 1)
			{
				currentScreen++;
				UpdateScreenInfo();
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			UpdateScreenInfo();
		}

		private void cbxLocation_CheckedChanged(object sender, EventArgs e)
		{
			btnSave.Enabled = cbxLocation.Checked || cbxSize.Checked;
		}

		private void cbxSize_CheckedChanged(object sender, EventArgs e)
		{
			btnSave.Enabled = cbxLocation.Checked || cbxSize.Checked;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveSetting();
		}

		private void btnRelocate_Click(object sender, EventArgs e)
		{
			Relocate();
		}

		private void pbxScreen_Paint(object sender, PaintEventArgs e)
		{
			foreach (var window in windows)
			{
				var x = (float)(window.Left / scale);
				var y = (float)(window.Top / scale);
				var width = (float)(window.Width / scale);
				var height = (float)(window.Height / scale);
				e.Graphics.FillRectangle(Brushes.Gray, x, y, width, height);
				e.Graphics.DrawRectangle(Pens.DarkGray, x, y, width, height);
				e.Graphics.DrawString(window.Text, SystemFonts.DefaultFont, Brushes.Black, x+1, y+1);
			}
		}

		private void pbxScreen_MouseClick(object sender, MouseEventArgs e)
		{
			var found = showing == null;
			NativeWindowModoki first = null;
			foreach (var window in Enumerable.Reverse(windows))
			{
				var left = (float)(window.Left / scale);
				var top = (float)(window.Top / scale);
				var right = left + (float)(window.Width / scale);
				var bottom = top + (float)(window.Height / scale);
				if (e.Location.X >= left && e.Location.X <= right && e.Location.Y >= top && e.Location.Y <= bottom)
				{
					if (showing == window)
					{
						found = true;
						continue;
					}
					if (!found)
					{
						if (first == null)
						{
							first = window;
						}
						continue;
					}
					ShowInformation(window);
					return;
				}
			}

			if (first != null)
			{
				ShowInformation(first);
			}
		}

		private void SetupPictureBox()
		{
			if (currentScreen < 0 || currentScreen >= Screen.AllScreens.Length)
			{
				return;
			}

			var diff1 = lblScreenName.Top - pbxScreen.Bottom;
			var diff2 = btnPrevScreen.Top - pbxScreen.Bottom;
			var diff3 = cbxTitle.Top - lblScreenName.Bottom;
			var diff4 = cbxClass.Top - cbxTitle.Bottom;
			var diff5 = btnSave.Top - cbxSize.Bottom;
			var diff6 = this.ClientSize.Height - btnSave.Bottom;

			var screen = Screen.AllScreens[currentScreen];
			scale = (double)screen.Bounds.Width / this.ClientSize.Width;
			pbxScreen.Height = (int)(screen.Bounds.Height / scale);

			lblScreenName.Top = pbxScreen.Bottom + diff1;
			btnPrevScreen.Top = btnNextScreen.Top = btnRefresh.Top = pbxScreen.Bottom + diff2;

			var width = btnNextScreen.Right - btnPrevScreen.Left;
			btnPrevScreen.Left = (this.ClientSize.Width - width) / 2;
			btnNextScreen.Left = btnPrevScreen.Left + width - btnNextScreen.Width;

			cbxTitle.Top = lblScreenName.Bottom + diff3;
			cbxClass.Top = cbxTitle.Bottom + diff4;
			cbxProcess.Top = cbxClass.Bottom + diff4;
			cbxLocation.Top = cbxProcess.Bottom + diff4;
			cbxSize.Top = cbxLocation.Bottom + diff4;

			btnSave.Top = btnRelocate.Top = cbxSize.Bottom + diff5;
			

			if (this.Height < btnSave.Bottom + diff6)
			{
				this.Height = btnSave.Bottom + diff6;
			}
		}

		private void SetScreenCount()
		{
			//screenCount = SystemInformation.MonitorCount;
			screenCount = Screen.AllScreens.Length;
			if (currentScreen >= screenCount || currentScreen < 0)
			{
				currentScreen = 0;
				UpdateScreenInfo();
			}
			btnPrevScreen.Enabled = currentScreen > 0;
			btnNextScreen.Enabled = currentScreen < screenCount - 1;
		}

		private NativeWindowModoki GetToplevelWindow(IntPtr hwnd)
		{
			if (hwnd == IntPtr.Zero)
			{
				return null;
			}

			var tgt = new NativeWindowModoki(hwnd);
			while (tgt != null && (tgt.Style & NativeWindowModoki.WS_CHILD) != 0)
			{
				tgt = tgt.Parent;
			}
			return tgt;
		}

		private void UpdateScreenInfo()
		{
			lblScreenName.Text = string.Format("Screen {0}", currentScreen + 1);

			SetupPictureBox();

			windows.Clear();
			EnumWindows((hwnd, lParam)=>{
				var window = new NativeWindowModoki(hwnd);
				if (window.Visible && (window.Style & NativeWindowModoki.WS_CHILD) == 0 &&
					window.Width > 0 && window.Height > 0 &&
					window.Text != "Program Manager")
				{
					windows.Add(window);
				}
				return true;
			}, IntPtr.Zero);

			foreach (var window in windows)
			{
				NativeWindowModoki.POINT[] points =
				{
					new NativeWindowModoki.POINT { x = window.Left, y = window.Top },
					new NativeWindowModoki.POINT { x = window.Right, y = window.Top },
					new NativeWindowModoki.POINT { x = window.Left, y = window.Bottom },
					new NativeWindowModoki.POINT { x = window.Right, y = window.Bottom },
				};
				for (var i = 0; i < points.Length; i++)
				{
					var tgt = GetToplevelWindow(WindowFromPoint(points[i]));
					if (tgt != null)
					{
						window.FrontWindows[i] = tgt.Handle;
					}
				}

				// top side
				if (window.FrontWindows[0] == IntPtr.Zero && window.FrontWindows[1] == IntPtr.Zero)
				{
					var tgt1 = GetToplevelWindow(WindowFromPoint(new NativeWindowModoki.POINT { x = window.Left, y = window.Top - 1 }));
					var tgt2 = GetToplevelWindow(WindowFromPoint(new NativeWindowModoki.POINT { x = window.Right, y = window.Top - 1 }));
					if (tgt1 != null && tgt2 != null && tgt1.Handle == tgt2.Handle)
					{
						window.BackWindows[0] = tgt1.Handle;
					}
				}

				// bottom side
				if (window.FrontWindows[2] == IntPtr.Zero && window.FrontWindows[3] == IntPtr.Zero)
				{
					var tgt1 = GetToplevelWindow(WindowFromPoint(new NativeWindowModoki.POINT { x = window.Left, y = window.Top + 1 }));
					var tgt2 = GetToplevelWindow(WindowFromPoint(new NativeWindowModoki.POINT { x = window.Right, y = window.Top + 1 }));
					if (tgt1 != null && tgt2 != null && tgt1.Handle == tgt2.Handle)
					{
						window.BackWindows[1] = tgt1.Handle;
					}
				}

				// left side
				if (window.FrontWindows[0] == IntPtr.Zero && window.FrontWindows[2] == IntPtr.Zero)
				{
					var tgt1 = GetToplevelWindow(WindowFromPoint(new NativeWindowModoki.POINT { x = window.Left - 1, y = window.Top }));
					var tgt2 = GetToplevelWindow(WindowFromPoint(new NativeWindowModoki.POINT { x = window.Right - 1, y = window.Top }));
					if (tgt1 != null && tgt2 != null && tgt1.Handle == tgt2.Handle)
					{
						window.BackWindows[2] = tgt1.Handle;
					}
				}

				// right side
				if (window.FrontWindows[1] == IntPtr.Zero && window.FrontWindows[3] == IntPtr.Zero)
				{
					var tgt1 = GetToplevelWindow(WindowFromPoint(new NativeWindowModoki.POINT { x = window.Left + 1, y = window.Top }));
					var tgt2 = GetToplevelWindow(WindowFromPoint(new NativeWindowModoki.POINT { x = window.Right + 1, y = window.Top }));
					if (tgt1 != null && tgt2 != null && tgt1.Handle == tgt2.Handle)
					{
						window.BackWindows[3] = tgt1.Handle;
					}
				}
			}
			windows.Sort((a, b) =>
			{
				if (a.FrontWindows.Contains(b.Handle) || b.BackWindows.Contains(a.Handle))
				{
					return -1;
				}
				else if (b.FrontWindows.Contains(a.Handle) || a.BackWindows.Contains(b.Handle))
				{
					return 1;
				}
				else
				{
					return 0;
				}
			});

			ClearInformation();

			pbxScreen.Refresh();
		}

		private void ClearInformation()
		{
			showing = null;
			cbxTitle.Text = "Title: ";
			cbxClass.Text = "Class: ";
			cbxProcess.Text = "Process: ";
			cbxLocation.Text = "Location: ";
			cbxSize.Text = "Size: ";

			cbxTitle.Checked = false;
			cbxClass.Checked = false;
			cbxLocation.Checked = false;
			cbxSize.Checked = false;

			cbxTitle.Enabled = false;
			cbxClass.Enabled = false;
			cbxLocation.Enabled = false;
			cbxSize.Enabled = false;
			btnSave.Enabled = false;
		}

		private int GetSettingIndex(string title, string className, string process)
		{
			for (var i = 0; i < saveData.Settings.Count; i++)
			{
				var setting = saveData.Settings[i];
				if (setting.process != process)
				{
					continue;
				}
				if (!string.IsNullOrEmpty(setting.title) && setting.title != title)
				{
					continue;
				}
				if (!string.IsNullOrEmpty(setting.className) && setting.className != className)
				{
					continue;
				}

				return i;
			}

			return -1;
		}

		private bool LoadSetting(string title, string className, string process)
		{
			var index = GetSettingIndex(title, className, process);
			if (index >= 0)
			{
				var setting = saveData.Settings[index];
				cbxTitle.Checked = !string.IsNullOrEmpty(setting.title);
				cbxClass.Checked = !string.IsNullOrEmpty(setting.className);
				cbxLocation.Checked = setting.location;
				cbxSize.Checked = setting.size;

				return true;
			}
			else
			{
				return false;
			}
		}

		private void SaveSetting()
		{
			if (showing == null)
			{
				return;
			}

			var setting = new SaveData.Setting();
			if (cbxTitle.Checked)
			{
				setting.title = showing.Text;
			}
			if (cbxClass.Checked)
			{
				setting.className = showing.ClassName;
			}
			setting.process = showing.ProcessName;
			setting.location = cbxLocation.Checked;
			if (setting.location)
			{
				setting.x = showing.Left;
				setting.y = showing.Top;
			}
			setting.size = cbxSize.Checked;
			if (setting.size)
			{
				setting.width = showing.Width;
				setting.height = showing.Height;
			}

			var index = GetSettingIndex(showing.Text, showing.ClassName, showing.ProcessName);
			if (index >= 0)
			{
				saveData.Settings[index] = setting;
			}
			else
			{
				saveData.Settings.Add(setting);
			}

			saveData.Save();
		}

		private void ShowInformation(NativeWindowModoki window)
		{
			showing = window;

			if (string.IsNullOrEmpty(window.Text))
			{
				cbxTitle.Text = "Title: (noname)";
			}
			else
			{
				cbxTitle.Text = "Title: " + window.Text;
			}
			cbxClass.Text = "Class: " + window.ClassName;
			cbxProcess.Text = "Process: " + window.ProcessName;
			cbxLocation.Text = string.Format("Location: {0}, {1}", window.Left, window.Top);
			cbxSize.Text = string.Format("Size: {0} x {1}", window.Width, window.Height);

			if (!LoadSetting(window.Text, window.ClassName, window.ProcessName))
			{
				cbxTitle.Checked = false;
				cbxClass.Checked = false;
				cbxLocation.Checked = false;
				cbxSize.Checked = false;
			}

			cbxTitle.Enabled = true;
			cbxClass.Enabled = true;
			cbxLocation.Enabled = true;
			cbxSize.Enabled = true;
			btnSave.Enabled = cbxLocation.Checked || cbxSize.Checked;
		}

		private void Relocate()
		{
			foreach (var setting in saveData.Settings)
			{
				foreach (var target in windows.Where((window) =>
				{
					if ((string.IsNullOrEmpty(setting.title) || setting.title == window.Text) &&
						(string.IsNullOrEmpty(setting.className) || setting.className == window.ClassName) &&
						setting.process == window.ProcessName)
					{
						return true;
					}
					else
					{
						return false;
					}
				}))
				{
					int x;
					int y;
					int width;
					int height;
					if (setting.location)
					{
						x = setting.x;
						y = setting.y;
					}
					else
					{
						x = target.Left;
						y = target.Top;
					}
					if (setting.size)
					{
						width = setting.width;
						height = setting.height;
					}
					else
					{
						width = target.Width;
						height = target.Height;
					}
					MoveWindow(target.Handle, x, y, width, height, false);
				}
			}
		}
	}
}
