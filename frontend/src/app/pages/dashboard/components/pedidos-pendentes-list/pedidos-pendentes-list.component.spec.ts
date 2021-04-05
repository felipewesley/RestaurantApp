import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PedidosPendentesListComponent } from './pedidos-pendentes-list.component';

describe('PedidosPendentesListComponent', () => {
  let component: PedidosPendentesListComponent;
  let fixture: ComponentFixture<PedidosPendentesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PedidosPendentesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PedidosPendentesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
