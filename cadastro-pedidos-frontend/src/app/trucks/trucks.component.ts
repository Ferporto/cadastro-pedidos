import {Component, TemplateRef} from '@angular/core';

import {TrucksService} from "./trucks.service";

@Component({
  selector: 'app-trucks',
  templateUrl: './trucks.component.html',
  styleUrls: ['./trucks.component.scss']
})
export class TrucksComponent {
  public headerTemplate!: TemplateRef<any>

  constructor(private service: TrucksService) {
    this.subscribeToSetHeader();
  }

  private subscribeToSetHeader(): void {
    this.service.headerTemplate.subscribe((header: TemplateRef<any>) => {
      this.headerTemplate = header;
    });
  }
}
