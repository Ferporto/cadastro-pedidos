import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroPedidosModelsListComponent } from './cadastro-pedidos-models-list.component';

describe('CadastroPedidosModelsListComponent', () => {
  let component: CadastroPedidosModelsListComponent;
  let fixture: ComponentFixture<CadastroPedidosModelsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CadastroPedidosModelsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastroPedidosModelsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
