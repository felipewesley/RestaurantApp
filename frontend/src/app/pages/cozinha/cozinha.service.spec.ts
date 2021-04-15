/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CozinhaService } from './cozinha.service';

describe('Service: Cozinha', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CozinhaService]
    });
  });

  it('should ...', inject([CozinhaService], (service: CozinhaService) => {
    expect(service).toBeTruthy();
  }));
});
