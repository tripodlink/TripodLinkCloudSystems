
export interface IAllDataDictionaryJoin {

  //Item Group
  itemGroupID: string;
  itemgroupname: string;

  //Item Master
  itemMasterID: string;
  ItemGroup: string;
  ItemName: string;
  Unit: string;
  Supplier: string;
  Manufacturer: string;

  //UnitCode
  code: string;
  description: string;
  shortDescription: string;

  //Manufacturer
  mauFactID: string;
  ManufactName: string;

  //Supplier
  suppID: string;
  SuppName: string;
}
