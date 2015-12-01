using Gtk;
using System;
using SerpisAd;
using System.Collections;
using System.Data;


namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private Articulo articulo;
		System.Action save;

		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{
			articulo = new Articulo ();
			init ();
			//saveAction.Activated += delegate{insert();};
			saveAction.Activated += delegate {
				insert();
			};
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

//		private void save() {
//			if (id == null)
//				insert ();
//			else
//				update ();
//		}

		public void insert(){
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId(comboBoxCategoria);
			articulo.Precio = Convert.ToDecimal(spinButtonPrecio.Value);
			ArticuloPersister.Insert (articulo); 
			Destroy ();
		}
//		resolver el update 
		private void update() {
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId(comboBoxCategoria);
			articulo.Precio = Convert.ToDecimal(spinButtonPrecio.Value);
			ArticuloPersister.Update (articulo);

			Destroy ();
		}

	}
}

