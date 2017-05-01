using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reloca
{
	public class SaveData : ApplicationSettingsBase
	{
		public struct Setting
		{
			public string title;
			public string className;
			public string process;
			public int x;
			public int y;
			public int width;
			public int height;
			public bool location;
			public bool size;
			public bool titleRegex;
			public bool classRegex;
		}

		[UserScopedSetting()]
		public List<Setting> Settings
		{
			get
			{
				if ((List<Setting>)this["Settings"] == null)
				{
					this["Settings"] = new List<Setting>();
				}
				return (List<Setting>)this["Settings"];
			}
			set
			{
				this["Settings"] = value;
			}
		}
	}
}
