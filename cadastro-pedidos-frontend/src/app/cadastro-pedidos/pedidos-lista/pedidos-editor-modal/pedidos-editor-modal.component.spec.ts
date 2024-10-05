import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PedidosEditorModalComponent } from './pedidos-editor-modal.component';

describe('PedidosEditorModalComponent', () => {
  let component: PedidosEditorModalComponent;
  let fixture: ComponentFixture<PedidosEditorModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PedidosEditorModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PedidosEditorModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
