using System;
using Gtk;

using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWindow ctor.");

		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult);
	}



	protected void OnDeleteEvent (object sender, DeleteEventArgs a){
			Application.Quit ();
			a.RetVal = true;
	}

}


