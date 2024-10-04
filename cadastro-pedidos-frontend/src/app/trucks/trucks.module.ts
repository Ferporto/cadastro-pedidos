import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TrucksComponent} from './trucks.component';
import {MatIconModule} from "@angular/material/icon";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatTabsModule} from "@angular/material/tabs";
import {MatTableModule} from "@angular/material/table";
import {MatButtonModule} from "@angular/material/button";
import {RouterModule} from "@angular/router";
import {TrucksListComponent} from './trucks-list/trucks-list.component';
import {TrucksModelsListComponent} from './trucks-models-list/trucks-models-list.component';
import {TrucksService} from "./trucks.service";
import {TrucksEditorModalComponent} from './trucks-list/trucks-editor-modal/trucks-editor-modal.component';
import {
  TrucksModelsEditorModalComponent
} from './trucks-models-list/trucks-models-editor-modal/trucks-models-editor-modal.component';
import {TrucksApiService} from "./api/services/trucks-api.service";
import {TrucksModelsApiService} from "./api/services/trucks-models-api.service";
import {MatSelectModule} from "@angular/material/select";
import {MatMenuModule} from "@angular/material/menu";
import {MatDialogModule} from "@angular/material/dialog";
import {MatTooltipModule} from "@angular/material/tooltip";

@NgModule({
  declarations: [
    TrucksComponent,
    TrucksListComponent,
    TrucksModelsListComponent,
    TrucksEditorModalComponent,
    TrucksModelsEditorModalComponent
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
        component: TrucksComponent
      },
      {
        path: '**',
        redirectTo: ''
      }
    ]),
    MatTooltipModule,
  ],
  providers: [
    TrucksService
  ]
})
export class TrucksModule {
}
