﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpreadsheetGui;

namespace SpreadsheetGUI
{
	public partial class SpreadsheetWindow : Form, ISpreadsheetView
	{

		StringBuilder colRefference = new StringBuilder("ABCDEFGHIJKLMNOPQRSTUZWXYZ");

		public event Action<string> cellHighlighted;
		public event Action<string> loadSS;
		public event Action saveSS;
		public event Action closeSS;
		public event Action<string, string> cellWithNameChagendContents;

		public string message { set { MessageBox.Show(value); } }

		public string currentValue {
			set {
				ValueBox.Text = value;

			}
		}

		public string currentContents { set { ContentBox.Text = value; } }

		public string currentName { set { AddressBox.Text = value; } }

		public Dictionary<string, string> toUpdate
		{
			set
			{
				UpdateAll(value);
			}
		}

		

		/// <summary>
		///any time this value changes we update all cells that changed.
		/// </summary>
		/// <value>
		/// The cells2 change.
		/// </value>

		/// <summary>
		/// Initializes a new instance of the <see cref="SpreadsheetWindow"/> class.
		/// </summary>
		public SpreadsheetWindow()
		{
			InitializeComponent();
			spreadsheetCellArray.SelectionChanged += HandleSelectionChanged;
		}

		private void HandleSelectionChanged(SpreadsheetPanel ss)
		{

			//pull the name of sellection and fires cellhighlighted event.
			int row, col;
			ss.GetSelection(out col, out row);
			var val = colRefference[col] + (row + 1).ToString();
			cellHighlighted(val);


		}

		private void UpdateAll(Dictionary<string, string> value)
		{
			string temp;
			foreach (string s in value.Keys)
			{
				value.TryGetValue(s, out temp);
				int[] coords = convertNameToCoords(s);
				spreadsheetCellArray.SetValue(coords[0], coords[1], temp);
			}
		}

		private int[] convertNameToCoords(string s)
		{
			var toReturn = new int[2];
			int i = 0;
			while (s.Substring(0, 1) != "" + colRefference[i])
			{
			i++;
				
			}
			toReturn[0] = i;
			int j;
			int.TryParse(s.Substring(1), out j);
			toReturn[1] = j - 1;
			return toReturn;
		}

	private void ContentBox_Keypress(object sender, KeyPressEventArgs e)
		{
			int col, row;
			spreadsheetCellArray.GetSelection(out col, out row);
			var name =  colRefference[col] + (row + 1).ToString();
			if (((char)Keys.Enter).Equals(e.KeyChar))
			{
				cellWithNameChagendContents(name, ContentBox.Text);
			}

		}
		/// <summary>
		/// Handles the Load event of the spreadsheetPanel1 control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void spreadsheetPanel1_Load(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Handles the ItemClicked event of the menuStrip1 control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}


		private void AdressBoxClick(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Handles the Load event of the spreadsheetCellArray control. Load and New will access this method.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void spreadsheetCellArray_Load(object sender, EventArgs e)
		{
			spreadsheetCellArray.Select();
		}

		private void ClickOnCells(object sender, EventArgs e)
		{
		}
		
	
		
		private void ex(object sender, EventArgs e)
		{
			
		}

		private void HandleSelectNew(object sender, ToolStripItemClickedEventArgs e)
		{
			SpreadsheetApplicationContext.GetContext().RunNew();
		}

		private void LoadSelected(object sender, ToolStripItemClickedEventArgs e)
		{
			
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				loadSS(openFileDialog1.FileName);
					
			}
		}

		private void SaveSelected(object sender, ToolStripItemClickedEventArgs e)
		{
			saveSS();
		}

		private void CloseSelected(object sender, ToolStripItemClickedEventArgs e)
		{
			closeSS();
		}
	}
}
