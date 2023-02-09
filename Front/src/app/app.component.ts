import { Component } from '@angular/core';
import { Descripcion } from './Modelos/descripcion';
import { Marca } from './Modelos/marca';
import { Modelo } from './Modelos/modelo';
import { Sepomex } from './Modelos/sepomex';
import { SubmMarca } from './Modelos/subm-marca';
import { Ubicacion } from './Modelos/ubicacion';
import { ServicoMarcaService } from './Servicios/servico-marca.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Examen';
  Marcas: Marca[];
  Submarcas:SubmMarca[];
  Modelos:Modelo[];
  Descripcion:Descripcion[];
  DescripcionSelect:string;
  ModeloSelect:number;
  SubmarcaSelect:number;
  ColoniaSelect:any;
  MarcaSelct:number;
  cpSearch:string="";
 estado:string;
 municipio:string;
 ubicaciones:Ubicacion[];

  constructor(private  servicios:ServicoMarcaService){
      servicios.getMarcas().subscribe((datos:Marca[])=>{
        console.log(datos );
        this.Marcas = datos;
      });
  }

  public loadSubmarcas() {
    this.Submarcas=[];
    this.Modelos=[];
    if(this.MarcaSelct>0){
      this.servicios.getSubmarca(this.MarcaSelct).subscribe((datos:SubmMarca[])=>{
        console.log(datos);
        this.Submarcas=datos;
      });
    }
  }
  public loadModelos() {
    this.Modelos=[];
    if(this.MarcaSelct>0 && this.SubmarcaSelect>0){
      this.servicios.getModelos(this.SubmarcaSelect).subscribe((datos:Modelo[])=>{
        console.log(datos);
        this.Modelos=datos;
      });
    }
  }
  public loadDescripciones() {
    this.Descripcion=[];
    if(this.ModeloSelect>0){
      this.servicios.getDescripcion(this.SubmarcaSelect,this.ModeloSelect).subscribe((datos:Descripcion[])=>{
        console.log(datos);
        this.Descripcion=datos;
      });
    }
  }
  public buscarCP(){
    console.log(this.cpSearch);
    if(this.cpSearch != undefined &&this.cpSearch.length==5){
      this.servicios.getCp(parseInt(this.cpSearch)).subscribe((datos:Sepomex[])=>{
        console.log(datos);
        this.estado = datos[0].Municipio.Estado.sEstado;
        //this.municipio = datos[0].Municipio.sMunicipio;
        this,this.ubicaciones = datos[0].Ubicacion;
      });
    }
  }
}   
