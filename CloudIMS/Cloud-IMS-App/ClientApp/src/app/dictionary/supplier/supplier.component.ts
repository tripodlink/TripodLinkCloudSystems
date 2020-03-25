import { Component, OnInit } from '@angular/core';
import { ISupplier } from '../../classes/ISupplier.interface';
import { SupplierService } from '../../services/supplier.service';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
})
export class SupplierComponent implements OnInit {
  suppliers: ISupplier[];
  message: string;

  constructor(private supplierService: SupplierService) {
  }

  ngOnInit(): void {
    this.supplierService.getSupplier().subscribe((suppliers)=>this.suppliers = suppliers)
  }

}
