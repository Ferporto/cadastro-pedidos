import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PedidosItensListaComponent } from './pedidos-itens-lista.component';

describe('PedidosItensListaComponent', () => {
  let component: PedidosItensListaComponent;
  let fixture: ComponentFixture<PedidosItensListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PedidosItensListaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PedidosItensListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
