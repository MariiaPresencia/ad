using System;
using Gtk;
using MySql.Data.MySqlClient;


public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWindow ctor.");
		MySqlConnection mySqlConnection = new MySqlConnection (
			"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas"
			);
		mySqlConnection.Open ();
		
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "select id, nombre from articulo";
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

		//establezco el modelo
		ListStore listStore = new ListStore(typeof(long), typeof(String));

		//añadimos columnas
		treeView.AppendColumn ("id", new CellRendererText (), "text" , 0);
		treeView.AppendColumn ("nombre", new CellRendererText (), "text" , 1);

		//mostramos los datos de la BD en el treeView
		while (mySqlDataReader.Read()) {
			listStore.AppendValues(mySqlDataReader[0], mySqlDataReader[1]);
		}

		treeView.Model = listStore;
		mySqlDataReader.Close ();
		mySqlConnection.Close ();

		//mostrar todos los datos de la BD cuando cambiamos a select * from articulo




		//vamos a rellenar el TreeVeiw con los datos de articulos [BD]



//		//establezco el modelo
//		ListStore listStore1 = new ListStore(typeof(String), typeof(String));
//		//alternativa : ListStore listStore = new ListStore(typeof(String), typeof(String));
//		//TODO rellenar listStore
//		listStore.AppendValues ("1L" , "Nombre del primero");
//		treeView.Model = listStore;
//
//
//		string[] values = new string[2];
//		values [0] = "2L";
//		values [1] = "Nombre del segundo";
//		listStore.AppendValues (values);




	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

}
