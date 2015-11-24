using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;


namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private object id = null;
		private string nombre = "";
		private object categoria = null;
		private decimal precio = 0;
		System.Action save;

		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{
			init ();
			saveAction.Activated += delegate {insert();};
		}
		public ArticuloView(object id) :base(WindowType.Toplevel) {
			this.id = id;
			load ();
			init ();
			saveAction.Activated += delegate {update();};
		}

		private void init(){
			this.Build ();
			entryNombre.Text = nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult, categoria);
			spinButtonPrecio.Value = Convert.ToDouble(precio);
			//saveAction.Activated += delegate {	save();	};
		}

		private void load(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter(dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if (!dataReader.Read ())
				return;
			nombre = (string)dataReader ["nombre"];
			categoria = dataReader ["categoria"];
			if (categoria is DBNull)
				categoria = null;
			precio = (decimal)dataReader ["precio"];
			dataReader.Close ();
		}

//		private void save() {
//			if (id == null)
//				insert ();
//			else
//				update ();
//		}

		public void insert(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";
			nombre = entryNombre.Text;
			categoria = ComboBoxHelper.GetId(comboBoxCategoria);
			precio = Convert.ToDecimal(spinButtonPrecio.Value);

			DbCommandHelper.AddParameter(dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter(dbCommand,"categoria", categoria);
			DbCommandHelper.AddParameter(dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

		private void update() {
			Console.WriteLine ("update");
		}

	}
}

