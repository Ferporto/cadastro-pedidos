import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrucksEditorModalComponent } from './trucks-editor-modal.component';

describe('TrucksEditorModalComponent', () => {
  let component: TrucksEditorModalComponent;
  let fixture: ComponentFixture<TrucksEditorModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrucksEditorModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrucksEditorModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
