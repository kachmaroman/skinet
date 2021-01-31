import { IBasket, IBasketItem } from './../../models/basket';
import { Observable } from 'rxjs';
import { BasketService } from './../../../basket/basket.service';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent implements OnInit {

  @Input() isBasket: boolean = true;
  @Output() decrement: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() increment: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() remove: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();

  basket$: Observable<IBasket>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  onDecrementItemQuantity(item: IBasketItem): void {
    this.decrement.emit(item);
  }

  onIncrementItemQuantity(item: IBasketItem): void {
    this.increment.emit(item);
  }

  onRemoveBasketItem(item: IBasketItem): void {
    this.remove.emit(item);
  }
}
