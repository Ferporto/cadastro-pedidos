import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CadastroPedidosComponent} from './cadastro-pedidos.component';
import {MatIconModule} from "@angular/material/icon";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatTabsModule} from "@angular/material/tabs";
import {MatTableModule} from "@angular/material/table";
import {MatButtonModule} from "@angular/material/button";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {RouterModule} from "@angular/router";
import {PedidosListaComponent} from './pedidos-lista/pedidos-lista.component';
//import {CadastroPedidosModelsListComponent} from './cadastro-pedidos-models-list/cadastro-pedidos-models-list.component';
import {CadastroPedidosService} from "./cadastro-pedidos.service";
import {PedidosEditorModalComponent} from './pedidos-lista/pedidos-editor-modal/pedidos-editor-modal.component';
//import {
//  CadastroPedidosModelsEditorModalComponent
//} from './cadastro-pedidos-models-list/cadastro-pedidos-models-editor-modal/cadastro-pedidos-models-editor-modal.component';
import {MatSelectModule} from "@angular/material/select";
import {MatMenuModule} from "@angular/material/menu";
import {MatDialogModule} from "@angular/material/dialog";
import {MatTooltipModule} from "@angular/material/tooltip";
import {MatFormFieldModule} from '@angular/material/form-field';

@NgModule({
  declarations: [
    CadastroPedidosComponent,
    PedidosListaComponent,
    PedidosEditorModalComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
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
    MatCheckboxModule
  ],
  providers: [
    CadastroPedidosService
  ]
})
export class CadastroPedidosModule {
}
