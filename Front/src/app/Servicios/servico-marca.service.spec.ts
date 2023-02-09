import { TestBed } from '@angular/core/testing';

import { ServicoMarcaService } from './servico-marca.service';

describe('ServicoMarcaService', () => {
  let service: ServicoMarcaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicoMarcaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
