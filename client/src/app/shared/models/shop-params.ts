export class ShopParams {
  private _brandId: number = 0;
  private _typeId: number = 0;
  private _sort: string = 'name';
  private _search: string;
  private _pageNumber: number = 1;
  private _pageSize: number = 6;

  public get brandId(): number {
    return this._brandId;
  }

  public get typeId(): number {
    return this._typeId;
  }

  public get sort(): string {
    return this._sort;
  }

  public get search(): string {
    return this._search;
  }

  public get pageNumber(): number {
    return this._pageNumber;
  }

  public get pageSize(): number {
    return this._pageSize;
  }

  setBrandId(brandId: number): void {
    this._brandId = brandId;
    this.resetPageNumber();
  }

  setTypeId(typeId: number): void {
    this._typeId = typeId;
    this.resetPageNumber();
  }

  setSort(sort: string): void {
    this._sort = sort;
  }

  setSearch(search: string): void {
    this._search = search;
    this.resetPageNumber();
  }

  setPageNumber(pageNumber: number): void {
    this._pageNumber = pageNumber;
  }

  setPageSize(pageSize: number): void {
    this._pageSize = pageSize;
  }

  private resetPageNumber(): void {
    this._pageNumber = 1;
  }
}
