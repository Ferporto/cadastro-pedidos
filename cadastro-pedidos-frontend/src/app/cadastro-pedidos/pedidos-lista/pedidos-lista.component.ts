import {AfterViewInit, Component, TemplateRef, ViewChild} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";

import {CadastroPedidosService} from "../cadastro-pedidos.service";
import {CadastroPedidosEditorModalComponent} from "./cadastro-pedidos-editor-modal/cadastro-pedidos-editor-modal.component";
import {CadastroPedidosApiService} from "../api/services/cadastro-pedidos-api.service";
import {TruckModelOutput} from "../api/models/truck-model-output";
import {TruckOutput} from "../api/models/truck-output";

@Component({
  selector: 'app-cadastro-pedidos-list',
  templateUrl: './cadastro-pedidos-list.component.html',
  styleUrls: ['./cadastro-pedidos-list.component.scss']
})
export class CadastroPedidosListComponent implements AfterViewInit {
  @ViewChild('CadastroPedidosListHeader') private headerTemplate!: TemplateRef<any>;

  public columns: string[] = ['actions', 'licensePlate', 'modelName', 'modelYear', 'manufacturingYear'];
  public cadastro-pedidos: TruckOutput[] = [];

  constructor(private cadastro-pedidosService: CadastroPedidosService, private matDialog: MatDialog,
              private service: CadastroPedidosApiService) {
    this.getCadastroPedidos();
  }

  ngAfterViewInit(): void {
    this.emitHeaderTemplate();
  }

  public openTruckEditor(truck?: TruckOutput): void {
    this.matDialog.open(CadastroPedidosEditorModalComponent, {
      data: truck,
      hasBackdrop: true,
      height: 'calc(100% - 64px)',
      width: '40%',
      position: {
        right: '0',
        bottom: '0',
      }
    }).afterClosed().subscribe(() => {
      this.getCadastroPedidos();
    });
  }

  public update(truckModel: TruckModelOutput): void {
    this.openTruckEditor(truckModel);
  }

  public delete(truckModel: TruckModelOutput): void {
    this.service.delete(truckModel.id).subscribe(() => {
      this.getCadastroPedidos();
    });
  }

  private getCadastroPedidos(): void {
    this.service.getList().subscribe((cadastro-pedidos: TruckOutput[]) => {
      this.cadastroPedidos = cadastro-pedidos;
    });
  }

  private emitHeaderTemplate(): void {
    this.cadastroPedidosService.headerTemplate.next(this.headerTemplate);
  }
}
