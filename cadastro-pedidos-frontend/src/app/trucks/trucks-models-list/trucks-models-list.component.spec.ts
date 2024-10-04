import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrucksModelsListComponent } from './trucks-models-list.component';

describe('TrucksModelsListComponent', () => {
  let component: TrucksModelsListComponent;
  let fixture: ComponentFixture<TrucksModelsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrucksModelsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrucksModelsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
