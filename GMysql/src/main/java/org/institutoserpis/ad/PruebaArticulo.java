package org.institutoserpis.ad;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;

public class PruebaArticulo {
	public static void main(String[] args) throws SQLException {
		//conectar con la base de datos 
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		
		Statement statement = connection.createStatement();
		//ejecutamos la sentencia select * from articulo
		ResultSet result = statement.executeQuery("SELECT * FROM articulo");
		
		ResultSetMetaData rsMetaData = result.getMetaData();
		int numColum = rsMetaData.getColumnCount();
		System.out.println("Nombre de las columnas:");
		for (int i =1; i < numColum+1; i++) {
		    //mostamos los nombres de las columnas
			String columnName = rsMetaData.getColumnName(i);
		    System.out.print(columnName+"|");
		    
		}
		System.out.println();
		System.out.println();
		while(result.next()){
			String id1 = result.getString(1);
		    String nombre1 = result.getString(2);
		    String categoria1= result.getString(3);
		    String precio1 = result.getString(4);
		    //mostramos los datos de las columnas de la base de datos
		    System.out.println(id1+" | "+nombre1+" | "+ categoria1 +" | "+ precio1 +"\n");
		}

		connection.close();
		System.out.println();
		System.out.println("Fin");
	}
}
