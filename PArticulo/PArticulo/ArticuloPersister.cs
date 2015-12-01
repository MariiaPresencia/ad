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
		public static void Insert(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) values (@nombre, @categoria, @precio)";
			String nom = articulo.Nombre;
			object categoria = articulo.Categoria;
			decimal precio = articulo.Precio;
			DbCommandHelper.AddParameter(dbCommand, "nombre", nom);
			DbCommandHelper.AddParameter(dbCommand,"categoria", categoria);
			DbCommandHelper.AddParameter(dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();

		}
		public static void Update(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio) where id = {0}";
			String nom = articulo.Nombre;
			object categoria = articulo.Categoria;
			decimal precio = articulo.Precio;

			DbCommandHelper.AddParameter(dbCommand, "nombre", nom);
			DbCommandHelper.AddParameter(dbCommand,"categoria", categoria);
			DbCommandHelper.AddParameter(dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
		}
	}
}

