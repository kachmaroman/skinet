import { IBasketItem } from './../shared/models/basket';
import { BasketService } from './basket.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  basket$: Observable<IBasket>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  onDecrementItemQuantity(item: IBasketItem) {
    this.basketService.decrementItemQuantity(item);
  }

  onIncrementItemQuantity(item: IBasketItem) {
    this.basketService.incrementItemQuantity(item);
  }

  onRemoveBasketItem(item: IBasketItem) {
    this.basketService.removeItemFromBasket(item);
  }
}
