import { Component } from '@angular/core';

import { UnitCodeService } from '../../services/UnitCode.service';
import { IUnitCode } from '../../classes/IUnitCode.interface';

@Component({
  selector: 'dic-unitCode',
  templateUrl: './unitCode.component.html',
})
export class UnitCodeComponent {
  unitCodes: IUnitCode[];
  message: string;

  constructor(private unitcodeService: UnitCodeService ) {
  }

  ngOnInit(): void {
    this.unitcodeService.getUnitCodes().subscribe((unitCodes) => this.unitCodes = unitCodes);
  }
}
