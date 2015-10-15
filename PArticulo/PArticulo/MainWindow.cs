using System;
using Gtk;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWindow ctor.");

		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");

		//extraer columnas 
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
			treeView.AppendColumn (columnas[i].ToString(), new CellRendererText (), "text", i);
		}

		ListStore listStore = new ListStore (typeof(IList));
		foreach (IList row in queryResult.Rows) {
			listStore.AppendValues (row);
		}
		treeView.Model = listStore;

	}



	protected void OnDeleteEvent (object sender, DeleteEventArgs a){
			Application.Quit ();
			a.RetVal = true;
	}

}


