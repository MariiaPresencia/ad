using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWindow ctor.");
		IDbConnection dbConnection = new MySqlConnection (
			"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas"
		);
		dbConnection.Open ();
		
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from articulo";
		IDataReader dataReader = dbCommand.ExecuteReader ();



		//extraer columnas con el metodo getColumNames de abajo
		string[] columnas = getColumnNames (dataReader);

		//añadimos columnas
		for (int i = 0; i < columnas.Length; i ++) {
			treeView.AppendColumn (columnas[i].ToString(), new CellRendererText (), "text", i);
		}
		//extraemos solo id , nombre 
		//treeView.AppendColumn ("id", new CellRendererText (), "text" , 0);
		//treeView.AppendColumn ("nombre", new CellRendererText (), "text" , 1);

		//establezco el modelo
		Type[] types = getTypes (dataReader.FieldCount);
		ListStore listStore = new ListStore (types);
		
		//mostramos los datos de la BD en el treeView
			while (dataReader.Read()) {
				//extraemos los valies con el metodo getValues de abajo
				string[] values = getValues (dataReader);
				listStore.AppendValues (values);
				//en el caso de que solo fuera id y nombre y sin el for
				//listStore.AppendValues(dataReader[0].toString(),dataReader[1].toString());
			}


		dataReader.Close ();

		treeView.Model = listStore;

		dbConnection.Close ();

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
	private string[] getColumnNames(IDataReader dataReader) {
			int count = dataReader.FieldCount;
			List<string> columNames = new List<string> ();
			for (int index = 0; index < count; index++)
				columNames.Add (dataReader.GetName (index));
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


	private string[] getValues(IDataReader dataReader){
		List<string> values = new List<string> ();
		int count = dataReader.FieldCount;
		for (int i = 0; i < count; i++) {
			values.Add(dataReader[i].ToString());
		}
		return values.ToArray ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a){
			Application.Quit ();
			a.RetVal = true;
	}

}


