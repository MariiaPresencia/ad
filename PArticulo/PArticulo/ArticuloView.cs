using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
//			entryNombre.Text = "nuevo";
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult);

			saveAction.Activated += delegate {save();};

		}
		private void save(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo(nombre, categoria, precio)" 
				+"values(@nombre, @categoria, @precio)";

			string nombre = entryNombre.Text;
			object categoria = GetID (comboBoxCategoria);
			decimal precio = Convert.ToDecimal(spinButtonPrecio.Value);

			addParameter (dbCommand, "nombre", nombre);
			addParameter (dbCommand, "categoria", categoria);
			addParameter (dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

		private static void addParameter(IDbCommand dbCommand, string name, object value){
			IDbDataParameter dbDataParameter = dbCommand.CreateParameter ();
			dbDataParameter.ParameterName = name;
			dbDataParameter.Value = value;
			dbCommand.Parameters.Add (dbDataParameter);
		}

		public static object GetID(ComboBox comboBox){
			TreeIter treeIter;
			comboBox.GetActiveIter (out treeIter);
			IList row = (IList) comboBox.Model.GetValue (treeIter, 0);
			return row [0];
		}
	}
}

