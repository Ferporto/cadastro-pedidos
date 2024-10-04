import {AfterViewInit, Component, TemplateRef, ViewChild} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";

import {TrucksService} from "../trucks.service";
import {TrucksEditorModalComponent} from "./trucks-editor-modal/trucks-editor-modal.component";
import {TrucksApiService} from "../api/services/trucks-api.service";
import {TruckModelOutput} from "../api/models/truck-model-output";
import {TruckOutput} from "../api/models/truck-output";

@Component({
  selector: 'app-trucks-list',
  templateUrl: './trucks-list.component.html',
  styleUrls: ['./trucks-list.component.scss']
})
export class TrucksListComponent implements AfterViewInit {
  @ViewChild('TrucksListHeader') private headerTemplate!: TemplateRef<any>;

  public columns: string[] = ['actions', 'licensePlate', 'modelName', 'modelYear', 'manufacturingYear'];
  public trucks: TruckOutput[] = [];

  constructor(private trucksService: TrucksService, private matDialog: MatDialog,
              private service: TrucksApiService) {
    this.getTrucks();
  }

  ngAfterViewInit(): void {
    this.emitHeaderTemplate();
  }

  public openTruckEditor(truck?: TruckOutput): void {
    this.matDialog.open(TrucksEditorModalComponent, {
      data: truck,
      hasBackdrop: true,
      height: 'calc(100% - 64px)',
      width: '40%',
      position: {
        right: '0',
        bottom: '0',
      }
    }).afterClosed().subscribe(() => {
      this.getTrucks();
    });
  }

  public update(truckModel: TruckModelOutput): void {
    this.openTruckEditor(truckModel);
  }

  public delete(truckModel: TruckModelOutput): void {
    this.service.delete(truckModel.id).subscribe(() => {
      this.getTrucks();
    });
  }

  private getTrucks(): void {
    this.service.getList().subscribe((trucks: TruckOutput[]) => {
      this.trucks = trucks;
    });
  }

  private emitHeaderTemplate(): void {
    this.trucksService.headerTemplate.next(this.headerTemplate);
  }
}
