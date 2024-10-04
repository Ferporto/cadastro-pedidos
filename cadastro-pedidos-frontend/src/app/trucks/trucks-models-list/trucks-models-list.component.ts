import {AfterViewInit, Component, TemplateRef, ViewChild} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";

import {TrucksService} from "../trucks.service";
import {TrucksModelsEditorModalComponent} from "./trucks-models-editor-modal/trucks-models-editor-modal.component";
import {TrucksModelsApiService} from "../api/services/trucks-models-api.service";
import {TruckModelOutput} from "../api/models/truck-model-output";
import {TruckModelType} from "../api/models/truck-model-type";

@Component({
  selector: 'app-trucks-models-list',
  templateUrl: './trucks-models-list.component.html',
  styleUrls: ['./trucks-models-list.component.scss']
})
export class TrucksModelsListComponent implements AfterViewInit {
  @ViewChild('TrucksModelsListHeader') private headerTemplate!: TemplateRef<any>;

  public columns: string[] = ['actions', 'name', 'type', 'year'];
  public truckModels: TruckModelOutput[] = [];
  public readonly TruckModelType = TruckModelType;

  constructor(private trucksService: TrucksService,  private matDialog: MatDialog,
              private service: TrucksModelsApiService) {
    this.getTruckModels();
  }

  ngAfterViewInit(): void {
    this.emitHeaderTemplate();
  }

  public openTruckModelEditor(truckModel?: TruckModelOutput): void {
    this.matDialog.open(TrucksModelsEditorModalComponent, {
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
    this.trucksService.headerTemplate.next(this.headerTemplate);
  }
}
