import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserGuestComponent } from './user-guest.component';

describe('UserGuestComponent', () => {
  let component: UserGuestComponent;
  let fixture: ComponentFixture<UserGuestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserGuestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserGuestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
