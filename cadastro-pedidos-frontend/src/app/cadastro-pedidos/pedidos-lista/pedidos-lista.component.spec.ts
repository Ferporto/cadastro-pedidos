import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroPedidosListComponent } from './cadastro-pedidos-list.component';

describe('CadastroPedidosListComponent', () => {
  let component: CadastroPedidosListComponent;
  let fixture: ComponentFixture<CadastroPedidosListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CadastroPedidosListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastroPedidosListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
