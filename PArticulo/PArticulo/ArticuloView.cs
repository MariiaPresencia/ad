using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;


namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private string nombre = "";
		private object categoria = null;
		private decimal precio = 0;
		System.Action save;
		private Articulo articulo;

		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{
			init ();
			saveAction.Activated += delegate {insert();};
		}
		public ArticuloView(object id) :base(WindowType.Toplevel) {
			articulo = ArticuloPersister.Load (id);
			init ();
			saveAction.Activated += delegate {update();};
		}

		private void init(){
			this.Build ();
			entryNombre.Text = articulo.Nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult, articulo.Categoria);
			spinButtonPrecio.Value = Convert.ToDouble(articulo.Precio);
			//saveAction.Activated += delegate {	save();	};
		}

//		private void load(){
//			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
//			dbCommand.CommandText = "select * from articulo where id = @id";
//			DbCommandHelper.AddParameter(dbCommand, "id", id);
//			IDataReader dataReader = dbCommand.ExecuteReader ();
//			if (!dataReader.Read ())
//				return;
//			nombre = (string)dataReader ["nombre"];
//			categoria = dataReader ["categoria"];
//			if (categoria is DBNull)
//				categoria = null;
//			precio = (decimal)dataReader ["precio"];
//			dataReader.Close ();
//		}

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
		//resolver el update 
		private void update() {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio) where id = {0}";
			nombre = entryNombre.Text;
			categoria = ComboBoxHelper.GetId(comboBoxCategoria);
			precio = Convert.ToDecimal(spinButtonPrecio.Value);

			DbCommandHelper.AddParameter(dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter(dbCommand,"categoria", categoria);
			DbCommandHelper.AddParameter(dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

	}
}

