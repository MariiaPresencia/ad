using System;
using Gtk;
using System.Collections;

namespace SerpisAd
{
	public class TreeViewHelper
	{
		public static void Fill(TreeView treeView , QueryResult queryResult){
			string[] columnas = queryResult.ColumnNames;
			CellRendererText cellRendererText = new CellRendererText ();
			//añadimos columnas
			for (int i = 0; i < columnas.Length; i ++) {
				//otra manera de añadir columnas con un delegate
				int column = i;
				treeView.AppendColumn (columnas [i], cellRendererText,
				                       delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
					IList row = (IList)tree_model.GetValue(iter,0);
					cellRendererText.Text = row[column].ToString();
				});
			}

			ListStore listStore = new ListStore (typeof(IList));
			foreach (IList row in queryResult.Rows) {
				listStore.AppendValues (row);
			}
			treeView.Model = listStore;
		}

	}
}

