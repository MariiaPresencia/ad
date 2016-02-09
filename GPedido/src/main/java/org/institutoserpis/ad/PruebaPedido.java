package org.institutoserpis.ad;

import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

public class PruebaPedido {
	
	private static EntityManagerFactory entityManagerFactory;

	public static void main(String[] args) {
		Logger.getLogger("org.hibernate").setLevel(Level.SEVERE);
		System.out.println("inicio");
		
		entityManagerFactory = Persistence.createEntityManagerFactory("org.institutoserpis.ad");	
		
		//MOSTRAR ARTICULO
		/*List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
		for(Articulo articulo : articulos)
			System.out.println(articulo);
		entityManager.getTransaction().commit();
		entityManager.close();*/
		
		//MOSTRAR CATEGORIA
		/*List<Categoria> categorias = entityManager.createQuery("from Categoria", Categoria.class).getResultList();
		for(Categoria categoria : categorias)
			System.out.println(categoria);
		entityManager.getTransaction().commit();
		entityManager.close();*/
		
		//MOSTRAR CLIENTE
		/*List<Cliente> clientes = entityManager.createQuery("from Cliente", Cliente.class).getResultList();
		for(Cliente cliente : clientes)
			System.out.println(cliente);
		entityManager.getTransaction().commit();
		entityManager.close();*/
		
		query();
		
		entityManagerFactory.close();
		
		System.out.println("fin");
	}
	
	private static void query(){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Pedido> pedidos = entityManager.createQuery("from Pedido", Pedido.class).getResultList();
		for(Pedido pedido : pedidos)
			System.out.println(pedido);
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	
}
