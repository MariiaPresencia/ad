using System;
using Gtk;
using System.Collections;

namespace SerpisAd
{
	public class TreeViewHelper
	{
		public static void Fill(TreeView treeView , QueryResult queryResult){
			removeAllColumns (treeView);
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
		private static void removeAllColumns(TreeView treeView) {
			TreeViewColumn[] treeViewColumns = treeView.Columns;
			foreach (TreeViewColumn treeViewColumn in treeViewColumns)
				treeView.RemoveColumn(treeViewColumn); 
		}
		public static object GetId(TreeView treeView){
			TreeIter treeIter;
			if (!treeView.Selection.GetSelected (out treeIter))
				return null;
			IList row = (IList)treeView.Model.GetValue (treeIter, 0);
			return row [0];
		}
		public static bool IsSelected(TreeView treeView){
			TreeIter treeIter;
			return treeView.Selection.GetSelected (out treeIter);
			//o bien
			//return treeView.Selection.CountSelectedRows() != 0;

		}
	}


}

