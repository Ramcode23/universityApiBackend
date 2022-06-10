import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrollCardComponent } from './enroll-card.component';

describe('EnrollCardComponent', () => {
  let component: EnrollCardComponent;
  let fixture: ComponentFixture<EnrollCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrollCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrollCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
