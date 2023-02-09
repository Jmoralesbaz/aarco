import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Descripcion } from '../Modelos/descripcion';
import { Marca } from '../Modelos/marca';
import { Modelo } from '../Modelos/modelo';
import { Sepomex } from '../Modelos/sepomex';
import { SubmMarca } from '../Modelos/subm-marca';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ServicoMarcaService  extends BaseService {

  constructor(protected http: HttpClient) {
    super(http);
    this.pathService ='';
  }
 
  public getMarcas(){
    return this.Get<Marca[]>('marcas',[]);
  }
  public getSubmarca(marca:number){
    return this.Get<SubmMarca[]>('submarcas/'+marca,[]);
  }
  public getModelos(submarca:number){
    return this.Get<Modelo[]>('modelos/'+submarca,[]);
  }
  public getDescripcion(submarca:number, modelo:number){
    return this.Get<Descripcion[]>('descripciones/'+submarca+'/'+modelo,[]);
  }
  public getCp(cp:number){
    return this.Get<Sepomex[]>('consultacp/'+cp,[]);
  }
}