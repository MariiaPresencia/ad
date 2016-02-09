package org.institutoserpis.ad;

import java.math.BigDecimal;
import java.util.Calendar;

import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import org.hibernate.annotations.GenericGenerator;

@Entity
public class Pedido {
	private Long id;
	private Cliente cliente;
	private Calendar fecha;
	private BigDecimal importe;
	
	@Id
	@GeneratedValue(generator="increment")
	@GenericGenerator(name="increment", strategy = "increment")
	public Long getId() {
		return id;
	}
	@ManyToOne(fetch=FetchType.LAZY)
	@JoinColumn(name="cliente")
	public Cliente getCliente() {
		return cliente;
	}
	public void setCliente(Cliente cliente) {
		this.cliente = cliente;
	}
	
	public Calendar getCalendar(){
		return fecha;
	}
	public void setCalendar(Calendar fecha){
		this.fecha=fecha;
	}
	
	public BigDecimal getPrecio() {
		return importe;
	}
	public void setPrecio(BigDecimal importe) {
		this.importe = importe;
	}
	@Override
	public String toString() {
		return String.format("%s %s %s %s", id,cliente,fecha,importe);
	}
}
