import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrucksModelsEditorModalComponent } from './trucks-models-editor-modal.component';

describe('TrucksModelsEditorModalComponent', () => {
  let component: TrucksModelsEditorModalComponent;
  let fixture: ComponentFixture<TrucksModelsEditorModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrucksModelsEditorModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrucksModelsEditorModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
