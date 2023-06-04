import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataIEComponent } from './data-ie.component';

describe('DataIEComponent', () => {
  let component: DataIEComponent;
  let fixture: ComponentFixture<DataIEComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DataIEComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DataIEComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
