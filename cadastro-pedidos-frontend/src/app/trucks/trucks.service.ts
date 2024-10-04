import {Injectable, TemplateRef} from '@angular/core';

import {Subject} from "rxjs";

@Injectable()
export class TrucksService {
  public headerTemplate: Subject<TemplateRef<any>> = new Subject<TemplateRef<any>>();
}
