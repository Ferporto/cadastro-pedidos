import {AfterViewInit, Component, TemplateRef, ViewChild} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";

import {CadastroPedidosService} from "../cadastro-pedidos.service";
import {CadastroPedidosModelsEditorModalComponent} from "./cadastro-pedidos-models-editor-modal/cadastro-pedidos-models-editor-modal.component";
import {CadastroPedidosModelsApiService} from "../api/services/cadastro-pedidos-models-api.service";
import {TruckModelOutput} from "../api/models/truck-model-output";
import {TruckModelType} from "../api/models/truck-model-type";

@Component({
  selector: 'app-cadastro-pedidos-models-list',
  templateUrl: './cadastro-pedidos-models-list.component.html',
  styleUrls: ['./cadastro-pedidos-models-list.component.scss']
})
export class CadastroPedidosModelsListComponent implements AfterViewInit {
  @ViewChild('CadastroPedidosModelsListHeader') private headerTemplate!: TemplateRef<any>;

  public columns: string[] = ['actions', 'name', 'type', 'year'];
  public truckModels: TruckModelOutput[] = [];
  public readonly TruckModelType = TruckModelType;

  constructor(private cadastro-pedidosService: CadastroPedidosService,  private matDialog: MatDialog,
              private service: CadastroPedidosModelsApiService) {
    this.getTruckModels();
  }

  ngAfterViewInit(): void {
    this.emitHeaderTemplate();
  }

  public openTruckModelEditor(truckModel?: TruckModelOutput): void {
    this.matDialog.open(CadastroPedidosModelsEditorModalComponent, {
      data: truckModel,
      hasBackdrop: true,
      height: 'calc(100% - 64px)',
      width: '40%',
      position: {
        right: '0',
        bottom: '0',
      }
    }).afterClosed().subscribe(() => {
      this.getTruckModels();
    });
  }

  public update(truckModel: TruckModelOutput): void {
    this.openTruckModelEditor(truckModel);
  }

  public delete(truckModel: TruckModelOutput): void {
    this.service.delete(truckModel.id).subscribe(() => {
      this.getTruckModels();
    });
  }

  private getTruckModels(): void {
    this.service.getList().subscribe((truckModels: TruckModelOutput[]) => {
      this.truckModels = truckModels;
    });
  }

  private emitHeaderTemplate(): void {
    this.cadastroPedidosService.headerTemplate.next(this.headerTemplate);
  }
}
