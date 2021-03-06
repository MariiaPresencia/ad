package org.institutoserpis.ad;

import java.util.Date;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

public class PruebaArticulo {
	private static EntityManagerFactory entityManagerFactory;
	
	public static void main(String[] args) {
		Logger.getLogger("org.hibernate").setLevel(Level.SEVERE);
		System.out.println("inicio");
		entityManagerFactory = Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		//find(2L);
		//Long articuloId = persist();
		//update(articuloId);
		//find(articuloId);
		//remove(articuloId);
		query();
		
		entityManagerFactory.close();
	}
	private static void show(Articulo articulo){
		System.out.printf("%5s %-30s %-25s %10s\n", 
				articulo.getId(), 
				articulo.getNombre(),
				format(articulo.getCategoria()), 
				articulo.getPrecio()
		);
	}
	
	private static String format(Categoria categoria){
		if(categoria == null)
			return null;
		return String.format("%4s %-20s", categoria.getId(), categoria.getNombre());
		//return String.format("%4s", categoria.getId());
	}
	//metodo para visualizar todos los objetos
	private static void query(){

		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		//mostrara todos los articulos. Leemos las transacciones ya confirmadas y de tu transaccion sin ser confirmada
		List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
		for (Articulo articulo : articulos)
			show(articulo);
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	//introducimos un objeto (INSERT)
	private static Long persist(){
		//creamos un articulo , le establezco el nombre y lo enviamos
		System.out.println("persist:");
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo articulo = new Articulo();
		articulo.setNombre("nuevo" + new Date());
		entityManager.persist(articulo);
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
		return articulo.getId();
	}
	//esto permite obtener un objeto y lo muestro (BUSCAR)
	private static void find(Long id){
		System.out.println("find:" + id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		Articulo articulo = entityManager.find(Articulo.class, id);
		
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
	}
	//eliminar un objeto (REMOVE)
	private static void remove(Long id){
		System.out.println("remove:" + id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		Articulo articulo = entityManager.find(Articulo.class, id);
		entityManager.remove(articulo);
		
		entityManager.getTransaction().commit();
		entityManager.close();

	}
	//modificamos y refrescamos (UPDATE)
	private static void update(Long id){
		System.out.println("update:" + id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		Articulo articulo = entityManager.find(Articulo.class, id);
		articulo.setNombre("modificado "+ new Date());
		
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
	}
	
}