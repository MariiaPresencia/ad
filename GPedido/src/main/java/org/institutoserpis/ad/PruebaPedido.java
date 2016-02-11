package org.institutoserpis.ad;

import java.util.Calendar;
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
		
		//insertamos un nuevo Pedido
		persist();
		//mostramos los datos de Pedido
		query();
		
		entityManagerFactory.close();
		
		System.out.println("fin");
	}
	
	private static void show(Pedido pedido){
		System.out.println(pedido);
		for(PedidoLinea pedidoLinea: pedido.getPedidosLineas())
			System.out.println(pedidoLinea);
	}
	
	private static void query(){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Pedido> pedidos = entityManager.createQuery("from Pedido", Pedido.class).getResultList();
		for(Pedido pedido : pedidos)
			show(pedido);
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	
	private static Long persist(){
		//creamos un pedido , le establezco el nombre y lo enviamos
		System.out.println("persist:");
		EntityManager entityManager = entityManagerFactory.createEntityManager();	
		entityManager.getTransaction().begin();
		//le asignamos al atributo cliente el cliente con id = 1
		Cliente cliente = entityManager.find(Cliente.class, 1L);
		Pedido pedido = new Pedido();
		//le asignamos el cliente creado
		pedido.setCliente(cliente);
		//le asignamos la fecha actual 
		pedido.setFecha(Calendar.getInstance());
		//creamos lineas de pedido
		for(Long articuloId = 2L; articuloId <= 3;articuloId++){
			Articulo articulo = entityManager.find(Articulo.class, articuloId);
			PedidoLinea pedidoLinea = new PedidoLinea();
			pedidoLinea.setPedido(pedido);
			pedidoLinea.setArticulo(articulo);
			
			//OJO importante,a la coleccion le añadimos todo lo creado en la clase PedidoLinea
			pedido.getPedidosLineas().add(pedidoLinea);
		}
		
		//TODO lo que toque
		entityManager.persist(pedido);
		entityManager.getTransaction().commit();
		entityManager.close();
		show(pedido);
		
		return pedido.getId();
	}
	
}
