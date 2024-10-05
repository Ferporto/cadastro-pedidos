import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroPedidosModelsEditorModalComponent } from './cadastro-pedidos-models-editor-modal.component';

describe('CadastroPedidosModelsEditorModalComponent', () => {
  let component: CadastroPedidosModelsEditorModalComponent;
  let fixture: ComponentFixture<CadastroPedidosModelsEditorModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CadastroPedidosModelsEditorModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastroPedidosModelsEditorModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
