package org.institutoserpis.ad;

import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Scanner;
import java.util.InputMismatchException;

public class PruebaArticulo {
	static Scanner teclado = new Scanner(System.in);
	public static void main(String[] args) throws Exception {
		//conectar con la base de datos 
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		Statement statement = connection.createStatement();
		
		//ejecutamos la sentencia select * from articulo
		ResultSet result = statement.executeQuery("SELECT * FROM articulo");
		
		System.out.println( "                 "+"\tTABLA:");
		mostrarDatos(result);
		System.out.println("\n");
		
		//creamos el menu y ejecutamos los metodos correspondientes
		System.out.println("MENU:");
		System.out.println("1- Leer");
		System.out.println("2- Nuevo");
		System.out.println("3- Editar");
		System.out.println("4- Eliminar");
		System.out.println("");
		System.out.print("Opcion que eliges:");
		int num = teclado.nextInt();
		
		switch(num){
		case 1:
			
			break;
		case 2: 
			String insert="INSERT INTO articulo VALUES(null,?,?,?)";
			PreparedStatement insertar = (PreparedStatement)connection.prepareStatement(insert);
			insertarDatos(insertar);
			break;
		case 3:
			String update = "UPDATE articulo SET nombre = ? WHERE categoria=? AND precio = ?";
			PreparedStatement update1 = (PreparedStatement) connection.prepareStatement(update);
			updateDatos(update1);
			break;
		case 4:
			String delete = "DELETE FROM articulo WHERE nombre = ?";
			PreparedStatement delete1 = (PreparedStatement) connection.prepareStatement(delete);
			deleteDatos(delete1);
			break;	
		}
		ResultSet result1 = statement.executeQuery("SELECT * FROM articulo");
		//mostramos la tabla con los datos
		System.out.println("TABLA MODIFICADA:");
		mostrarDatos(result1);
		System.out.println("\n");

		connection.close();
		System.out.println("Fin");
	}
	
	private static int insertarDatos(PreparedStatement insertar) throws SQLException{
		System.out.println("Nombre del articulo :");
		String nombre = teclado.next();
		System.out.println("Categoria (1-3):");
		int categoria = teclado.nextInt();
		System.out.println("Precio:");
		double precio = teclado.nextDouble();
		insertar.setString(1, nombre);
		insertar.setInt(2,categoria);
		insertar.setDouble(3, precio);
		return insertar.executeUpdate();
	}
	
	private static int updateDatos(PreparedStatement insertar) throws SQLException{
		System.out.println("Nombre del articulo :");
		String nombre = teclado.next();
		System.out.println("Categoria (1-3):");
		int categoria = teclado.nextInt();
		System.out.println("Precio:");
		double precio = teclado.nextDouble();
		insertar.setString(1, nombre);
		insertar.setInt(2, categoria);
		insertar.setDouble(3, precio);
		return insertar.executeUpdate();
	}
	
	private static int deleteDatos(PreparedStatement insertar)throws SQLException{
		System.out.println("Nombre del articulo :");
		String nombre = teclado.next();
		insertar.setString(1, nombre);
		return insertar.executeUpdate();
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
