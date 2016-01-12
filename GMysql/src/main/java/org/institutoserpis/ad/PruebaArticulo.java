package org.institutoserpis.ad;

import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Scanner;

public class PruebaArticulo {
	static Scanner teclado = new Scanner(System.in);
	public static void main(String[] args) throws Exception {
		//conectar con la base de datos 
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		Statement statement = connection.createStatement();
		
		//ejecutamos la sentencia select * from articulo
		ResultSet result = statement.executeQuery("SELECT * FROM articulo");
		mostrarDatos(result);
		int num;
		do{
		//creamos el menu y ejecutamos los metodos correspondientes
			System.out.println("MENU:");
			System.out.println("1- Leer");
			System.out.println("2- Nuevo");
			System.out.println("3- Editar");
			System.out.println("4- Eliminar");
			System.out.println("5- Listar todos");
			System.out.println("0- Salir");
			System.out.println("");
			System.out.print("Opcion que eliges:");
			num = teclado.nextInt();
			//vaciamos el buffer
			teclado.nextLine();
			
		switch(num){
		case 1:
			System.out.println("Que id quieres visualizar?");
			int id1 = teclado.nextInt();
			String visualizar = "SELECT * FROM articulo WHERE id ="+id1+"";
			ResultSet vis = statement.executeQuery(visualizar);
			mostrarDatos(vis);
			break;
		case 2: 
			String insert="INSERT INTO articulo VALUES (null,?,?,?)";
			PreparedStatement insertar = (PreparedStatement)connection.prepareStatement(insert);
			insertarDatos(insertar);
			break;	
		case 3:
			String update = "UPDATE articulo SET nombre =?,categoria=?,precio=? WHERE id=?";
			PreparedStatement update1 = (PreparedStatement) connection.prepareStatement(update);
			updateDatos(update1);
			break;
		case 4:
			String delete = "DELETE FROM articulo WHERE id = ?";
			PreparedStatement delete1 = (PreparedStatement) connection.prepareStatement(delete);
			deleteDatos(delete1);
			break;
		case 5:
			ResultSet result1 = statement.executeQuery("SELECT * FROM articulo");
			//mostramos la tabla con los datos
			mostrarDatos(result1);
			break;
		case 0:
			connection.close();
			System.out.println("Fin");
		}
		}while(num > 0);
	}
	private static int insertarDatos(PreparedStatement insertar) throws SQLException{
		System.out.println("Nombre del articulo :");
		String nombre = teclado.nextLine();
		insertar.setString(1, nombre);
		System.out.println("Categoria (1-3):");
		int categoria = teclado.nextInt();
		if(categoria>1 && categoria <3){
			insertar.setInt(2,categoria);
		}else{
			System.out.println("Introduce 1 o 2 o 3 para la categoria: ");
			int categoria1 = teclado.nextInt();
			insertar.setInt(2, categoria1);
		}
		System.out.println("Precio:");
		double precio = teclado.nextDouble();
		insertar.setDouble(3, precio);
		return insertar.executeUpdate();
	}
	private static int updateDatos(PreparedStatement update) throws SQLException{
		System.out.println("El id:");
		int id = teclado.nextInt();
		teclado.nextLine();
		System.out.println("Nombre del articulo :");
		String nombre = teclado.nextLine();
		System.out.println("Categoria (1-3):");
		int categoria = teclado.nextInt();
		if(categoria>1 && categoria <3){
			update.setInt(2,categoria);
		}else{
			System.out.println("Introduce 1 o 2 o 3 para la categoria: ");
			int categoria1 = teclado.nextInt();
			update.setInt(2, categoria1);
		}
		System.out.println("Precio:");
		double precio = teclado.nextDouble();
		update.setInt(4,id);
		update.setDouble(3,precio);
		update.setString(1,nombre);
		return update.executeUpdate();
	}
	private static int deleteDatos(PreparedStatement delete)throws SQLException{
		System.out.println("Id del articulo :");
		int nombre = teclado.nextInt();
		delete.setInt(1, nombre);
		return delete.executeUpdate();
	}
	private static void mostrarDatos(ResultSet rs) throws Exception {	
		ResultSetMetaData rsMetaData = rs.getMetaData();
		int numColum = rsMetaData.getColumnCount();
		System.out.println("Nombre de las columnas: ");
		for (int i =1; i < numColum+1; i++) {
		    //mostamos los nombres de las columnas
			String columnName = rsMetaData.getColumnName(i);
		    System.out.print(columnName+ "         "+"\t");
		}
		System.out.println();
		System.out.println("---------------------------------------------------------------");
		while (rs.next()) {
		      for (int i = 1; i < numColum + 1; i++) {
		        System.out.print(rs.getString(i) + "         "+"\t");
		      }
		      System.out.println();
		    }
	}
		
}
