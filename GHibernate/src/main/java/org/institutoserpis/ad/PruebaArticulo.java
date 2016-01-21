package org.institutoserpis.ad;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

public class PruebaArticulo {

	public static void main(String[] args) {
		System.out.println("inicio");
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
		for (Articulo articulo : articulos)
			System.out.printf("%5s %-30s %5s %10s\n", 
					articulo.getId(), 
					articulo.getNombre(), 
					articulo.getCategoria(), 
					articulo.getPrecio()
			);
		entityManager.getTransaction().commit();
		entityManager.close();
		
		entityManagerFactory.close();
	}

}