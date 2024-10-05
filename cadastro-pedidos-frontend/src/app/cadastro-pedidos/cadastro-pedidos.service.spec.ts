import { TestBed } from '@angular/core/testing';

import { CadastroPedidosService } from './cadastro-pedidos.service';

describe('CadastroPedidosService', () => {
  let service: CadastroPedidosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CadastroPedidosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
