import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CadastroPedidosComponent} from './cadastro-pedidos.component';
import {MatIconModule} from "@angular/material/icon";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatTabsModule} from "@angular/material/tabs";
import {MatTableModule} from "@angular/material/table";
import {MatButtonModule} from "@angular/material/button";
import {RouterModule} from "@angular/router";
import {CadastroPedidosListComponent} from './cadastro-pedidos-list/cadastro-pedidos-list.component';
import {CadastroPedidosModelsListComponent} from './cadastro-pedidos-models-list/cadastro-pedidos-models-list.component';
import {CadastroPedidosService} from "./cadastro-pedidos.service";
import {CadastroPedidosEditorModalComponent} from './cadastro-pedidos-list/cadastro-pedidos-editor-modal/cadastro-pedidos-editor-modal.component';
import {
  CadastroPedidosModelsEditorModalComponent
} from './cadastro-pedidos-models-list/cadastro-pedidos-models-editor-modal/cadastro-pedidos-models-editor-modal.component';
import {CadastroPedidosApiService} from "./api/services/cadastro-pedidos-api.service";
import {CadastroPedidosModelsApiService} from "./api/services/cadastro-pedidos-models-api.service";
import {MatSelectModule} from "@angular/material/select";
import {MatMenuModule} from "@angular/material/menu";
import {MatDialogModule} from "@angular/material/dialog";
import {MatTooltipModule} from "@angular/material/tooltip";

@NgModule({
  declarations: [
    CadastroPedidosComponent,
    CadastroPedidosListComponent,
    CadastroPedidosModelsListComponent,
    CadastroPedidosEditorModalComponent,
    CadastroPedidosModelsEditorModalComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatTabsModule,
    MatTableModule,
    MatButtonModule,
    MatSelectModule,
    MatMenuModule,
    MatDialogModule,
    RouterModule.forChild([
      {
        path: '',
        component: CadastroPedidosComponent
      },
      {
        path: '**',
        redirectTo: ''
      }
    ]),
    MatTooltipModule,
  ],
  providers: [
    CadastroPedidosService
  ]
})
export class CadastroPedidosModule {
}
