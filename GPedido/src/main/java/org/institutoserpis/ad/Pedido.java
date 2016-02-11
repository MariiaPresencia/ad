package org.institutoserpis.ad;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;

import org.hibernate.annotations.GenericGenerator;

@Entity
public class Pedido {
	private Long id;
	private Cliente cliente;
	private Calendar fecha;
	//private BigDecimal importe;
	private List<PedidoLinea> pedidosLineas = new ArrayList<>();
	
	@Id
	@GeneratedValue(generator="increment")
	@GenericGenerator(name="increment", strategy = "increment")
	public Long getId() {
		return id;
	}
	public void setId(Long id){
		this.id=id;
	}
	@ManyToOne
	@JoinColumn(name="cliente")
	public Cliente getCliente() {
		return cliente;
	}
	
	public void setCliente(Cliente cliente) {
		this.cliente = cliente;
	}
	
	public Calendar getFecha(){
		return fecha;
	}
	public void setFecha(Calendar fecha){
		this.fecha=fecha;
	}
	
	/*public BigDecimal getPrecio() {
		return importe;
	}
	public void setPrecio(BigDecimal importe) {
		this.importe = importe;
	}*/
	//con el cascade ya no tienes por que preocuparte para que haga un persist de cada Linea
	@OneToMany(mappedBy="pedido", cascade=CascadeType.ALL)
	public List<PedidoLinea> getPedidosLineas() {
		return pedidosLineas;
	}
	public void setPedidosLineas(List<PedidoLinea> pedidosLineas) {
		this.pedidosLineas = pedidosLineas;
	}
	@Override
	public String toString() {
		return String.format("%s [cliente-%s] %s",
				id,
				cliente == null ? null : cliente.getId(),
				fecha == null ? null : fecha.getTime());
	}
}
