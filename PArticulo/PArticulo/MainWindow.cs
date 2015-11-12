using System;
using Gtk;
using System.Collections;

using SerpisAd;
using PArticulo;

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
	public bool ConfirmDelete(Window window){
		//TODO lozalizacion del ¿Quieres eliminar...
		MessageDialog messageDialog = new MessageDialog (window, DialogFlags.DestroyWithParent, MessageType.Question, ButtonsType.YesNo, "¿Quieres eliminar el elemento seleccionado?");
		messageDialog.Title = window.Title;
		ResponseType response = (ResponseType)messageDialog.Run ();
		messageDialog.Destroy ();
		return response == ResponseType.Yes;
	
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
		if (ConfirmDelete (this))
			return;

	}



}


