using System;
using Gtk;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;

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
		mySqlCommand.CommandText = "select * from articulo";
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();



		//extraer columnas con el metodo getColumNames de abajo
		string[] columnas = getColumnNames (mySqlDataReader);

		//añadimos columnas
		for (int i = 0; i < columnas.Length; i ++) {
			treeView.AppendColumn (columnas[i].ToString(), new CellRendererText (), "text", i);
		}
		//extraemos solo id , nombre 
		//treeView.AppendColumn ("id", new CellRendererText (), "text" , 0);
		//treeView.AppendColumn ("nombre", new CellRendererText (), "text" , 1);

		//establezco el modelo
		Type[] types = getTypes (mySqlDataReader.FieldCount);
		ListStore listStore = new ListStore (types);
		
		//mostramos los datos de la BD en el treeView
			while (mySqlDataReader.Read()) {
				//extraemos los valies con el metodo getValues de abajo
				string[] values = getValues (mySqlDataReader);
				listStore.AppendValues (values);
				//en el caso de que solo fuera id y nombre y sin el for
				//listStore.AppendValues(mySqlDataReader[0].toString(),mySqlDataReader[1].toString());
			}


		mySqlDataReader.Close ();

		treeView.Model = listStore;

		mySqlConnection.Close ();

//		//añadimos columnas
//		
//		treeView.AppendColumn ("categoria", new CellRendererText (), "text" , 2);
//		treeView.AppendColumn ("precio", new CellRendererText (), "text" , 3);
//		//establezco el modelo
//		ListStore listStore1 = new ListStore(typeof(String), typeof(String));
//		//alternativa : ListStore listStore = new ListStore(typeof(String), typeof(String));
//		//TODO rellenar listStore
//		listStore.AppendValues ("1L" , "Nombre del primero");
//		treeView.Model = listStore;
//		string[] values = new string[2];
//		values [0] = "2L";
//		values [1] = "Nombre del segundo";
//		listStore.AppendValues (values);

	}
	//metodo que devuelve el nombre de las columnas
	private string[] getColumnNames(MySqlDataReader mySqlDataReader) {
			int count = mySqlDataReader.FieldCount;
			List<string> columNames = new List<string> ();
			for (int index = 0; index < count; index++)
				columNames.Add (mySqlDataReader.GetName (index));
			return columNames.ToArray ();
		}

	//metodo que devuelve los tipos 
	private Type[] getTypes(int count){
		List<Type> types = new List<Type> ();
		for (int i = 0; i < count; i ++) {
			types.Add(typeof(string));
		}
		return types.ToArray ();
	}


	private string[] getValues(MySqlDataReader mySqlDataReader){
		List<string> values = new List<string> ();
		int count = mySqlDataReader.FieldCount;
		for (int i = 0; i < count; i++) {
			values.Add(mySqlDataReader[i].ToString());
		}
		return values.ToArray ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a){
			Application.Quit ();
			a.RetVal = true;
	}

}


