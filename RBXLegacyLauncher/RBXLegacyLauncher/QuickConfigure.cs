﻿/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 6/13/2017
 * Time: 4:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace RBXLegacyLauncher
{
	/// <summary>
	/// Description of QuickConfigure.
	/// </summary>
	public partial class QuickConfigure : Form
	{
		public QuickConfigure()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void QuickConfigureLoad(object sender, EventArgs e)
		{
			string cfgpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\config.txt";
			if (!File.Exists(cfgpath))
			{
				LauncherFuncs.WriteConfigValues(cfgpath);
			}
			else
			{
				ReadConfigValues(cfgpath);
			}
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			GeneratePlayerID();		
		}
		
		void ReadConfigValues(string cfgpath)
		{
			LauncherFuncs.ReadConfigValues(cfgpath);
			
			if (GlobalVars.UserID == 0)
			{
				GeneratePlayerID();
				LauncherFuncs.WriteConfigValues(cfgpath);
			}
			else
			{
				textBox2.Text = GlobalVars.UserID.ToString();
			}
			
			textBox1.Text = GlobalVars.PlayerName;
		}
		
		void GeneratePlayerID()
		{
			LauncherFuncs.GeneratePlayerID();
			textBox2.Text = GlobalVars.UserID.ToString();
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.PlayerName = textBox1.Text;
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			int parsedValue;
			if (int.TryParse(textBox2.Text, out parsedValue))
			{
				if (textBox2.Text.Equals(""))
				{
					GlobalVars.UserID = 0;
				}
				else
				{
					GlobalVars.UserID = Convert.ToInt32(textBox2.Text);
				}
			}
			else
			{
				GlobalVars.UserID = 0;
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			CharacterCustomization ccustom = new CharacterCustomization();
			ccustom.Show();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			string cfgpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\config.txt";
    		base.OnFormClosing(e);
    		LauncherFuncs.WriteConfigValues(cfgpath);
    		GlobalVars.ReadyToLaunch = true;
		}
	}
}
