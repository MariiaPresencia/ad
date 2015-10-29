using Gtk;
using System;
using SerpisAd;
using System.Collections;

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
			CellRendererText cellRendererText = new CellRendererText ();
			comboBoxCategoria.PackStart (cellRendererText, false);
			comboBoxCategoria.SetCellDataFunc (cellRendererText,
			       delegate(CellLayout cell_layout, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				IList row = (IList)tree_model.GetValue(iter, 0);
				cellRendererText.Text = row[1].ToString();

			});
			ListStore listStore = new ListStore (typeof(IList));
			foreach (IList row in queryResult.Rows)
				listStore.AppendValues (row);
			comboBoxCategoria.Model = listStore;

		}
//			spinButtonPrecio.Value = 1.5;

		}
	}

