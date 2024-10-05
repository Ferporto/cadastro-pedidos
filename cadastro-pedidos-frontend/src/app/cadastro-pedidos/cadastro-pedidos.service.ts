import {Injectable, TemplateRef} from '@angular/core';

import {Subject} from "rxjs";

@Injectable()
export class CadastroPedidosService {
  public headerTemplate: Subject<TemplateRef<any>> = new Subject<TemplateRef<any>>();
}
