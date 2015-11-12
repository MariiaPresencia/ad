using System;
using Gtk;
using System.Collections;

using SerpisAd;
using PArticulo;
using System.Data;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Title = "Articulo";
		Console.WriteLine ("MainWindow ctor.");

		fillTreeView ();

		newAction.Activated += delegate {
			new ArticuloView();
		};

		refreshAction.Activated += delegate {
			fillTreeView();
			};

		deleteAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			Console.WriteLine("click en deleteAction id={0}",id);
			delete(id);
			};

		treeView.Selection.Changed += delegate {
			Console.WriteLine("ha ocurrido treeView.Selection.Changed");
			deleteAction.Sensitive = TreeViewHelper.IsSelected(treeView);
		};

		deleteAction.Sensitive = false;

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a){
			Application.Quit ();
			a.RetVal = true;
	}

	private void fillTreeView(){
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult);
	}

	protected void delete (object id)
	{
		if (WindowHelper.ConfirmDelete (this)) {
			Console.WriteLine ("Dice que si a eliminar");
			//hacemos la conexion
			IDbCommand connection = App.Instance.DbConnection.CreateCommand();
			//escribimos la instruccion
			string delete = string.Format ("delete from articulo where id={0}", id);
			//lo llevamos a cabo 
			connection.CommandText = delete;
			connection.ExecuteNonQuery ();

		}
	}



}


