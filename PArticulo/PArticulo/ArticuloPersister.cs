using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;

namespace PArticulo
{
	public class ArticuloPersister
	{
		public ArticuloPersister (){
		}
		public static Articulo Load(object id){
			Articulo articulo = new Articulo ();
			articulo.Id = id;
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter(dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if (!dataReader.Read ())
				return null;
			articulo.Nombre = (string)dataReader ["nombre"];
			articulo.Categoria = dataReader ["categoria"];
			if (articulo.Categoria is DBNull)
				articulo.Categoria = null;
			articulo.Precio = (decimal)dataReader ["precio"];

			dataReader.Close ();
			return articulo;
		}
		public static int Insert(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";

			DbCommandHelper.AddParameter(dbCommand, "nombre", articulo.Nombre);
			DbCommandHelper.AddParameter(dbCommand,"categoria", articulo.Categoria);
			DbCommandHelper.AddParameter(dbCommand, "precio", articulo.Precio);
			return dbCommand.ExecuteNonQuery ();

		}
		public static int Update(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			string update = "update articulo set nombre=@nombre, categoria=@categoria, precio=@precio where id = {0}";
			dbCommand.CommandText = string.Format (update, articulo.Id);
			DbCommandHelper.AddParameter(dbCommand, "nombre", articulo.Nombre);
			DbCommandHelper.AddParameter(dbCommand,"categoria", articulo.Categoria);
			DbCommandHelper.AddParameter(dbCommand, "precio", articulo.Precio);
			return dbCommand.ExecuteNonQuery ();
		}
	}
}

