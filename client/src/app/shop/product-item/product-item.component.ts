import { BasketService } from './../../basket/basket.service';

import { IProduct } from '../../shared/models/product';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent {

  @Input() product: IProduct;

  constructor(private basketService: BasketService) { }

  onAddItemToBasket(): void {
    this.basketService.addItemToBasket(this.product);
  }
}
