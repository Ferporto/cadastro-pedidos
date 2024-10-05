import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroPedidosEditorModalComponent } from './cadastro-pedidos-editor-modal.component';

describe('CadastroPedidosEditorModalComponent', () => {
  let component: CadastroPedidosEditorModalComponent;
  let fixture: ComponentFixture<CadastroPedidosEditorModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CadastroPedidosEditorModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastroPedidosEditorModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
