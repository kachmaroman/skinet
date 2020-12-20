import { BasketService } from './../../basket/basket.service';
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
  quantity: number = 1;

  constructor(
    private route: ActivatedRoute,
    private shopServide: ShopService,
    private bcService: BreadcrumbService,
    private basketService: BasketService) {
    this.bcService.set('@productDetails', { skip: true });
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

  onDecrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  onIncrementQuantity() {
    this.quantity++;
  }

  onAddItemToBasket(): void {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }
}
