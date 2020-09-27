import { IiTemMaster } from "../ItemMaster/IitemMaster.interface";

export class IitemMasterClass implements IiTemMaster{
  id: string;
  ItemGroup: string;
  ItemName: string;
  Unit: string;
  minimumStockLevel: number
  Supplier: string;
  Manufacturer: string;
}
