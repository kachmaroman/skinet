import { IType } from './../shared/models/type';
import { IBrand } from './../shared/models/brand';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { ShopParams } from '../shared/models/shop-params';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  @ViewChild('search', { static: false }) searchTerm: ElementRef;

  private _products: IProduct[];
  private _brands: IBrand[];
  private _types: IType[];

  private _shopParams = new ShopParams();
  private _totalCount: number;

  public get products(): IProduct[] {
    return this._products;
  }

  public get brands(): IBrand[] {
    return this._brands;
  }

  public get types(): IType[] {
    return this._types;
  }

  public get shopParams(): ShopParams {
    return this._shopParams;
  }

  public get totalCount(): number {
    return this._totalCount;
  }

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: Hign to Low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(): void {
    this.shopService.getProducts(this.shopParams)
      .subscribe(response => {
        this._products = response.data;
        this.shopParams.setPageNumber(response.pageIndex);
        this.shopParams.setPageSize(response.pageSize);
        this._totalCount = response.count;
      });
  }

  getBrands(): void {
    this.shopService.getBrands().subscribe(response => this._brands = [{ id: 0, name: 'All' }, ...response]);
  }

  getTypes(): void {
    this.shopService.getTypes().subscribe(response => this._types = [{ id: 0, name: 'All' }, ...response]);
  }

  onBrandSelected(brandId: number): void {
    if (this.shopParams.brandId === brandId) { return; }

    this.shopParams.setBrandId(brandId);
    this.getProducts();
  }

  onTypeSelected(typeId: number): void {
    if (this.shopParams.typeId === typeId) { return; }

    this.shopParams.setTypeId(typeId);
    this.getProducts();
  }

  onSortSelected(sort: string): void {
    this.shopParams.setSort(sort);
    this.getProducts();
  }

  onSearch(): void {
    const search: string = this.searchTerm.nativeElement.value;

    if (!search || search === this.shopParams.search) { return; }

    this.shopParams.setSearch(this.searchTerm.nativeElement.value);
    this.getProducts();
  }

  onReset(): void {
    if (!this.searchTerm.nativeElement.value) { return; }

    this.searchTerm.nativeElement.value = '';
    this._shopParams = new ShopParams();
    this.getProducts();
  }

  onPageChanged(page: number): void {
    if (this.shopParams.pageNumber === page) { return; }

    this.shopParams.setPageNumber(page);
    this.getProducts();
  }
}
