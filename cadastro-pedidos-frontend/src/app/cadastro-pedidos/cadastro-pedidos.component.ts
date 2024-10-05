import {Component, TemplateRef} from '@angular/core';

import {CadastroPedidosService} from "./cadastro-pedidos.service";

@Component({
  selector: 'app-cadastro-pedidos',
  templateUrl: './cadastro-pedidos.component.html',
  styleUrls: ['./cadastro-pedidos.component.scss']
})
export class CadastroPedidosComponent {
  public headerTemplate!: TemplateRef<any>

  constructor(private service: CadastroPedidosService) {
    this.subscribeToSetHeader();
  }

  private subscribeToSetHeader(): void {
    this.service.headerTemplate.subscribe((header: TemplateRef<any>) => {
      this.headerTemplate = header;
    });
  }
}
