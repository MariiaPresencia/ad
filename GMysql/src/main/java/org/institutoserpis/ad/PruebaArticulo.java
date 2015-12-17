package org.institutoserpis.ad;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;
import com.mysql.jdbc.PreparedStatement;

public class PruebaArticulo {
	public static void main(String[] args) throws Exception {
		//conectar con la base de datos 
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		Statement statement = connection.createStatement();
		
		//preparando el insert, update y delete
		String insert="INSERT INTO articulo VALUES(null,?,?,?)";
		String update = "UPDATE articulo SET nombre = ? WHERE categoria=? AND precio = ?";
		String delete = "DELETE FROM articulo WHERE nombre = ?";
		
		//los creamos y ejecutamos
		PreparedStatement insertar = (PreparedStatement)connection.prepareStatement(insert);
		insertarDatos(insertar);
		PreparedStatement update1 = (PreparedStatement) connection.prepareStatement(update);
		updateDatos(update1);
		PreparedStatement delete1 = (PreparedStatement) connection.prepareStatement(delete);
		deleteDatos(delete1);
		
		//ejecutamos la sentencia select * from articulo
		ResultSet result = statement.executeQuery("SELECT * FROM articulo");
		
		//mostramos la tabla con los datos
		System.out.println("TABLA:");
		mostrarDatos(result);
		System.out.println("\n");
		
		result.close();
		statement.close();
		connection.close();
		System.out.println("Fin");
	}
	
	private static int insertarDatos(PreparedStatement insertar) throws SQLException{
		insertar.setString(1, "articuloInsertar3");
		insertar.setInt(2, 3);
		insertar.setDouble(3, 5.7);
		return insertar.executeUpdate();
	}
	
	private static int updateDatos(PreparedStatement insertar) throws SQLException{
		insertar.setString(1, "articulo7");
		insertar.setInt(2, 1);
		insertar.setDouble(3, 5.7);
		return insertar.executeUpdate();
	}
	
	private static int deleteDatos(PreparedStatement insertar)throws SQLException{
		insertar.setString(1, "articulo 23");
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
		System.out.println("\n");
		while (rs.next()) {
		      for (int i = 1; i < numColum + 1; i++) {
		        System.out.print(rs.getString(i) + "         "+"\t");
		      }
		      System.out.println();
		    }
	}
		
}
