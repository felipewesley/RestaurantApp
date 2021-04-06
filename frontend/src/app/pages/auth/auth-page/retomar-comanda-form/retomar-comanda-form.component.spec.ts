/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RetomarComandaFormComponent } from './retomar-comanda-form.component';

describe('RetomarComandaFormComponent', () => {
  let component: RetomarComandaFormComponent;
  let fixture: ComponentFixture<RetomarComandaFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RetomarComandaFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RetomarComandaFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
