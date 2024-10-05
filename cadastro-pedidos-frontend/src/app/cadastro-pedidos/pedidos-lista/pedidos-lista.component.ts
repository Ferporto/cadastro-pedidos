import {AfterViewInit, Component, TemplateRef, ViewChild} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";

import {CadastroPedidosService} from "../cadastro-pedidos.service";
import {PedidosEditorModalComponent} from "./pedidos-editor-modal/pedidos-editor-modal.component";
import { PedidoOutput, PedidoOutputPagedResultDto, PedidoService } from '../../api';

@Component({
  selector: 'app-pedidos-lista',
  templateUrl: './pedidos-lista.component.html',
  styleUrls: ['./pedidos-lista.component.scss']
})
export class PedidosListaComponent implements AfterViewInit {
  @ViewChild('PedidosListaHeader') private headerTemplate!: TemplateRef<any>;

  public columns: string[] = ['actions', 'nomeCliente', 'emailCliente', 'pago'];
  public pedidos: PedidoOutput[] = [];

  constructor(private cadastroPedidosService: CadastroPedidosService, private matDialog: MatDialog,
              private service: PedidoService) {
    this.getPedidos();
  }

  ngAfterViewInit(): void {
    this.emitHeaderTemplate();
  }

  public openPedidosEditor(pedido?: any): void {
    this.matDialog.open(PedidosEditorModalComponent, {
      data: pedido,
      hasBackdrop: true,
      height: 'calc(100% - 64px)',
      width: '40%',
      position: {
        right: '0',
        bottom: '0',
      }
    }).afterClosed().subscribe(() => {
      this.getPedidos();
    });
  }

  public update(pedido: PedidoOutput): void {
    this.openPedidosEditor(pedido);
  }

  public delete(pedido: PedidoOutput): void {
    this.service.pedidosIdDelete(pedido.id).subscribe(() => {
      this.getPedidos();
    });
  }

  private getPedidos(): void {
    this.service.pedidosGet().subscribe((pedidosPaged: PedidoOutputPagedResultDto) => {
      this.pedidos = pedidosPaged.itens;
    });
  }

  private emitHeaderTemplate(): void {
    this.cadastroPedidosService.headerTemplate.next(this.headerTemplate);
  }
}
