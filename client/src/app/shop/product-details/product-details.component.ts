import { ShopService } from './../shop.service';
import { IProduct } from './../../shared/models/product';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product: IProduct;

  constructor(
    private shopServide: ShopService,
    private route: ActivatedRoute,
    private bcService: BreadcrumbService) {
    this.bcService.set('@productDetails', { label: '', skip: true });
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(): void {
    const productId: number = +this.route.snapshot.params['id'];
    this.shopServide.getProduct(productId).subscribe(product => {
      this.product = product;
      this.bcService.set('@productDetails', { label: product.name, skip: false });
    });
  }
}
